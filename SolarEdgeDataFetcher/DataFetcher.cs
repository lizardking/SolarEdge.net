using SolarEdgeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace SolarEdgeDataFetcher
{
    /// <summary>
    /// The DataFetcher class reads data from the SolarEdge Inverter and/or Meter and maps the received information to the SolarEdge data classes found in the SolarEdgeData project.
    /// It will receive and update data on regular user defineable intervals (see config properties). 
    /// This class is implemented as a singleton class since the SolarEdge inverter can only handle 1 modbus TCP connection at the time.
    /// To use this class get the class instance from the static Instance property (DataFetcher.Instance).
    /// Use the Start() and Stop() methods to start and stop the data fetcher.
    /// The received data is available from the SolarEdgeFullData and SolarEdgeBaseData properties. SolarEdgeDataIsValid indicates whether the current values of these properties are valid.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public sealed class DataFetcher : IDisposable
    {

        #region Constants
        private const int meterStartPos = 40190;
        private const int meterNumberOfRegisters = 105;
        private const int inverterStartPos = 40069;
        private const int inverterNumberOfRegisters = 48;
        private const int maxSuccessiveFails = 5;

        #endregion

        #region Private static variables
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #endregion


        #region Config Properties
        /// <summary>
        /// Gets or sets the refresh interval in ms.
        /// If the time required for reading and updating the data plus the MinIntervalBetweenUpdatesMs is larger than the RefreshIntervalMs, the RefreshIntervalMs will be overidden ny MinIntervalBetweenUpdatesMs plus the time required for the updates.
        /// </summary>
        /// <value>
        /// The refresh interval ms.
        /// </value>
        public int RefreshIntervalMs { get; set; } = 20000;

        /// <summary>
        /// Gets or sets the minimum interval between updates in ms.
        /// </summary>
        /// <value>
        /// The minimum interval between updates in ms.
        /// </value>
        public int MinIntervalBetweenUpdatesMs { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the ip adress of the modbus TCP.
        /// </summary>
        /// <value>
        /// The ip adress.
        /// </value>
        public string IPAdress { get; set; }

        /// <summary>
        /// Gets or sets the modbus TCP port.
        /// </summary>
        /// <value>
        /// The modbus port.
        /// </value>
        public int ModBusPort { get; set; } = 502;

        /// <summary>
        /// Gets or sets the connection timeout in ms for the ModBus.
        /// </summary>
        /// <value>
        /// The connection timeout in ms.
        /// </value>
        public int ConnectionTimeoutMs { get; set; } = 2000;

        /// <summary>
        /// Gets or sets a value indicating whether inverter data should be read.
        /// </summary>
        /// <value>
        ///   <c>true</c> if inverter data should be read; otherwise, <c>false</c>.
        /// </value>
        public bool ReadInverterData { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the meter data should be read.
        /// </summary>
        /// <value>
        ///   <c>true</c> if should be read; otherwise, <c>false</c>.
        /// </value>
        public bool ReadMeterData { get; set; } = true;


        /// <summary>
        /// Gets or sets the data update mode.
        /// </summary>
        /// <value>
        /// The data update mode.
        /// </value>
        public DataUpdateModeEnum DataUpdateMode { get; set; } = DataUpdateModeEnum.UpdateExistingObjects;

        /// <summary>
        /// Gets or sets the number of ms after which the DataIsValid property will be set to false if the data has not been updated.
        /// </summary>
        /// <value>
        /// The number of ms after which the DataIsValid property will be set to false if the data has not been updated (default 120000 ms resp. 2min).
        /// </value>
        public int DataInvalidAfterMs { get; set; } = 2 * 60 * 1000;


        #endregion


        #region Public methods
        /// <summary>
        /// Starts the DataFetcher.
        /// </summary>
        public void Start()
        {
            lock (locker)
            {
                if (modbusClient == null)
                {
                    log.Info($"Starting {nameof(DataFetcher)}");
                    modbusClient = new EasyModbus.ModbusClient
                    {
                        IPAddress = IPAdress,
                        Port = ModBusPort,
                        ConnectionTimeout = ConnectionTimeoutMs
                    };

                    dataValidTimeoutTimer = new Timer
                    {
                        AutoReset=false,
                        Interval=DataInvalidAfterMs
                    };
                    dataValidTimeoutTimer.Elapsed += DataValidTimeoutTimer_Elapsed;
                    updateTimer = new Timer()
                    {
                        AutoReset = false,
                        Interval = 10,
                    };
                    updateTimer.Elapsed += UpdateTimer_Elapsed;
                    updateTimer.Start();
                    log.Info($"{nameof(DataFetcher)} started");
                }
                else
                {
                    log.Warn($"Cant start {nameof(DataFetcher)}, since it has already been started.");
                }
            }
        }

   

        /// <summary>
        /// Stops the DataFetcher.
        /// </summary>
        public void Stop()
        {
            lock (locker)
            {
                if (modbusClient != null)
                {
                    log.Info($"Stopping {nameof(DataFetcher)}");
                    dataValidTimeoutTimer.Stop();
                    dataValidTimeoutTimer.Elapsed -= DataValidTimeoutTimer_Elapsed;
                    updateTimer.Stop();
                    updateTimer.Elapsed -= UpdateTimer_Elapsed;
                    modbusClient.Disconnect();
                    ConnectionEstablished = false;

                    updateTimer = null;
                    modbusClient = null;
                    SolarEdgeDataIsValid = false;
                    log.Info($"{nameof(DataFetcher)} stopped");

                }
                else
                {
                    log.Warn($"Cant stop {nameof(DataFetcher)}, since it has not been started.");
                }
            }
        }
        #endregion


        #region Modbus handling and data updating (all private)

        private Timer updateTimer = null;

        private EasyModbus.ModbusClient modbusClient = null;

        private object locker = new object();

        private Timer dataValidTimeoutTimer = new Timer();

        private Queue<DateTime> connEstablishedQueue = new Queue<DateTime>();

        private int successiveFailCount = 0;

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                if (modbusClient != null)
                {
                    updateTimer.Stop();

                    DateTime updateStart = DateTime.Now;

                    bool updateSucces = false;
                    int[] inverterResponse = null;
                    int[] meterResponse = null; ;

                    TimeSpan inverterReadDuration = TimeSpan.MinValue;
                    TimeSpan meterReadDuration = TimeSpan.MinValue;
                    string ExceptionText = "";
                    try
                    {
                        if (!ConnectionEstablished)
                        {

                            try
                            {
                                ConnectToModBus();
                                log.Debug($"Modbus connection established for {IPAdress}:{ModBusPort}");
                            }
                            catch (Exception E)
                            {
                                log.Debug($"A exeception occured while establishing Modbos connection to {IPAdress}:{ModBusPort}. {E.Message}", E);
                                throw new Exception($"A exeception occured while establishing Modbos connection to {IPAdress}:{ModBusPort} {E.Message}", E);
                            }
                        }

                        if (ReadInverterData)
                        {
                            try
                            {
                                DateTime StartTime = DateTime.Now;
                                inverterResponse = modbusClient.ReadHoldingRegisters(inverterStartPos, inverterNumberOfRegisters);
                                inverterReadDuration = DateTime.Now - StartTime;
                                log.Debug($"Inverter data read: {inverterReadDuration.TotalMilliseconds}ms");

                            }
                            catch (Exception E)
                            {
                                log.Debug($"Reading inverter data failed. {E.Message}", E);
                                throw new Exception($"Reading inverter data failed. {E.Message}", E);
                            }

                            if (inverterResponse == null || inverterResponse.All(v => v == 0))
                            {
                                log.Debug("No valid data received from inverter.");
                                throw new Exception("No valid data received from inverter.");
                            }
                        }

                        if (ReadMeterData)
                        {
                            try
                            {
                                DateTime StartTime = DateTime.Now;
                                meterResponse = modbusClient.ReadHoldingRegisters(meterStartPos, meterNumberOfRegisters);
                                meterReadDuration = DateTime.Now - StartTime;
                                log.Debug($"Meter data read: {meterReadDuration.TotalMilliseconds}ms");
                            }
                            catch (Exception E)
                            {

                                log.Debug($"Reading meter data failed. {E.Message}", E);
                                throw new Exception($"Reading meter data failed. {E.Message}", E);
                            }


                            if (meterResponse == null || meterResponse.All(v => v == 0))
                            {
                                log.Debug("No valid data received from inverter.");
                                throw new Exception("No valid data received from inverter.");
                            }
                        }

                    }
                    catch (Exception E)
                    {
                        ExceptionText = E.Message;
                        
                    }

                    if (!ExceptionText.IsNullOrWhiteSpace())
                    {
                        log.Debug($"Exception occured while establishing the connection or while reading data.\n{ExceptionText}");
                        DisconnectFromModbus();
                      
                    }
                    else
                    {
                        ModbusDataUpdater U = new ModbusDataUpdater();

                        try
                        {
                            switch (DataUpdateMode)
                            {
                                case DataUpdateModeEnum.UpdateExistingObjects:

                                    lock (DataLocker)
                                    {
                                        if (SolarEdgeFullData == null) SolarEdgeFullData = new SolarEdgeFullData();
                                        
                                        if (ReadInverterData) { U.UpdateData(inverterResponse, inverterStartPos, SolarEdgeFullData); }
                                        if (ReadMeterData) { U.UpdateData(meterResponse, meterStartPos, SolarEdgeFullData); }

                                        if (_SolarEdgeBaseData == null) SolarEdgeBaseData = new SolarEdgeBaseData();
                                        if (ReadInverterData) { U.UpdateData(inverterResponse, inverterStartPos, SolarEdgeBaseData); }
                                        if (ReadMeterData) { U.UpdateData(meterResponse, meterStartPos, SolarEdgeBaseData); }
                                       
                                    };
                                    break;
                                case DataUpdateModeEnum.CreateNewObjects:
                                    SolarEdgeFullData FD = new SolarEdgeFullData();
                                    if (ReadInverterData) { U.UpdateData(inverterResponse, inverterStartPos, FD); }
                                    if (ReadMeterData) { U.UpdateData(meterResponse, meterStartPos, FD); }

                                    SolarEdgeBaseData BD = new SolarEdgeBaseData();
                                    if (ReadInverterData) { U.UpdateData(inverterResponse, inverterStartPos, BD); }
                                    if (ReadMeterData) { U.UpdateData(meterResponse, meterStartPos, BD); }


                                    lock (DataLocker)
                                    {
                                        SolarEdgeFullData = FD;
                                        SolarEdgeBaseData = BD;
                                       
                                        
                                    };
                                    break;
                                default:
                                    log.Error($"Unknown {nameof(DataUpdateMode)} {DataUpdateMode}");
                                    throw new Exception($"Unknown {nameof(DataUpdateMode)} {DataUpdateMode}");
                            }

                          

                            successiveFailCount = 0;
                            updateSucces = true;
                            log.Debug("Data successfully updated");
                        }
                        catch (Exception E)
                        {
                            successiveFailCount++;
                            
                            log.Warn($"Updating data failed. {E.Message}", E);

                            if (successiveFailCount > maxSuccessiveFails)
                            {
                                DisconnectFromModbus();
                                log.Debug($"Disconnected from Modbus on {IPAdress}:{ModBusPort}\n{ExceptionText}. Data update failed more than {maxSuccessiveFails} times.");
                                successiveFailCount = 0;
                            }

                        }
                    }




                    if (updateSucces)
                    {
                        LastDataUpdate = DateTime.Now;

                        dataValidTimeoutTimer.Stop();
                        dataValidTimeoutTimer.Interval = DataInvalidAfterMs;
                        dataValidTimeoutTimer.Start();

                        OnSolarEdgeDataUpdated();
                        SolarEdgeDataIsValid = true;
                    }
                    else
                    {
                        
                        OnSolarEdgeDataUpdateFailed();
                    }


                    int totalUpdateDurationMs = (int)(DateTime.Now - updateStart).TotalMilliseconds;

                    int sleepDurationMs = (RefreshIntervalMs - totalUpdateDurationMs).Limit(MinIntervalBetweenUpdatesMs.Limit(10, int.MaxValue), int.MaxValue);

                    log.Debug($"Will wait for {sleepDurationMs}ms before next update.");

                    updateTimer.Interval = sleepDurationMs;

                    updateTimer.Start();
                }
                else
                {
                    log.Warn($"{nameof(modbusClient)} is null. Most likely Stop has been called.");
                }
            }
        }

        private void DataValidTimeoutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SolarEdgeDataIsValid = false;
        }


        private void ConnectToModBus()
        {
            modbusClient.Connect();
            ConnectionEstablished = true;
            ConnectionLastEstablished = DateTime.Now;
            connEstablishedQueue.Enqueue(ConnectionLastEstablished);
            while (connEstablishedQueue.Count > 100)
            {
                connEstablishedQueue.Dequeue();
            }
        }

        private void DisconnectFromModbus()
        {
            try
            {
                modbusClient.Disconnect();
            }
            catch (Exception E)
            {

                log.Warn($"A exception has occured while diconnection from the modbus TCP. {E.Message}", E);
            }
            ConnectionEstablished = false;
        }
        #endregion


        #region Events
        /// <summary>
        /// Occurs when the update of the SolarEdgeData has failed.
        /// </summary>
        public event EventHandler<EventArgs> SolarEdgeDataUpdateFailed;
        /// <summary>
        /// Is called when the update of the SolarEdgeData has failed.
        /// </summary>
        private void OnSolarEdgeDataUpdateFailed()
        {
            SolarEdgeDataUpdateFailed?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// Occurs when the SolarEdgeData has been updated.
        /// </summary>
        public event EventHandler<EventArgs> SolarEdgeDataUpdated;
        /// <summary>
        /// Is called when the SolarEdgeData has been updated.
        /// </summary>
        private void OnSolarEdgeDataUpdated()
        {
            SolarEdgeDataUpdated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Occurs when SolarEdgeDataIsValidChanged haschanged.
        /// </summary>
        public event EventHandler<EventArgs> SolarEdgeDataIsValidChanged;
        /// <summary>
        /// Is called when SolarEdgeDataIsValidChanged has changed.
        /// </summary>
        private void OnSolarEdgeDataIsValidChanged()
        {
            SolarEdgeDataIsValidChanged?.Invoke(this, new EventArgs());
        }


        #endregion

        #region Public read only properties
        private object DataLocker = new object();
        private SolarEdgeFullData _SolarEdgeFullData;
        private SolarEdgeBaseData _SolarEdgeBaseData;

        private DateTime _LastDataUpdate = DateTime.MinValue;
        private bool _solarEdgeDataIsValid = false;

        /// <summary>
        /// Gets a value indicating whether the data of the SolarEdge data properties is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the data is valid; otherwise, <c>false</c>.
        /// </value>
        public bool SolarEdgeDataIsValid
        {
            get { lock (DataLocker) { return _solarEdgeDataIsValid; }; }
            private set
            {
                lock (DataLocker)
                {
                    if (_solarEdgeDataIsValid != value)
                    {
                        _solarEdgeDataIsValid = value;
                        OnSolarEdgeDataIsValidChanged();
                    }
                }
            }
        }


        /// <summary>
        /// Gets a value indicating whether the connection is established.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the connection is established; otherwise, <c>false</c>.
        /// </value>
        public bool ConnectionEstablished { get; private set; } = false;

        /// <summary>
        /// Gets the timestamp when the connection was established.
        /// </summary>
        /// <value>
        /// The timestamp when the connection was established.
        /// </value>
        public DateTime ConnectionLastEstablished { get; private set; } = DateTime.MinValue;

        /// <summary>
        /// Gets the timestamp of the last data update.
        /// </summary>
        /// <value>
        /// The timestamp of the last data update.
        /// </value>
        public DateTime LastDataUpdate
        {
            get
            {
                lock (DataLocker)
                {
                    return _LastDataUpdate;
                }
            }
            private set
            {
                lock (DataLocker)
                {
                    _LastDataUpdate = value;
                }
            }
        }

        /// <summary>
        /// Gets the SolarEdgeFullData.
        /// This object contains all inverter and meter fields
        /// </summary>
        /// <value>
        /// The SolarEdgeFullData.
        /// </value>
        public SolarEdgeFullData SolarEdgeFullData
        {
            get { lock (DataLocker) { return _SolarEdgeFullData; } }
            private set { lock (DataLocker) { _SolarEdgeFullData = value; } }
        }

        /// <summary>
        /// Gets the SolarEdgeBaseData.
        /// This object does only contain the most important fields.
        /// </summary>
        /// <value>
        /// The SolarEdgeBaseData.
        /// </value>
        public SolarEdgeBaseData SolarEdgeBaseData
        {
            get { lock (DataLocker) { return _SolarEdgeBaseData; } }
            private set { lock (DataLocker) { _SolarEdgeBaseData = value; } }
        }


        /// <summary>
        /// Gets a value indicating whether the <see cref="DataFetcher"/> is started.
        /// </summary>
        /// <value>
        ///   <c>true</c> if started; otherwise, <c>false</c>.
        /// </value>
        public bool Started
        {
            get
            {
                return modbusClient != null;
            }
        }




        /// <summary>
        /// Gets the number of times the modbus connection was (re)established during the last hour.
        /// </summary>
        /// <value>
        /// The number of times the modbus connection was (re)established during the last hour.
        /// </value>
        public int ConnectionEstablishedCountLastHour
        {
            get
            {
                DateTime current = DateTime.Now;
                return connEstablishedQueue.Where(DT => (current - DT) < TimeSpan.FromMinutes(60)).Count();
            }
        }
        #endregion


     
        #region IDisposable Support
        private bool disposedValue = false; 


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                    
                }



                disposedValue = true;
            }
        }


        /// <summary>
        /// FDisposes the class
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region Singleton implementation
        private static readonly Lazy<DataFetcher> SingletonInstance = new Lazy<DataFetcher>(() => new DataFetcher());

        public static DataFetcher Instance { get { return SingletonInstance.Value; } }

        private DataFetcher()
        {
        }

        #endregion


    }
}
