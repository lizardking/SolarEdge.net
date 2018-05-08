using SolarEdgeData.Attributes;
using SolarEdgeData.TypeConverters;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SolarEdgeData
{
    [DataContract]
    public class SolarEdgeFullData : INotifyPropertyChanged
    {
        const int MeterBaseOffset = 0;
        const int InverterBaseOffset = -1;



        #region Property changed event
        /// <summary>
        /// Fires after a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Is after the value of a property has changed changed.
        //  Call this method using the following syntax: OnPropertyChanged(nameof(PropertyName))
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        protected void OnPropertyChanged(string PropertyName)
        {
            LastUpdate = DateTime.Now;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }
        #endregion


        #region Property LastUpdate of type DateTime
        /// <summary>
        /// Gets the LastUpdate of type DateTime
        /// </summary>
        /// <value>
        /// The Lastpdate timestamp.
        /// </value>
        [Category("General")]
        [DisplayName("Last Update Timestamp")]
        [Description("Timestamp of the last update of a data point")]
       
        [DataMember]
        public DateTime LastUpdate
        {
            get { return _LastUpdate; }
            private set
            {
                _LastUpdate = value;

            }
        }
        private DateTime _LastUpdate = DateTime.MinValue;
        #endregion


        #region Inverter
        #region Inverter AC Current

        #region Property Inverter_AC_Current of type float with property changed event
        /// <summary>
        /// Gets or sets the AC_Current of type float
        /// </summary>
        /// <value>
        /// The AC_Current.
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Total Current value ")]
        [Description("AC Total Current value in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40072 + InverterBaseOffset, 40076 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_Current
        {
            get { return _Inverter_AC_Current; }
            set
            {
                if (_Inverter_AC_Current != value)
                {
                    _Inverter_AC_Current = value;
                    OnPropertyChanged(nameof(Inverter_AC_Current));
                }
            }
        }
        private float _Inverter_AC_Current = -1;
        #endregion

        #region Property Inverter_AC_CurrentAA  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_CurrentAA  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_CurrentAA in Amps .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Phase A Current value")]
        [Description("AC Phase A Current value in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40073 + InverterBaseOffset, 40076 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_CurrentAA
        {
            get { return _Inverter_AC_CurrentAA; }
            set
            {
                if (_Inverter_AC_CurrentAA != value)
                {
                    _Inverter_AC_CurrentAA = value;
                    OnPropertyChanged(nameof(Inverter_AC_CurrentAA));
                }
            }
        }
        private float _Inverter_AC_CurrentAA = -1;
        #endregion

        #region Property Inverter_AC_CurrentAB  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_CurrentAB  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_CurrentAB .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Phase B Current value")]
        [Description("AC Phase B Current value in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40074 + InverterBaseOffset, 40076 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_CurrentAB
        {
            get { return _Inverter_AC_CurrentAB; }
            set
            {
                if (_Inverter_AC_CurrentAB != value)
                {
                    _Inverter_AC_CurrentAB = value;
                    OnPropertyChanged(nameof(Inverter_AC_CurrentAB));
                }
            }
        }
        private float _Inverter_AC_CurrentAB = -1;
        #endregion

        #region Property Inverter_AC_CurrentAC  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_CurrentAC  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_CurrentAC .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Phase C Current value")]
        [Description("AC Phase C Current value in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40075 + InverterBaseOffset, 40076 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_CurrentAC
        {
            get { return _Inverter_AC_CurrentAC; }
            set
            {
                if (_Inverter_AC_CurrentAC != value)
                {
                    _Inverter_AC_CurrentAC = value;
                    OnPropertyChanged(nameof(Inverter_AC_CurrentAC));
                }
            }
        }
        private float _Inverter_AC_CurrentAC = -1;
        #endregion
        #endregion

        #region Inverter AC Voltage
        #region Property Inverter_AC_VoltageAB  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageAB  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageAB .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase AB value")]
        [Description("AC Voltage Phase AB value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40077 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_VoltageAB
        {
            get { return _Inverter_AC_VoltageAB; }
            set
            {
                if (_Inverter_AC_VoltageAB != value)
                {
                    _Inverter_AC_VoltageAB = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageAB));
                }
            }
        }
        private float _Inverter_AC_VoltageAB = -1;
        #endregion

        #region Property Inverter_AC_VoltageBC  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageBC  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageBC .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase BC value")]
        [Description("AC Voltage Phase BC value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40078 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_VoltageBC
        {
            get { return _Inverter_AC_VoltageBC; }
            set
            {
                if (_Inverter_AC_VoltageBC != value)
                {
                    _Inverter_AC_VoltageBC = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageBC));
                }
            }
        }
        private float _Inverter_AC_VoltageBC = -1;
        #endregion

        #region Property Inverter_AC_VoltageCA  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageCA  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageCA .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase CA value")]
        [Description("AC Voltage Phase CA value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40079 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]

        public float Inverter_AC_VoltageCA
        {
            get { return _Inverter_AC_VoltageCA; }
            set
            {
                if (_Inverter_AC_VoltageCA != value)
                {
                    _Inverter_AC_VoltageCA = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageCA));
                }
            }
        }
        private float _Inverter_AC_VoltageCA = -1;
        #endregion


        #region Property Inverter_AC_VoltageAN  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageAN  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageAN .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase A to N value")]
        [Description("AC Voltage Phase A to N value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40080 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]

        public float Inverter_AC_VoltageAN
        {
            get { return _Inverter_AC_VoltageAN; }
            set
            {
                if (_Inverter_AC_VoltageAN != value)
                {
                    _Inverter_AC_VoltageAN = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageAN));
                }
            }
        }
        private float _Inverter_AC_VoltageAN = -1;
        #endregion

        #region Property Inverter_AC_VoltageBN  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageBN  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageBN .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase B to N value")]
        [Description("AC Voltage Phase B to N value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40081 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_VoltageBN
        {
            get { return _Inverter_AC_VoltageBN; }
            set
            {
                if (_Inverter_AC_VoltageBN != value)
                {
                    _Inverter_AC_VoltageBN = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageBN));
                }
            }
        }
        private float _Inverter_AC_VoltageBN = -1;
        #endregion

        #region Property Inverter_AC_VoltageCN  of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VoltageCN  of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VoltageCN .
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Voltage Phase C to N value")]
        [Description("AC Voltage Phase C to N value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40082 + InverterBaseOffset, 40083 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_VoltageCN
        {
            get { return _Inverter_AC_VoltageCN; }
            set
            {
                if (_Inverter_AC_VoltageCN != value)
                {
                    _Inverter_AC_VoltageCN = value;
                    OnPropertyChanged(nameof(Inverter_AC_VoltageCN));
                }
            }
        }
        private float _Inverter_AC_VoltageCN = -1;
        #endregion
        #endregion

        #region Inverter AC Power

        #region Property AC_Power of type float with property changed event
        /// <summary>
        /// Gets or sets the AC_Power of type float
        /// </summary>
        /// <value>
        /// The AC_Power.
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Power value")]
        [Description("AC Power value in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40084 + InverterBaseOffset, 40085 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.int16)]

        public float Inverter_AC_Power
        {
            get { return _Inverter_AC_Power; }
            set
            {
                if (_Inverter_AC_Power != value)
                {
                    _Inverter_AC_Power = value;
                    OnPropertyChanged(nameof(Inverter_AC_Power));
                }
            }
        }
        private float _Inverter_AC_Power = -1;
        #endregion

        #endregion

        #region Property Inverter_AC_Frequency of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_Frequency of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_Frequency.
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Frequency value")]
        [Description("AC Frequency value in Hertz")]
        [TypeConverter(typeof(HertzTypeConverter))]
        [DataMember]
        [ModbusSource(40086 + InverterBaseOffset, 40087 + InverterBaseOffset)]
        public float Inverter_AC_Frequency
        {
            get { return _Inverter_AC_Frequency; }
            set
            {
                if (_Inverter_AC_Frequency != value)
                {
                    _Inverter_AC_Frequency = value;
                    OnPropertyChanged(nameof(Inverter_AC_Frequency));
                }
            }
        }
        private float _Inverter_AC_Frequency = -1;
        #endregion

        #region Property Inverter_AC_VA of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VA of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VA.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Apparent Power")]
        [Description("Apparent Power in Volt-Amps")]
        [TypeConverter(typeof(VoltAmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40088 + InverterBaseOffset, 40089 + InverterBaseOffset)]
        public float Inverter_AC_VA
        {
            get { return _Inverter_AC_VA; }
            set
            {
                if (_Inverter_AC_VA != value)
                {
                    _Inverter_AC_VA = value;
                    OnPropertyChanged(nameof(Inverter_AC_VA));
                }
            }
        }
        private float _Inverter_AC_VA = -1;
        #endregion

        #region Property Inverter_AC_VAR of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_VAR of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_VAR.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Reactive Power")]
        [Description("Reactive Power in VAR")]
        [TypeConverter(typeof(VoltAmpsReactiveTypeConverter))]
        [DataMember]
        [ModbusSource(40090 + InverterBaseOffset, 40091 + InverterBaseOffset)]
        public float Inverter_AC_VAR
        {
            get { return _Inverter_AC_VAR; }
            set
            {
                if (_Inverter_AC_VAR != value)
                {
                    _Inverter_AC_VAR = value;
                    OnPropertyChanged(nameof(Inverter_AC_VAR));
                }
            }
        }
        private float _Inverter_AC_VAR = -1;
        #endregion

        #region Property Inverter_AC_PF of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_PF of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_PF.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Power Factor")]
        [Description("Power Factor in %")]
        [TypeConverter(typeof(PercentTypeConverter))]
        [DataMember]
        [ModbusSource(40092 + InverterBaseOffset, 40093 + InverterBaseOffset)]
        public float Inverter_AC_PF
        {
            get { return _Inverter_AC_PF; }
            set
            {
                if (_Inverter_AC_PF != value)
                {
                    _Inverter_AC_PF = value;
                    OnPropertyChanged(nameof(Inverter_AC_PF));
                }
            }
        }
        private float _Inverter_AC_PF = 0;
        #endregion

        #region Property Inverter_AC_Energy_WH of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_AC_Energy_WH of type float
        /// </summary>
        /// <value>
        /// The Inverter_AC_Energy_WH.
        /// </value>
        [Category("Inverter")]
        [DisplayName("AC Lifetime Energy production ")]
        [Description("AC Lifetime Energy production in WattHours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40094 + InverterBaseOffset, 40096 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32, ScaleFactorSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_AC_Energy_WH
        {
            get { return _Inverter_AC_Energy_WH; }
            set
            {
                if (_Inverter_AC_Energy_WH != value)
                {
                    _Inverter_AC_Energy_WH = value;
                    OnPropertyChanged(nameof(Inverter_AC_Energy_WH));
                }
            }
        }
        private float _Inverter_AC_Energy_WH = 0;
        #endregion

        #region Property Inverter_DC_Current of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_DC_Current of type float
        /// </summary>
        /// <value>
        /// The Inverter_DC_Current.
        /// </value>
        [Category("Inverter")]
        [DisplayName("DC Current value")]
        [Description("DC Current value in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40097 + InverterBaseOffset, 40098 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_DC_Current
        {
            get { return _Inverter_DC_Current; }
            set
            {
                if (_Inverter_DC_Current != value)
                {
                    _Inverter_DC_Current = value;
                    OnPropertyChanged(nameof(Inverter_DC_Current));
                }
            }
        }
        private float _Inverter_DC_Current = 0;
        #endregion

        #region Property Inverter_DC_Voltage of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_DC_Voltage of type float
        /// </summary>
        /// <value>
        /// The Inverter_DC_Voltage.
        /// </value>
        [Category("Inverter")]
        [DisplayName("DC Voltage value")]
        [Description("DC Voltage value in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40099 + InverterBaseOffset, 40100 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        public float Inverter_DC_Voltage
        {
            get { return _Inverter_DC_Voltage; }
            set
            {
                if (_Inverter_DC_Voltage != value)
                {
                    _Inverter_DC_Voltage = value;
                    OnPropertyChanged(nameof(Inverter_DC_Voltage));
                }
            }
        }
        private float _Inverter_DC_Voltage = 0;
        #endregion

        #region Property Inverter_DC_Power of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_DC_Power of type float
        /// </summary>
        /// <value>
        /// The Inverter_DC_Power.
        /// </value>
        [Category("Inverter")]
        [DisplayName("DC Power value")]
        [Description("DC Power value in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40101 + InverterBaseOffset, 40102 + InverterBaseOffset)]
        public float Inverter_DC_Power
        {
            get { return _Inverter_DC_Power; }
            set
            {
                if (_Inverter_DC_Power != value)
                {
                    _Inverter_DC_Power = value;
                    OnPropertyChanged(nameof(Inverter_DC_Power));
                }
            }
        }
        private float _Inverter_DC_Power = 0;
        #endregion

        #region Property Inverter_Temp_Sink of type float with property changed event
        /// <summary>
        /// Gets or sets the Inverter_Temp_Sink of type float
        /// </summary>
        /// <value>
        /// The Inverter_Temp_Sink.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Heat Sink Temperature")]
        [Description("Heat Sink Temperature in Degrees Celsius")]
        [TypeConverter(typeof(CelsiusTypeConverter))]
        [DataMember]
        [ModbusSource(40104 + InverterBaseOffset, 40107 + InverterBaseOffset)]
        public float Inverter_Temp_Sink
        {
            get { return _Inverter_Temp_Sink; }
            set
            {
                if (_Inverter_Temp_Sink != value)
                {
                    _Inverter_Temp_Sink = value;
                    OnPropertyChanged(nameof(Inverter_Temp_Sink));
                }
            }
        }
        private float _Inverter_Temp_Sink = 0;
        #endregion

        //#region Property Inverter_Status of type SolarEdgeInverterStatusEnum with property changed event
        ///// <summary>
        ///// Gets or sets the Status of type SolarEdgeInverterStatusEnum
        ///// </summary>
        ///// <value>
        ///// The Status.
        ///// </value>
        //[Category("Inverter")]
        //[DisplayName("Operating State")]
        //[Description("Operating Sate of the Inverter")]
        // [DataMember][ModbusSource(40108 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint16)]
        //public SolarEdgeInverterStatusFlagEnum Inverter_Status
        //{
        //    get { return _Inverter_Status; }
        //    set
        //    {
        //        if (_Inverter_Status != value)
        //        {
        //            _Inverter_Status = value;
        //            OnPropertyChanged(nameof(Inverter_Status));
        //        }
        //    }
        //}
        //private SolarEdgeInverterStatusFlagEnum _Inverter_Status = SolarEdgeInverterStatusFlagEnum.NotKnown;
        //#endregion
        #endregion

        #region Meter

        #region Property Meter_AC_Current of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Current of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Current.
        /// </value>
        [Category("Meter")]
        [DisplayName("AC Current (sum of active phases)")]
        [Description("AC Current (sum of active phases)  in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40190 + MeterBaseOffset, 40194 + MeterBaseOffset)]
        public float Meter_AC_Current
        {
            get { return _Meter_AC_Current; }
            set
            {
                if (_Meter_AC_Current != value)
                {
                    _Meter_AC_Current = value;
                    OnPropertyChanged(nameof(Meter_AC_Current));
                }
            }
        }
        private float _Meter_AC_Current = 0;
        #endregion

        #region Property Meter_CurrentA of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_CurrentA of type float
        /// </summary>
        /// <value>
        /// The Meter_CurrentA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A AC Current")]
        [Description("Phase A AC Current in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40191 + MeterBaseOffset, 40194 + MeterBaseOffset)]
        public float Meter_CurrentA
        {
            get { return _Meter_CurrentA; }
            set
            {
                if (_Meter_CurrentA != value)
                {
                    _Meter_CurrentA = value;
                    OnPropertyChanged(nameof(Meter_CurrentA));
                }
            }
        }
        private float _Meter_CurrentA = 0;
        #endregion

        #region Property Meter_CurrentB of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_CurrentA of type float
        /// </summary>
        /// <value>
        /// The Meter_CurrentA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B AC Current")]
        [Description("Phase B AC Current in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40192 + MeterBaseOffset, 40194 + MeterBaseOffset)]
        public float Meter_CurrentB
        {
            get { return _Meter_CurrentB; }
            set
            {
                if (_Meter_CurrentB != value)
                {
                    _Meter_CurrentB = value;
                    OnPropertyChanged(nameof(Meter_CurrentB));
                }
            }
        }
        private float _Meter_CurrentB = 0;
        #endregion

        #region Property Meter_CurrentC of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_CurrentC of type float
        /// </summary>
        /// <value>
        /// The Meter_CurrentC.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C AC Current")]
        [Description("Phase C AC Current in Amps")]
        [TypeConverter(typeof(AmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40193 + MeterBaseOffset, 40194 + MeterBaseOffset)]
        public float Meter_CurrentC
        {
            get { return _Meter_CurrentC; }
            set
            {
                if (_Meter_CurrentC != value)
                {
                    _Meter_CurrentC = value;
                    OnPropertyChanged(nameof(Meter_CurrentC));
                }
            }
        }
        private float _Meter_CurrentC = 0;
        #endregion

        #region Property Meter_AC_Voltage_LN of type float with property changed event
        /// <summary>
        /// Gets or sets the AC_Voltage_LN of type float
        /// </summary>
        /// <value>
        /// The AC_Voltage_LN.
        /// </value>
        [Category("Meter")]
        [DisplayName("Line to Neutral AC Voltage (average of active phases)")]
        [Description("Line to Neutral AC Voltage (average of active phases) in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40195 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_LN
        {
            get { return _Meter_AC_Voltage_LN; }
            set
            {
                if (_Meter_AC_Voltage_LN != value)
                {
                    _Meter_AC_Voltage_LN = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_LN));
                }
            }
        }
        private float _Meter_AC_Voltage_LN = 0;
        #endregion

        #region Property Meter_AC_Voltage_AN of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_AN of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_AN.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A to Neutral AC Voltage")]
        [Description("Phase A to Neutral AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40196 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_AN
        {
            get { return _Meter_AC_Voltage_AN; }
            set
            {
                if (_Meter_AC_Voltage_AN != value)
                {
                    _Meter_AC_Voltage_AN = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_AN));
                }
            }
        }
        private float _Meter_AC_Voltage_AN = 0;
        #endregion

        #region Property Meter_AC_Voltage_BN of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_BN of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_BN.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B to Neutral AC Voltage")]
        [Description("Phase B to Neutral AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40197 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_BN
        {
            get { return _Meter_AC_Voltage_BN; }
            set
            {
                if (_Meter_AC_Voltage_BN != value)
                {
                    _Meter_AC_Voltage_BN = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_BN));
                }
            }
        }
        private float _Meter_AC_Voltage_BN = 0;
        #endregion

        #region Property Meter_AC_Voltage_CN of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_CN of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_CN.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C to Neutral AC Voltage")]
        [Description("Phase C to Neutral AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40198 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_CN
        {
            get { return _Meter_AC_Voltage_CN; }
            set
            {
                if (_Meter_AC_Voltage_CN != value)
                {
                    _Meter_AC_Voltage_CN = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_CN));
                }
            }
        }
        private float _Meter_AC_Voltage_CN = 0;
        #endregion

        #region Property Meter_AC_Voltage_LL  of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_LL  of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_LL .
        /// </value>
        [Category("Meter")]
        [DisplayName("Line to Line AC Voltage (average of active phases)")]
        [Description("Line to Line AC Voltage (average of active phases) in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40199 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_LL
        {
            get { return _Meter_AC_Voltage_LL; }
            set
            {
                if (_Meter_AC_Voltage_LL != value)
                {
                    _Meter_AC_Voltage_LL = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_LL));
                }
            }
        }
        private float _Meter_AC_Voltage_LL = 0;
        #endregion

        #region Property Meter_AC_Voltage_AB of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_AB of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_AB.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A to Phase B AC Voltage")]
        [Description("Phase A to Phase B AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40200 + MeterBaseOffset, 40203 + MeterBaseOffset)]
        public float Meter_AC_Voltage_AB
        {
            get { return _Meter_AC_Voltage_AB; }
            set
            {
                if (_Meter_AC_Voltage_AB != value)
                {
                    _Meter_AC_Voltage_AB = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_AB));
                }
            }
        }
        private float _Meter_AC_Voltage_AB = 0;
        #endregion

        #region Property Meter_AC_Voltage_BC of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_BC of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_BC.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B to Phase C AC Voltage")]
        [Description("Phase B to Phase C AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40201 + MeterBaseOffset, 40203 + MeterBaseOffset)]

        public float Meter_AC_Voltage_BC
        {
            get { return _Meter_AC_Voltage_BC; }
            set
            {
                if (_Meter_AC_Voltage_BC != value)
                {
                    _Meter_AC_Voltage_BC = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_BC));
                }
            }
        }
        private float _Meter_AC_Voltage_BC = 0;
        #endregion

        #region Property Meter_AC_Voltage_CA of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Voltage_CA of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Voltage_CA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C to Phase A AC Voltage")]
        [Description("Phase C to Phase A AC Voltage in Volts")]
        [TypeConverter(typeof(VoltsTypeConverter))]
        [DataMember]
        [ModbusSource(40202 + MeterBaseOffset, 40203 + MeterBaseOffset)]

        public float Meter_AC_Voltage_CA
        {
            get { return _Meter_AC_Voltage_CA; }
            set
            {
                if (_Meter_AC_Voltage_CA != value)
                {
                    _Meter_AC_Voltage_CA = value;
                    OnPropertyChanged(nameof(Meter_AC_Voltage_CA));
                }
            }
        }
        private float _Meter_AC_Voltage_CA = 0;
        #endregion


        #region Property Meter_AC_Frequency of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Frequency of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Frequency.
        /// </value>
        [Category("Meter")]
        [DisplayName("AC Frequency")]
        [Description("AC Frequency in Hertz")]
        [TypeConverter(typeof(HertzTypeConverter))]
        [DataMember]
        [ModbusSource(40204 + MeterBaseOffset, 40205 + MeterBaseOffset)]
        public float Meter_AC_Frequency
        {
            get { return _Meter_AC_Frequency; }
            set
            {
                if (_Meter_AC_Frequency != value)
                {
                    _Meter_AC_Frequency = value;
                    OnPropertyChanged(nameof(Meter_AC_Frequency));
                }
            }
        }
        private float _Meter_AC_Frequency = 0;
        #endregion

        #region Property Meter_AC_Power of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_Power of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_Power.
        /// </value>
        [Category("Meter")]
        [DisplayName("Total Real Power (sum of active phases)")]
        [Description("Total Real Power (sum of active phases) in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40206 + MeterBaseOffset, 40210 + MeterBaseOffset)]
        public float Meter_AC_Power
        {
            get { return _Meter_AC_Power; }
            set
            {
                if (_Meter_AC_Power != value)
                {
                    _Meter_AC_Power = value;
                    OnPropertyChanged(nameof(Meter_AC_Power));
                }
            }
        }
        private float _Meter_AC_Power = 0;
        #endregion

        #region Property Meter_AC_PowerA of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PowerA of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PowerA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A AC Real Power")]
        [Description("Phase A AC Real Power in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40207 + MeterBaseOffset, 40210 + MeterBaseOffset)]
        public float Meter_AC_PowerA
        {
            get { return _Meter_AC_PowerA; }
            set
            {
                if (_Meter_AC_PowerA != value)
                {
                    _Meter_AC_PowerA = value;
                    OnPropertyChanged(nameof(Meter_AC_PowerA));
                }
            }
        }
        private float _Meter_AC_PowerA = 0;
        #endregion

        #region Property Meter_AC_PowerB of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PowerA of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PowerA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B AC Real Power")]
        [Description("Phase B AC Real Power in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40208 + MeterBaseOffset, 40210 + MeterBaseOffset)]
        public float Meter_AC_PowerB
        {
            get { return _Meter_AC_PowerB; }
            set
            {
                if (_Meter_AC_PowerB != value)
                {
                    _Meter_AC_PowerB = value;
                    OnPropertyChanged(nameof(Meter_AC_PowerB));
                }
            }
        }
        private float _Meter_AC_PowerB = 0;
        #endregion

        #region Property Meter_AC_PowerC of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PowerA of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PowerA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C AC Real Power")]
        [Description("Phase C AC Real Power in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [DataMember]
        [ModbusSource(40209 + MeterBaseOffset, 40210 + MeterBaseOffset)]
        public float Meter_AC_PowerC
        {
            get { return _Meter_AC_PowerC; }
            set
            {
                if (_Meter_AC_PowerC != value)
                {
                    _Meter_AC_PowerC = value;
                    OnPropertyChanged(nameof(Meter_AC_PowerC));
                }
            }
        }
        private float _Meter_AC_PowerC = 0;
        #endregion

        #region Property Meter_AC_VA of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VA of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VA.
        /// </value>
        [Category("Meter")]
        [DisplayName("Total AC Apparent Power (sum of active phases)")]
        [Description("Total AC Apparent Power (sum of active phases) in Volt-Amps")]
        [TypeConverter(typeof(VoltAmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40211 + MeterBaseOffset, 40215 + MeterBaseOffset)]
        public float Meter_AC_VA
        {
            get { return _Meter_AC_VA; }
            set
            {
                if (_Meter_AC_VA != value)
                {
                    _Meter_AC_VA = value;
                    OnPropertyChanged(nameof(Meter_AC_VA));
                }
            }
        }
        private float _Meter_AC_VA = 0;
        #endregion

        #region Property Meter_AC_VA_A of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VA_A of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VA_A.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A AC Apparent Power")]
        [Description("Phase A AC Apparent Power in Volt-Amps")]
        [TypeConverter(typeof(VoltAmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40212 + MeterBaseOffset, 40215 + MeterBaseOffset)]
        public float Meter_AC_VA_A
        {
            get { return _Meter_AC_VA_A; }
            set
            {
                if (_Meter_AC_VA_A != value)
                {
                    _Meter_AC_VA_A = value;
                    OnPropertyChanged(nameof(Meter_AC_VA_A));
                }
            }
        }
        private float _Meter_AC_VA_A = 0;
        #endregion

        #region Property Meter_AC_VA_B of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VA_B of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VA_B.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B AC Apparent Power")]
        [Description("Phase B AC Apparent Power in Volt-Amps")]
        [TypeConverter(typeof(VoltAmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40213 + MeterBaseOffset, 40215 + MeterBaseOffset)]
        public float Meter_AC_VA_B
        {
            get { return _Meter_AC_VA_B; }
            set
            {
                if (_Meter_AC_VA_B != value)
                {
                    _Meter_AC_VA_B = value;
                    OnPropertyChanged(nameof(Meter_AC_VA_B));
                }
            }
        }
        private float _Meter_AC_VA_B = 0;
        #endregion

        #region Property Meter_AC_VA_C of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VA_C of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VA_C.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C AC Apparent Power")]
        [Description("Phase C AC Apparent Power in Volt-Amps")]
        [TypeConverter(typeof(VoltAmpsTypeConverter))]
        [DataMember]
        [ModbusSource(40214 + MeterBaseOffset, 40215 + MeterBaseOffset)]
        public float Meter_AC_VA_C
        {
            get { return _Meter_AC_VA_C; }
            set
            {
                if (_Meter_AC_VA_C != value)
                {
                    _Meter_AC_VA_C = value;
                    OnPropertyChanged(nameof(Meter_AC_VA_C));
                }
            }
        }
        private float _Meter_AC_VA_C = 0;
        #endregion



        #region Property Meter_AC_VAR of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VAR of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VAR.
        /// </value>
        [Category("Meter")]
        [DisplayName("Total AC Reactive Power(sum of active phases)")]
        [Description("Total AC Reactive Power(sum of active phases) in VAR")]
        [TypeConverter(typeof(VoltAmpsReactiveTypeConverter))]
        [DataMember]
        [ModbusSource(40216 + MeterBaseOffset, 40220 + MeterBaseOffset)]
        public float Meter_AC_VAR
        {
            get { return _Meter_AC_VAR; }
            set
            {
                if (_Meter_AC_VAR != value)
                {
                    _Meter_AC_VAR = value;
                    OnPropertyChanged(nameof(Meter_AC_VAR));
                }
            }
        }
        private float _Meter_AC_VAR = 0;
        #endregion

        #region Property Meter_AC_VAR_A of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VAR_A of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VAR_A.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A AC Reactive Power")]
        [Description("Phase A AC Reactive Power  in VAR")]
        [TypeConverter(typeof(VoltAmpsReactiveTypeConverter))]
        [DataMember]
        [ModbusSource(40217 + MeterBaseOffset, 40220 + MeterBaseOffset)]
        public float Meter_AC_VAR_A
        {
            get { return _Meter_AC_VAR_A; }
            set
            {
                if (_Meter_AC_VAR_A != value)
                {
                    _Meter_AC_VAR_A = value;
                    OnPropertyChanged(nameof(Meter_AC_VAR_A));
                }
            }
        }
        private float _Meter_AC_VAR_A = 0;
        #endregion

        #region Property Meter_AC_VAR_B of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VAR_B of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VAR_B.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B AC Reactive Power")]
        [Description("Phase B AC Reactive Power  in VAR")]
        [TypeConverter(typeof(VoltAmpsReactiveTypeConverter))]
        [DataMember]
        [ModbusSource(40218 + MeterBaseOffset, 40220 + MeterBaseOffset)]
        public float Meter_AC_VAR_B
        {
            get { return _Meter_AC_VAR_B; }
            set
            {
                if (_Meter_AC_VAR_B != value)
                {
                    _Meter_AC_VAR_B = value;
                    OnPropertyChanged(nameof(Meter_AC_VAR_B));
                }
            }
        }
        private float _Meter_AC_VAR_B = 0;
        #endregion

        #region Property Meter_AC_VAR_C of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_VAR_C of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_VAR_C.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C AC Reactive Power")]
        [Description("Phase C AC Reactive Power  in VAR")]
        [TypeConverter(typeof(VoltAmpsReactiveTypeConverter))]
        [DataMember]
        [ModbusSource(40219 + MeterBaseOffset, 40220 + MeterBaseOffset)]
        public float Meter_AC_VAR_C
        {
            get { return _Meter_AC_VAR_C; }
            set
            {
                if (_Meter_AC_VAR_C != value)
                {
                    _Meter_AC_VAR_C = value;
                    OnPropertyChanged(nameof(Meter_AC_VAR_C));
                }
            }
        }
        private float _Meter_AC_VAR_C = 0;
        #endregion

        #region Property Meter_AC_PF of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PF of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PF.
        /// </value>
        [Category("Meter")]
        [DisplayName("Average Power Factor (average of active phases)")]
        [Description("Average Power Factor (average of active phases) in %")]
        [TypeConverter(typeof(PercentTypeConverter))]
        [DataMember]
        [ModbusSource(40221 + MeterBaseOffset, 40225 + MeterBaseOffset)]
        public float Meter_AC_PF
        {
            get { return _Meter_AC_PF; }
            set
            {
                if (_Meter_AC_PF != value)
                {
                    _Meter_AC_PF = value;
                    OnPropertyChanged(nameof(Meter_AC_PF));
                }
            }
        }
        private float _Meter_AC_PF = 0;
        #endregion

        #region Property Meter_AC_PF_A of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PF_A of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PF_A.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A Power Factor")]
        [Description("Phase A Power Factor in %")]
        [TypeConverter(typeof(PercentTypeConverter))]
        [DataMember]
        [ModbusSource(40222 + MeterBaseOffset, 40225 + MeterBaseOffset)]
        public float Meter_AC_PF_A
        {
            get { return _Meter_AC_PF_A; }
            set
            {
                if (_Meter_AC_PF_A != value)
                {
                    _Meter_AC_PF_A = value;
                    OnPropertyChanged(nameof(Meter_AC_PF_A));
                }
            }
        }
        private float _Meter_AC_PF_A = 0;
        #endregion

        #region Property Meter_AC_PF_B of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PF_B of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PF_B.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B Power Factor")]
        [Description("Phase B Power Factor in %")]
        [TypeConverter(typeof(PercentTypeConverter))]
        [DataMember]
        [ModbusSource(40223 + MeterBaseOffset, 40225 + MeterBaseOffset)]
        public float Meter_AC_PF_B
        {
            get { return _Meter_AC_PF_B; }
            set
            {
                if (_Meter_AC_PF_B != value)
                {
                    _Meter_AC_PF_B = value;
                    OnPropertyChanged(nameof(Meter_AC_PF_B));
                }
            }
        }
        private float _Meter_AC_PF_B = 0;
        #endregion

        #region Property Meter_AC_PF_C of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_AC_PF_C of type float
        /// </summary>
        /// <value>
        /// The Meter_AC_PF_C.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C Power Factor")]
        [Description("Phase C Power Factor in %")]
        [TypeConverter(typeof(PercentTypeConverter))]
        [DataMember]
        [ModbusSource(40224 + MeterBaseOffset, 40225 + MeterBaseOffset)]
        public float Meter_AC_PF_C
        {
            get { return _Meter_AC_PF_C; }
            set
            {
                if (_Meter_AC_PF_C != value)
                {
                    _Meter_AC_PF_C = value;
                    OnPropertyChanged(nameof(Meter_AC_PF_C));
                }
            }
        }
        private float _Meter_AC_PF_C = 0;
        #endregion

        #region Property Meter_Exported of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Exported of type float
        /// </summary>
        /// <value>
        /// The Meter_Exported.
        /// </value>
        [Category("Meter")]
        [DisplayName("Total Exported Real Energy")]
        [Description("Total Exported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40226 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Exported
        {
            get { return _Meter_Exported; }
            set
            {
                if (_Meter_Exported != value)
                {
                    _Meter_Exported = value;
                    OnPropertyChanged(nameof(Meter_Exported));
                }
            }
        }
        private float _Meter_Exported = 0;
        #endregion

        #region Property Meter_Exported_A of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Exported_A of type float
        /// </summary>
        /// <value>
        /// The Meter_Exported_A.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A Exported Real Energy")]
        [Description("Phase A Exported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40228 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Exported_A
        {
            get { return _Meter_Exported_A; }
            set
            {
                if (_Meter_Exported_A != value)
                {
                    _Meter_Exported_A = value;
                    OnPropertyChanged(nameof(Meter_Exported_A));
                }
            }
        }
        private float _Meter_Exported_A = 0;
        #endregion

        #region Property Meter_Exported_B of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Exported_B of type float
        /// </summary>
        /// <value>
        /// The Meter_Exported_B.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B Exported Real Energy")]
        [Description("Phase B Exported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40230 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Exported_B
        {
            get { return _Meter_Exported_B; }
            set
            {
                if (_Meter_Exported_B != value)
                {
                    _Meter_Exported_B = value;
                    OnPropertyChanged(nameof(Meter_Exported_B));
                }
            }
        }
        private float _Meter_Exported_B = 0;
        #endregion

        #region Property Meter_Exported_C of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Exported_C of type float
        /// </summary>
        /// <value>
        /// The Meter_Exported_C.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C Exported Real Energy")]
        [Description("Phase C Exported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40232 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Exported_C
        {
            get { return _Meter_Exported_C; }
            set
            {
                if (_Meter_Exported_C != value)
                {
                    _Meter_Exported_C = value;
                    OnPropertyChanged(nameof(Meter_Exported_C));
                }
            }
        }
        private float _Meter_Exported_C = 0;
        #endregion

        #region Property Meter_Imported of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Imported of type float
        /// </summary>
        /// <value>
        /// The Meter_Imported.
        /// </value>
        [Category("Meter")]
        [DisplayName("Total Imported Real Energy")]
        [Description("Total Imported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40234 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Imported
        {
            get { return _Meter_Imported; }
            set
            {
                if (_Meter_Imported != value)
                {
                    _Meter_Imported = value;
                    OnPropertyChanged(nameof(Meter_Imported));
                }
            }
        }
        private float _Meter_Imported = 0;
        #endregion

        #region Property Meter_Imported_A of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Imported_A of type float
        /// </summary>
        /// <value>
        /// The Meter_Imported_A.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase A Imported Real Energy")]
        [Description("Phase A Imported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40236 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Imported_A
        {
            get { return _Meter_Imported_A; }
            set
            {
                if (_Meter_Imported_A != value)
                {
                    _Meter_Imported_A = value;
                    OnPropertyChanged(nameof(Meter_Imported_A));
                }
            }
        }
        private float _Meter_Imported_A = 0;
        #endregion

        #region Property Meter_Imported_B of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Imported_B of type float
        /// </summary>
        /// <value>
        /// The Meter_Imported_B.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase B Imported Real Energy")]
        [Description("Phase B Imported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40238 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Imported_B
        {
            get { return _Meter_Imported_B; }
            set
            {
                if (_Meter_Imported_B != value)
                {
                    _Meter_Imported_B = value;
                    OnPropertyChanged(nameof(Meter_Imported_B));
                }
            }
        }
        private float _Meter_Imported_B = 0;
        #endregion

        #region Property Meter_Imported_C of type float with property changed event
        /// <summary>
        /// Gets or sets the Meter_Imported_C of type float
        /// </summary>
        /// <value>
        /// The Meter_Imported_C.
        /// </value>
        [Category("Meter")]
        [DisplayName("Phase C Imported Real Energy")]
        [Description("Phase C Imported Real Energy in Watt-hours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [DataMember]
        [ModbusSource(40240 + MeterBaseOffset, 40242 + MeterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32)]
        public float Meter_Imported_C
        {
            get { return _Meter_Imported_C; }
            set
            {
                if (_Meter_Imported_C != value)
                {
                    _Meter_Imported_C = value;
                    OnPropertyChanged(nameof(Meter_Imported_C));
                }
            }
        }
        private float _Meter_Imported_C = 0;
        #endregion 
        #endregion

    }
}
