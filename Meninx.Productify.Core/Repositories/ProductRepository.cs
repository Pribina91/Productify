using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Core.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(IProductifyContext context) : base(context)
        {

        }

        public override IQueryable<Product> Table => this.context.Products.AsQueryable();
    }
}
