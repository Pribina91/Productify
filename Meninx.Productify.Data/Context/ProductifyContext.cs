using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Models;


namespace Meninx.Productify.Data.Context
{
    public class ProductifyContext : IProductifyContext
    {
        public ProductifyContext() : base("name=ProductifyContext")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public override DbSet<Product> Products { get; set; }
        public override DbSet<Attribute> Atrributes { get; set; }
        public override DbSet<StringAtrribute> StringAtrributes { get; set; }
        public override DbSet<IntegerAtrribute> IntegerAtrributes { get; set; }
        public override DbSet<DatetimeAtrribute> DatetimeAtrributes { get; set; }
        public override DbSet<AttributeType> AttributeTypes { get; set; }
    }
}