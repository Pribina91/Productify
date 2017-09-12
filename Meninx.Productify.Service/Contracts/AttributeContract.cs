using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Meninx.Productify.Service.Contracts
{
    public class AttributeContract
    {
        [DataMember]
        public int Id { get; set; }

        public string Value { get; set; }
        public int ProductId { get; set; }
        public int AttributeTypeId { get; set; }
    }
}