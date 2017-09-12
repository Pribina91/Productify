using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Meninx.Productify.Web.ProductifyServiceReference;
using Newtonsoft.Json;

namespace Meninx.Productify.Web
{
    public partial class Products : System.Web.UI.Page
    {
        //protected List<ProductContract> Datalist;

        private static ProductifyServiceClient _service;
        private static string _filterProductName;
        private static string _filterCode;
        private static int? _filterPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            _service = new ProductifyServiceReference.ProductifyServiceClient();
            Page.ClientScript.RegisterClientScriptInclude("Product", ResolveUrl("~/Scripts/Pages/Products.js"));
            attributeTypeListRepeater.DataSource = _service.GetAttributeTypes();
            attributeTypeListRepeater.DataBind();
        }


        [System.Web.Services.WebMethod]
        public static string GetProducts(string productName, string code)
        {
            var list = _service.GetData(null, null, null).ToList();
            return JsonConvert.SerializeObject(list);
        }

        [System.Web.Services.WebMethod]
        public static string GetProductDetail(int productId)
        {
            return JsonConvert.SerializeObject(_service.GetProductAttributes(productId).ToList());
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

        [System.Web.Services.WebMethod]
        public static void PostFilter(string name, string code, int? price)
        {
            _filterPrice = price;
            _filterCode = code;
            _filterProductName = name;
        }

        protected void OnJsonClick(object sender, EventArgs e)
        {
            var file = _service.GetJson(_filterProductName, _filterCode, _filterPrice);
        }

        protected void OnXmlClick(object sender, EventArgs e)
        {
            var file = _service.GetXml(_filterProductName, _filterCode, _filterPrice);
            AddFileToResponse(file);
        }

        private void AddFileToResponse(FileInfo file)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(file.FullName);
            Response.End();
        }
    }
}