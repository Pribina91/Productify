using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Meninx.Productify.Service.Contracts
{
    [DataContract]
    public class AttributeTypeContract
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int DataTypeId { get; set; }

        [DataMember]
        public string DataTypeName { get; set; }
    }
}