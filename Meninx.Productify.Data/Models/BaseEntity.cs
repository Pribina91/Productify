using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Meninx.Productify.Data.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}