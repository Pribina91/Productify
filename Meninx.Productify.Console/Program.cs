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
            ServiceHost studentServiceHost = null;
            try
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:4321/ProductifyService");

                //Instantiate ServiceHost
                studentServiceHost = new ServiceHost(
                    typeof(ProductifyService),
                    httpBaseAddress);

                //Add Endpoint to Host
                studentServiceHost.AddServiceEndpoint(
                    typeof(IProductifyService),
                    new WSHttpBinding(),
                    "");

                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                studentServiceHost.Description.Behaviors.Add(serviceBehavior);

                //Open
                studentServiceHost.Open();
                System.Console.WriteLine("Service is live now at : {0}", httpBaseAddress);
                System.Console.ReadKey();
            }

            catch (Exception ex)
            {
                studentServiceHost = null;
                System.Console.WriteLine("There is an issue with StudentService" + ex.Message);
            }
        }
    }
}