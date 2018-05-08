using System;
using System.ServiceModel;

namespace SolarEdgeService.Communication
{
    /// <summary>
    /// Implementation of the WCF Service contract
    /// </summary>
    /// <seealso cref="SolarEdgeService.Communication.ISolarEdgeDataServer" />
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class SolarEdgeWCFService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FaultException<ServiceFaultContract> GetDefaultFaultException(Exception E = null)
        {
            ServiceFaultContract FC = new ServiceFaultContract();
            if (E == null)
            {
                FC.Message = "Unspecified error";
            }
            else
            {
                FC.Message = E.Message;
            }
            return new FaultException<ServiceFaultContract>(FC);
        }
    }
}