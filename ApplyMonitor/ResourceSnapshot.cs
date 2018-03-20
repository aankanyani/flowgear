using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ApplyMonitor
{
    [DataContract]
    [Serializable]
    public class ResourceSnapshot
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string InstanceKey { get; set; }

        [DataMember]
        public string ResourceName { get; set; }
      
        [DataMember]
        public double Value { get; set; }
    }
}
