using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Meninx.Productify.Data.Models
{
    public abstract class Attribute : BaseEntity
    {
        public abstract string GetStringedValue();


        [ForeignKey(nameof(AttributeType))]
        public int AttributeTypeId { get; set; }

        public virtual AttributeType AttributeType { get; set; }
        public virtual List<Product> Product { get; set; }
    }

    [Table(nameof(IntegerAtrribute))]
    public class IntegerAtrribute : Attribute
    {
        [Column("IntegerValue")]
        public int Value { get; set; }

        public override string GetStringedValue()
        {
            return this.Value.ToString();
        }
    }

    [Table(nameof(StringAtrribute))]
    public class StringAtrribute : Attribute
    {
        [Column("StringValue")]
        public string Value { get; set; }

        public override string GetStringedValue()
        {
            return this.Value;
        }
    }

    [Table(nameof(DatetimeAtrribute))]
    public class DatetimeAtrribute : Attribute
    {
        [Column("DatetimeValue")]
        public DateTime Value { get; set; }

        public override string GetStringedValue()
        {
            return this.Value.ToString(CultureInfo.CurrentCulture);
        }
    }
}