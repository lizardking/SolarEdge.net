using SolarEdgeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace SolarEdgeService.Communication
{
    /// <summary>
    /// Service contract for the WCV services
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ISolarEdgeDataServerEvents))]
    public interface ISolarEdgeDataServer
    {
        /// <summary>
        /// Gets SolarEdgeFullData
        /// </summary>
        /// <returns>SolarEdgeFullData</returns>
        [OperationContract()]
        SolarEdgeFullData GetSolarEdgeFullData();

        /// <summary>
        /// Gets SolarEdgeBaseData.
        /// </summary>
        /// <returns>SolarEdgeBaseData</returns>
        [OperationContract()]
        SolarEdgeBaseData GetSolarEdgeBaseData();

        /// <summary>
        /// Gets SolarEdgeDataIsValid property
        /// </summary>
        /// <returns><c>true</c> if the data is valid, otherwise <c>false</c></returns>
        [OperationContract()]
        bool GetSolarEdgeDataIsValid();


        /// <summary>
        /// Subscribes for SolarEdgeFullData updates.
        /// </summary>
        [OperationContract()]
        void SubscribeForSolarEdgeFullDataUpdates();

        /// <summary>
        /// Unsubscribes from SolarEdgeFullData updates.
        /// </summary>
        [OperationContract()]
        void UnsubscribeFromSolarEdgeFullDataUpdates();

        /// <summary>
        /// Subscribes for SolarEdgeBaseData updates.
        /// </summary>
        [OperationContract()]
        void SubscribeForSolarEdgeBaseDataUpdates();

        /// <summary>
        /// Unsubscribes from SolarEdgeBaseData updates.
        /// </summary>
        [OperationContract()]
        void UnsubscribeFromSolarEdgeBaseDataUpdates();
    }
}
