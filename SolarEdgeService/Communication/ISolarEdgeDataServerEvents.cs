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
    /// Defines the callback methods for the subscribed updates functionality.
    /// </summary>
    [ServiceContract]
    public interface ISolarEdgeDataServerEvents
    {
        /// <summary>
        /// Sends SolarEdgeFullData.
        /// </summary>
        /// <param name="SolarEdgeFullData">The solar edge full data.</param>
        [OperationContract(IsOneWay = true)]
        void TransmitFullDataUpdate(SolarEdgeFullData SolarEdgeFullData);

        /// <summary>
        /// Sends SolarEdgeBaseData.
        /// </summary>
        /// <param name="SolarEdgeBaseData">The solar edge base data.</param>
        [OperationContract(IsOneWay = true)]
        void TransmitBaseDataUpdate(SolarEdgeBaseData SolarEdgeBaseData);

        /// <summary>
        /// Sends the value of the DataIsValid property.
        /// </summary>
        /// <param name="DataIsValid">if set to <c>true</c> [data is valid].</param>
        [OperationContract(IsOneWay = true)]
        void TransmitDataIsValidUpdate(bool DataIsValid);

        /// <summary>
        /// Sends a Haertbeat.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void TransmitHeartbeat();




    }
}
