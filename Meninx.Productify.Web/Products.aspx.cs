using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Meninx.Productify.Web.ProductifyServiceReference;

namespace Meninx.Productify.Web
{
    public partial class Products : System.Web.UI.Page
    {
        protected List<ProductContract> Datalist;

        private ProductifyServiceClient _service;

        protected void Page_Load(object sender, EventArgs e)
        {
            _service = new ProductifyServiceReference.ProductifyServiceClient();

            GetProducts(null, null);
        }

        protected List<ProductContract> GetProducts(string productName, string code)
        {
            Datalist = _service.GetData(productName, code).ToList();
            
            productList.DataSource = Datalist;
            productList.DataBind();

            return Datalist;
        }
        public List<AttributeContract> GetProductDetail(int productId)
        {
            return this._service.GetProductAttributes(productId).ToList();
        }

        public override void Dispose()
        {
            base.Dispose();

            if (_service != null)
            {
                _service.Close();
            }
        }
    }
}