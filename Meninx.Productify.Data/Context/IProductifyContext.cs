using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Data.Context
{
    public abstract class IProductifyContext : DbContext
    {
        public IProductifyContext() { }
        public IProductifyContext(string contextName):base(contextName)
        {
            
        }

        public abstract DbSet<Product> Products { get; set; }
        public abstract DbSet<Attribute> Atrributes { get; set; }
        public abstract DbSet<StringAtrribute> StringAtrributes { get; set; }
        public abstract DbSet<IntegerAtrribute> IntegerAtrributes { get; set; }
        public abstract DbSet<DatetimeAtrribute> DatetimeAtrributes { get; set; }
        public abstract DbSet<AttributeType> AttributeTypes { get; set; }
    }
    
}
