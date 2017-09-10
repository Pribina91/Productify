using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Models;


namespace Meninx.Productify.Data.Context
{
    class ProductifyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Attribute> Atrributes { get; set; }
        public DbSet<StringAtrribute> StringAtrributes { get; set; }
        public DbSet<IntegerAtrribute> IntegerAtrributes { get; set; }
        public DbSet<DatetimeAtrribute> DatetimeAtrributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
    }
}