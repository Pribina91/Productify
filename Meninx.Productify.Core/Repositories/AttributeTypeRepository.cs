using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Data;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Core.Repositories
{
    public class AttributeTypeRepository : Repository<AttributeType>
    {
        public AttributeTypeRepository(IProductifyContext context) : base(context)
        {
        }
    }
}