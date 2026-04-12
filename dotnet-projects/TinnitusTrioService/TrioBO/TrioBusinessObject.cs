using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TrioBO
{
    [DataContract]
    public class TrioBusinessObject
    {
        [DataMember]
        public string UniqueId { get; set; }
    }

     [DataContract]
    public class TrioUseridObject: TrioBusinessObject
    {
        [DataMember]
         public string Macid { get; set; }
        [DataMember]
        public string SerialNo { get; set; }
        [DataMember]
        public string UniqueString { get; set; }
    }
}
