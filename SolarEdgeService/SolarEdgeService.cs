using SolarEdgeDataFetcher;
using System;
using System.ServiceProcess;

namespace SolarEdgeService
{
    public partial class SolarEdgeService : ServiceBase
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public SolarEdgeService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Defines the actions at the start of the service
        /// </summary>
        /// <param name="args">Start para</param>
        protected override void OnStart(string[] args)
        {
            log.Info($"Starting the {nameof(SolarEdgeService)}");
            DataFetcher DF = DataFetcher.Instance;
            DF.ConnectionTimeoutMs = SolarEdgeServiceSettings.Default.ConnectionTimeoutMs;
            DF.IPAdress = SolarEdgeServiceSettings.Default.IPAdress;
            DF.ModBusPort = SolarEdgeServiceSettings.Default.ModBusPort;
            DF.RefreshIntervalMs = SolarEdgeServiceSettings.Default.RefreshIntervalMs.Limit(1,int.MaxValue);
            DF.ReadInverterData = SolarEdgeServiceSettings.Default.ReadInverterData;
            DF.ReadMeterData = SolarEdgeServiceSettings.Default.ReadMeterData;
            DF.MinIntervalBetweenUpdatesMs = SolarEdgeServiceSettings.Default.MinIntervalBetweenUpdatesMs.Limit(1, int.MaxValue);

            DataUpdateModeEnum dataUpdateMode;
            if (Enum.TryParse(SolarEdgeServiceSettings.Default.DataUpdateMode, out dataUpdateMode))
            {
                DF.DataUpdateMode = dataUpdateMode;
            }
            else
            {
                log.Debug($"No valid DataUpdateMode valkue found. Will use {DataUpdateModeEnum.UpdateExistingObjects} as default. Valid values are {DataUpdateModeEnum.UpdateExistingObjects} and {DataUpdateModeEnum.CreateNewObjects}");
                DF.DataUpdateMode = DataUpdateModeEnum.UpdateExistingObjects;
            }


            DF.Start();
            CommunicationManager.Instance.ActivateServices();

            log.Info($"{nameof(SolarEdgeService)} started");

        }

        /// <summary>
        /// Defines the actions required to stop the service
        /// </summary>
        protected override void OnStop()
        {
            log.Info($"Stopping the {nameof(SolarEdgeService)}");
            CommunicationManager.Instance.DeactivateServices();
            DataFetcher.Instance.Stop();
            log.Info($"{nameof(SolarEdgeService)} stopped");

        }
    }
}
