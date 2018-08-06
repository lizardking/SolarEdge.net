using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SolarEdgeData;
using SolarEdgeDataFetcher;

namespace SolarEdgeService.Communication
{
    /// <summary>
    /// Implementation of the ISolarEdgeDataServer methods for the WCF service
    /// </summary>
    /// <seealso cref="SolarEdgeService.Communication.ISolarEdgeDataServer" />
    public partial class SolarEdgeWCFService : ISolarEdgeDataServer
    {

        private static readonly object _sycnRoot = new object();


        /// <summary>
        /// Gets SolarEdgeFullData
        /// </summary>
        /// <returns>
        /// SolarEdgeFullData
        /// </returns>
        public SolarEdgeFullData GetSolarEdgeFullData()
        {
            return DataFetcher.Instance.SolarEdgeFullData;
        }

        /// <summary>
        /// Gets SolarEdgeBaseData.
        /// </summary>
        /// <returns>
        /// SolarEdgeBaseData
        /// </returns>
        public SolarEdgeBaseData GetSolarEdgeBaseData()
        {
            return DataFetcher.Instance.SolarEdgeBaseData;
        }

        /// <summary>
        /// Gets SolarEdgeDataIsValid property
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the data is valid, otherwise <c>false</c>
        /// </returns>
        public bool GetSolarEdgeDataIsValid()
        {
            return DataFetcher.Instance.SolarEdgeDataIsValid;
        }

        private static List<ISolarEdgeDataServerEvents> _FullUpdateCallbackChannels = new List<ISolarEdgeDataServerEvents>();


        /// <summary>
        /// Subscribes for SolarEdgeFullData updates.
        /// </summary>
        public void SubscribeForSolarEdgeFullDataUpdates()
        {
            try
            {
                ISolarEdgeDataServerEvents callbackChannel = OperationContext.Current.GetCallbackChannel<ISolarEdgeDataServerEvents>();

                lock (_sycnRoot)
                {
                    if (!_FullUpdateCallbackChannels.Contains(callbackChannel))
                    {
                        _FullUpdateCallbackChannels.Add(callbackChannel);

                        log.DebugFormat("Added Callback Channel for full data updates: {0}", callbackChannel.GetHashCode());

                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Unsubscribes from SolarEdgeFullData updates.
        /// </summary>
        public void UnsubscribeFromSolarEdgeFullDataUpdates()
        {
            ISolarEdgeDataServerEvents callbackChannel = OperationContext.Current.GetCallbackChannel<ISolarEdgeDataServerEvents>();

            try
            {
                lock (_sycnRoot)
                {
                    if (_FullUpdateCallbackChannels.Remove(callbackChannel))
                    {
                        log.DebugFormat("Removed Callback Channel for full data updates: {0}", callbackChannel.GetHashCode());
                    }
                }
            }
            catch
            {
            }

        }



        /// <summary>
        /// Sends the full data updates.
        /// </summary>
        public static void TransmitFullDataUpdates()
        {
            lock (_sycnRoot)
            {

                SolarEdgeFullData SED = DataFetcher.Instance.SolarEdgeFullData;

                for (int i = _FullUpdateCallbackChannels.Count - 1; i >= 0; i--)
                {
                    if (((ICommunicationObject)_FullUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                    {
                        log.DebugFormat("Detected Non-Open Callback Channel for full data updates: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                        _FullUpdateCallbackChannels.RemoveAt(i);
                        continue;
                    }

                    try
                    {
                        _FullUpdateCallbackChannels[i].TransmitFullDataUpdate(SED);
                        log.DebugFormat("Pushed full data update on Callback Channel: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                    }
                    catch (Exception ex)
                    {
                        log.Debug("Service threw exception while communicating on Callback Channel for full data updates: {0}".Build(_FullUpdateCallbackChannels[i].GetHashCode()), ex);
                        _FullUpdateCallbackChannels.RemoveAt(i);
                    }
                }
            }
        }


        private static List<ISolarEdgeDataServerEvents> _BaseUpdateCallbackChannels = new List<ISolarEdgeDataServerEvents>();


        /// <summary>
        /// Subscribes for SolarEdgeBaseData updates.
        /// </summary>
        public void SubscribeForSolarEdgeBaseDataUpdates()
        {
            try
            {
                ISolarEdgeDataServerEvents callbackChannel = OperationContext.Current.GetCallbackChannel<ISolarEdgeDataServerEvents>();

                lock (_sycnRoot)
                {
                    if (!_BaseUpdateCallbackChannels.Contains(callbackChannel))
                    {
                        _BaseUpdateCallbackChannels.Add(callbackChannel);

                        log.DebugFormat("Added Callback Channel for base data updates: {0}", callbackChannel.GetHashCode());

                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Unsubscribes from SolarEdgeBaseData updates.
        /// </summary>
        public void UnsubscribeFromSolarEdgeBaseDataUpdates()
        {
            ISolarEdgeDataServerEvents callbackChannel = OperationContext.Current.GetCallbackChannel<ISolarEdgeDataServerEvents>();

            try
            {
                lock (_sycnRoot)
                {
                    if (_BaseUpdateCallbackChannels.Remove(callbackChannel))
                    {
                        log.DebugFormat("Removed Callback Channel for base data updates: {0}", callbackChannel.GetHashCode());
                    }
                }
            }
            catch
            {
            }

        }



        /// <summary>
        /// Sends the base updates.
        /// </summary>
        public static void TransmitBaseUpdates()
        {
            lock (_sycnRoot)
            {

                SolarEdgeBaseData SED = DataFetcher.Instance.SolarEdgeBaseData;

                for (int i = _BaseUpdateCallbackChannels.Count - 1; i >= 0; i--)
                {

                    if (((ICommunicationObject)_BaseUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                    {
                        log.DebugFormat("Detected Non-Open Callback Channel for base updates: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                        _BaseUpdateCallbackChannels.RemoveAt(i);
                        continue;
                    }

                    try
                    {
                        _BaseUpdateCallbackChannels[i].TransmitBaseDataUpdate(SED);
                        log.DebugFormat("Pushed base data update on Callback Channel: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                    }
                    catch (Exception ex)
                    {
                        log.Debug("Service threw exception while communicating on Callback Channel for base data updates: {0}".Build(_BaseUpdateCallbackChannels[i].GetHashCode()), ex);
                        _BaseUpdateCallbackChannels.RemoveAt(i);
                    }
                }
            }
        }



        /// <summary>
        /// Sends the DataIsValid updates.
        /// </summary>
        /// <param name="DataIsValid">if set to <c>true</c> [data is valid].</param>
        public static void TransmitDataIsValidUpdate(bool DataIsValid)
        {
            for (int i = _FullUpdateCallbackChannels.Count - 1; i >= 0; i--)
            {
                if (((ICommunicationObject)_FullUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                {
                    log.DebugFormat("Detected Non-Open Callback Channel for full data updates: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                    _FullUpdateCallbackChannels.RemoveAt(i);
                    continue;
                }

                try
                {
                    _FullUpdateCallbackChannels[i].TransmitDataIsValidUpdate(DataIsValid);
                    log.DebugFormat("Pushed DataIsValidUpdate on Callback Channel: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                }
                catch (Exception ex)
                {
                    log.Debug("Service threw exception while sending DataIsValidUpdate on Callback Channel for full data updates: {0}".Build(_FullUpdateCallbackChannels[i].GetHashCode()), ex);
                    _FullUpdateCallbackChannels.RemoveAt(i);
                }
            }

            for (int i = _BaseUpdateCallbackChannels.Count - 1; i >= 0; i--)
            {
                if (((ICommunicationObject)_BaseUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                {
                    log.DebugFormat("Detected Non-Open Callback Channel for base data updates: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                    _BaseUpdateCallbackChannels.RemoveAt(i);
                    continue;
                }

                try
                {
                    _BaseUpdateCallbackChannels[i].TransmitDataIsValidUpdate(DataIsValid);
                    log.DebugFormat("Pushed DataIsValidUpdate on Callback Channel: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                }
                catch (Exception ex)
                {
                    log.Debug("Service threw exception while sending DataIsValidUpdate on Callback Channel for base data updates: {0}".Build(_BaseUpdateCallbackChannels[i].GetHashCode()), ex);
                    _BaseUpdateCallbackChannels.RemoveAt(i);
                }
            }

        }


        public static void TransmitHeartbeat()
        {
            for (int i = _FullUpdateCallbackChannels.Count - 1; i >= 0; i--)
            {
                if (((ICommunicationObject)_FullUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                {
                    log.DebugFormat("Detected Non-Open Callback Channel for full data updates: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                    _FullUpdateCallbackChannels.RemoveAt(i);
                    continue;
                }

                try
                {
                    _FullUpdateCallbackChannels[i].TransmitHeartbeat();
                    log.DebugFormat("Pushed DataIsValidUpdate on Callback Channel: {0}", _FullUpdateCallbackChannels[i].GetHashCode());
                }
                catch (Exception ex)
                {
                    log.Debug("Service threw exception while sending DataIsValidUpdate on Callback Channel for full data updates: {0}".Build(_FullUpdateCallbackChannels[i].GetHashCode()), ex);
                    _FullUpdateCallbackChannels.RemoveAt(i);
                }
            }

            for (int i = _BaseUpdateCallbackChannels.Count - 1; i >= 0; i--)
            {
                if (((ICommunicationObject)_BaseUpdateCallbackChannels[i]).State != CommunicationState.Opened)
                {
                    log.DebugFormat("Detected Non-Open Callback Channel for base data updates: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                    _BaseUpdateCallbackChannels.RemoveAt(i);
                    continue;
                }

                try
                {
                    _FullUpdateCallbackChannels[i].TransmitHeartbeat();
                    log.DebugFormat("Pushed DataIsValidUpdate on Callback Channel: {0}", _BaseUpdateCallbackChannels[i].GetHashCode());
                }
                catch (Exception ex)
                {
                    log.Debug("Service threw exception while sending DataIsValidUpdate on Callback Channel for base data updates: {0}".Build(_BaseUpdateCallbackChannels[i].GetHashCode()), ex);
                    _BaseUpdateCallbackChannels.RemoveAt(i);
                }
            }
        }


    }




}
