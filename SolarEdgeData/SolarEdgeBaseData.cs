using SolarEdgeData.Attributes;
using SolarEdgeData.TypeConverters;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SolarEdgeData
{
    [DataContract]
    public class SolarEdgeBaseData : INotifyPropertyChanged
    {
        const int MeterBaseOffset = 0;
        const int InverterBaseOffset = 0;

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


        #region Inverter AC Power

        #region Property AC_Power of type float with property changed event
        /// <summary>
        /// Gets or sets the ProductionWatts of type float
        /// </summary>
        /// <value>
        /// The ProductionWatts.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Current energy production")]
        [Description("Current energy production in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [ModbusSource(40084 + InverterBaseOffset, 40085 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.int16)]
        [DataMember]
        public float CurrentEnergyProduction
        {
            get { return _CurrentEnergyProduction; }
            set
            {
                if (_CurrentEnergyProduction != value)
                {
                    _CurrentEnergyProduction = value;
                    OnPropertyChanged(nameof(CurrentEnergyProduction));
                }
            }
        }
        private float _CurrentEnergyProduction = -1;
        #endregion

        #endregion




        #region Property LifeTimeEnergyProduction of type float with property changed event
        /// <summary>
        /// Gets or sets the LifeTimeEnergyProduction of type float
        /// </summary>
        /// <value>
        /// The LifeTimeEnergyProduction.
        /// </value>
        [Category("Inverter")]
        [DisplayName("Lifetime energy production")]
        [Description("Lifetime energy production in WattHours")]
        [TypeConverter(typeof(WattHoursTypeConverter))]
        [ModbusSource(40094 + InverterBaseOffset, 40096 + InverterBaseOffset, ValueSourceType = ModbusSourceTypeEnum.uint32,ScaleFactorSourceType =ModbusSourceTypeEnum.uint16)]
        [DataMember]
        public float LifeTimeEnergyProduction
        {
            get { return _LifeTimeEnergyProduction; }
            set
            {
                if (_LifeTimeEnergyProduction != value)
                {
                    _LifeTimeEnergyProduction = value;
                    OnPropertyChanged(nameof(LifeTimeEnergyProduction));
                }
            }
        }
        private float _LifeTimeEnergyProduction = 0;
        #endregion



        #endregion


        #region Meter
        #region Property CurrentExportedImportedEnergy of type float with property changed event
        /// <summary>
        /// Gets or sets the CurrentExportedImportedEnergy of type float
        /// </summary>
        /// <value>
        /// The CurrentExportedImportedEnergy.
        /// </value>
        [Category("Meter")]
        [DisplayName("Current exported/imported energy")]
        [Description("Current exported (positive values) resp. imported (negative values) energy in Watts")]
        [TypeConverter(typeof(WattsTypeConverter))]
        [ModbusSource(40206 + MeterBaseOffset, 40210 + MeterBaseOffset)]
        [DataMember]
        public float CurrentExportedImportedEnergy
        {
            get { return _CurrentExportedImportedEnergy; }
            set
            {
                if (_CurrentExportedImportedEnergy != value)
                {
                    _CurrentExportedImportedEnergy = value;
                    OnPropertyChanged(nameof(CurrentExportedImportedEnergy));
                }
            }
        }
        private float _CurrentExportedImportedEnergy = 0;
        #endregion



        #endregion




    }
}
