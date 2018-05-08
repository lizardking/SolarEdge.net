using SolarEdgeData.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace SolarEdgeDataFetcher
{
    /// <summary>
    /// Internal class used to update the property values of objects from the data received from the ModBus.
    /// </summary>
    internal class ModbusDataUpdater
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Updates the value of properties marked with a ModbusSourceAttribute from a array of data (typically received from modbus).
        /// </summary>
        /// <param name="ModbusDataArray">The modbus data array.</param>
        /// <param name="ArrayOffset">The array offset.</param>
        /// <param name="ObjectToUpdate">The object to update.</param>
        public void UpdateData(int[] ModbusDataArray, int ArrayOffset, object ObjectToUpdate)
        {
            List<Tuple<PropertyInfo, ModbusSourceAttribute>> l = GetUpdateableProperties(ObjectToUpdate);
            foreach (Tuple<PropertyInfo, ModbusSourceAttribute> item in l)
            {
                object Value = GetModbusValue(ModbusDataArray, ArrayOffset, item.Item2);
                if (Value != null)
                {
                    item.Item1.SetValue(ObjectToUpdate, Convert.ChangeType(Value, item.Item1.PropertyType));
                }
            }
        }

        /// <summary>
        /// Gets the modbus value.
        /// </summary>
        /// <param name="ModbusDataArray">The array of data received from the modbus.</param>
        /// <param name="ArrayOffset">The array offset.</param>
        /// <param name="ModbusSource">The modbus source attribute.</param>
        /// <returns>Value taken from the data array or null if the data specified in the ModbusSource does not exist in the data array.</returns>
        /// <exception cref="Exception">
        /// Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ValueSourceType} for {nameof(ModbusSource.ValueSourceType)}
        /// or
        /// Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ValueSourceType} for {nameof(ModbusSource.ValueSourceType)}
        /// </exception>
        private object GetModbusValue(int[] ModbusDataArray, int ArrayOffset, ModbusSourceAttribute ModbusSource)
        {
            if ((ModbusSource.ValueOffset - ArrayOffset) < 0 || (ModbusSource.ValueOffset - ArrayOffset) >= ModbusDataArray.Length)
            {
                return null;
            }

            double NumericValue;
 
            switch (ModbusSource.ValueSourceType)
            {
                case ModbusSourceTypeEnum.int16:
                    NumericValue = ModbusDataArray[ModbusSource.ValueOffset - ArrayOffset];
                    break;
                case ModbusSourceTypeEnum.uint32:
                    NumericValue = (BitConverter.ToUInt16(BitConverter.GetBytes(ModbusDataArray[ModbusSource.ValueOffset - ArrayOffset]), 0) * 65536) + (BitConverter.ToUInt16(BitConverter.GetBytes(ModbusDataArray[ModbusSource.ValueOffset - ArrayOffset + 1]), 0));
                    break;
                case ModbusSourceTypeEnum.uint16:
                    NumericValue = BitConverter.ToUInt16(BitConverter.GetBytes(ModbusDataArray[ModbusSource.ValueOffset - ArrayOffset]), 0);
                    break;
                default:
                    log.Error($"Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ValueSourceType} for {nameof(ModbusSource.ValueSourceType)}");
                    throw new Exception($"Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ValueSourceType} for {nameof(ModbusSource.ValueSourceType)}");
            }



            if (!ModbusSource.ScaleFactorOffset.HasValue)
            {
                return NumericValue;
            }

            if ((ModbusSource.ScaleFactorOffset.Value - ArrayOffset) < 0 || (ModbusSource.ScaleFactorOffset.Value - ArrayOffset) >= ModbusDataArray.Length)
            {
                return null;
            }


            double NumericScale;
            switch (ModbusSource.ScaleFactorSourceType)
            {
                case ModbusSourceTypeEnum.int16:
                    NumericScale = ModbusDataArray[ModbusSource.ScaleFactorOffset.Value - ArrayOffset];
                    break;
                case ModbusSourceTypeEnum.uint32:
                    NumericScale = ((uint)(int)ModbusDataArray[ModbusSource.ScaleFactorOffset.Value - ArrayOffset]) * 65536 + ((uint)(int)ModbusDataArray[ModbusSource.ScaleFactorOffset.Value - ArrayOffset + 1]);
                    break;
                case ModbusSourceTypeEnum.uint16:
                    NumericScale = (UInt16)(int)ModbusDataArray[ModbusSource.ScaleFactorOffset.Value - ArrayOffset];
                    break;
                default:
                    log.Error($"Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ScaleFactorSourceType} for {nameof(ModbusSource.ScaleFactorSourceType)}");
                    throw new Exception($"Unknown {nameof(ModbusSourceTypeEnum)} {ModbusSource.ScaleFactorSourceType}");
            }

            return NumericValue * Math.Pow(10, NumericScale);
        }




        /// <summary>
        /// Gets the updateable properties of the object which has to be updated by the data updater.
        /// </summary>
        /// <param name="ObjectToUpdate">The object to update.</param>
        /// <returns>List of updateable properties (PropertyInfo, ModBusSourceAttribute)</returns>
        private List<Tuple<PropertyInfo, ModbusSourceAttribute>> GetUpdateableProperties(object ObjectToUpdate)
        {
            Type objectType = ObjectToUpdate.GetType();

            lock (updatablePropertiesDictionaryLocker)
            {
                if (!updatablePropertiesDictionary.TryGetValue(objectType, out List<Tuple<PropertyInfo, ModbusSourceAttribute>> updateablePropertiesList))
                {
                    updateablePropertiesList = new List<Tuple<PropertyInfo, ModbusSourceAttribute>>();

                    foreach (PropertyInfo propertyInfo in ObjectToUpdate.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(PI => PI.CanRead && PI.CanWrite))
                    {
                        ModbusSourceAttribute sa = propertyInfo.GetCustomAttribute<ModbusSourceAttribute>(true);
                        if (sa != null)
                        {
                            updateablePropertiesList.Add(new Tuple<PropertyInfo, ModbusSourceAttribute>(propertyInfo, sa));
                        }
                    }
                    updatablePropertiesDictionary.Add(objectType, updateablePropertiesList);
                    if(updateablePropertiesList.Count==0)
                    {
                        log.Debug($"Object of type {objectType.Name} has no updateable properties.");
                    }
                }
                return updateablePropertiesList;
            }
        }
        private static Dictionary<Type, List<Tuple<PropertyInfo, ModbusSourceAttribute>>> updatablePropertiesDictionary = new Dictionary<Type, List<Tuple<PropertyInfo, ModbusSourceAttribute>>>();
        private static object updatablePropertiesDictionaryLocker = new object();

    }
}
