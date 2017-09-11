using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Meninx.Productify.Service;

namespace Meninx.Productify.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;
            try
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:4321/ProductifyService");

                //Instantiate ServiceHost
                host = new ServiceHost(
                    typeof(ProductifyService),
                    httpBaseAddress);

                //Add Endpoint to Host
                host.AddServiceEndpoint(
                    typeof(IProductifyService),
                    new WSHttpBinding(),
                    "");

                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                host.Description.Behaviors.Add(serviceBehavior);

                //Open
                host.Open();
                System.Console.WriteLine("Service is live now at : {0}", httpBaseAddress);
                System.Console.ReadKey();
            }

            catch (Exception ex)
            {
                host = null;
                System.Console.WriteLine("There is an issue with StudentService" + ex.Message);
            }
        }
    }
}