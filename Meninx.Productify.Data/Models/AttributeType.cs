using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Meninx.Productify.Data.Models.Enums;

namespace Meninx.Productify.Data.Models
{
    public class AttributeType : BaseEntity
    {
        public string Name { get; set; }

        public AttributeTypeDataType DataType { get; set; }

        [NotMapped]
        public Type Type
        {
            get
            {
                switch (DataType)
                {
                    case AttributeTypeDataType.StringValue:
                        return typeof(string);
                    case AttributeTypeDataType.IntegerValue:
                        return typeof(int);
                    case AttributeTypeDataType.DatetimeValue:
                        return typeof(DateTime);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}