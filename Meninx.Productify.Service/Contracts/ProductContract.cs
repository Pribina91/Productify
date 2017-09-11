using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Meninx.Productify.Service.Contracts
{
    [DataContract]
    public class ProductContract
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Price { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Code { get; set; }

        //public List<AttributeContract> Attributes { get; set; }
    }
}