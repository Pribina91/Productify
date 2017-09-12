using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Meninx.Productify.Service.Contracts
{
    [DataContract]
    public class AttributeContract
    {
        public AttributeContract()
        {
        }

        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public int AttributeTypeId { get; set; }

        [DataMember]
        public string AttributeTypeName { get; set; }
    }
}