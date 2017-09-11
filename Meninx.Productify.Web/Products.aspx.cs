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

        private static ProductifyServiceClient _service;

        protected void Page_Load(object sender, EventArgs e)
        {
            _service = new ProductifyServiceReference.ProductifyServiceClient();

            GetProducts(null, null);
        }


        [System.Web.Services.WebMethod]
        public List<ProductContract> GetProducts(string productName, string code)
        {
            Datalist = _service.GetData(productName, code).ToList();
            
            productList.DataSource = Datalist;
            productList.DataBind();

            return Datalist;
        }
        [System.Web.Services.WebMethod]
        public static List<AttributeContract> GetProductDetail(int productId)
        {
            return _service.GetProductAttributes(productId).ToList();
        }

        [System.Web.Services.WebMethod]
        public static void UpdateProduct(ProductContract product)
        {
            _service.UpdateProduct(product);
        }
        [System.Web.Services.WebMethod]
        public static void DeleteProduct(int productId)
        {
            _service.RemoveProduct(productId);
        }
        [System.Web.Services.WebMethod]
        public static void CreateProduct(string name, string code, string price)
        {
            int priceNumber;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name));
            }
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(nameof(code));
            }
            if (!int.TryParse(price, out priceNumber))
            {
                throw new ArgumentException(nameof(priceNumber));
            }

            var newProduct = new ProductContract()
            {
                Code = code,
                Name = name,
                Price = priceNumber,
            };
             
            _service.AddProduct(newProduct);
        }

        public override void Dispose()
        {
            base.Dispose();

            //if (_service != null)
            //{
            //    _service.Close();
            //}
        }
    }
}