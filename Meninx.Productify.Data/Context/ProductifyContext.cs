using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Models;


namespace Meninx.Productify.Data.Context
{
    public class ProductifyContext : DbContext
    {
        public ProductifyContext() : base("name=ProductifyContext")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Attribute> Atrributes { get; set; }
        public DbSet<StringAtrribute> StringAtrributes { get; set; }
        public DbSet<IntegerAtrribute> IntegerAtrributes { get; set; }
        public DbSet<DatetimeAtrribute> DatetimeAtrributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
    }
}