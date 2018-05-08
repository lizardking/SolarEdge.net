using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SolarEdgeService.Communication
{
    /// <summary>
    /// Fault contract for the WCF service
    /// </summary>
    [DataContract]
    public class ServiceFaultContract
    {
        [DataMember]
        public string Message { get; set; }
    }
}
