using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdooRpc.CoreCLR.Client;
using OdooRpc.CoreCLR.Client.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var settings = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("appsettings.json"));
            var odooConnection = settings["OdooConnection"].ToObject<OdooConnectionInfo>();

            try
            {
                var odooRpcClient = new OdooRpcClient(odooConnection);
                odooRpcClient.Authenticate().Wait();
                var odooVersion = odooRpcClient.GetOdooVersion().Result;

                Console.WriteLine("Odoo Version: {0} - {1}", odooVersion.ServerVersion, odooVersion.ProtocolVersion);

                

                if (odooRpcClient.SessionInfo.IsLoggedIn)
                {
                    Console.WriteLine("Login successful => User Id: {0}", odooRpcClient.SessionInfo.UserId);
                }
                else
                {
                    Console.WriteLine("Login failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Odoo: {0}", ex.Message);
            }
        }
    }
}
