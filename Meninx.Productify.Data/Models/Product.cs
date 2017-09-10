using System.Collections.Generic;
using Attribute = Meninx.Productify.Data.Models.Attribute;

namespace Meninx.Productify.Data.Models
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public virtual List<Attribute> Attributes { get; set; }
    }
}