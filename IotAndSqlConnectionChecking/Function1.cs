using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Data;
using System;



using Newtonsoft.Json;

using System.Collections.Generic;
using Microsoft.Azure.Services.AppAuthentication;
using System.Security;

using Microsoft.IdentityModel.Clients.ActiveDirectory;


namespace IotAndSqlConnectionChecking
{
    public static class Function1
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("Function1")]
        public static void Run([IoTHubTrigger("sql-pre-ue1-prd-mc",ConsumerGroup ="test", Connection = "Conn")] EventData message, ILogger log)
        {
            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");

            try
            {
                //parsing the input json data

                // Providing SQL credentials
                //string strcon = @"Data Source =sql-pre-ue1-prd-mc.database.windows.net;Trusted_Connection=no;Authentication=Active Directory Password;Database=DB-PRE-UE1-PRD-MC;User ID=sponnana@pregis.com;Password=India@2022;";

                 //----SQL SERVER Authentication Connection Strings---
                //string strcon = @"Server = tcp:sql - pre - ue1 - prd - mc.database.windows.net,1433; Initial Catalog = DB - PRE - UE1 - PRD - MC; Persist Security Info = False; User ID = PregisAdmin; Password ={ your_password}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
                //string strcon = @"Server=tcp:sql-pre-ue1-prd-mc.database.windows.net;Initial Catalog=DB-PRE-UE1-PRD-MC;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                //------------------

                string strcon = @"Server=tcp:sql-pre-ue1-prd-mc.database.windows.net,1433;Initial Catalog=DB-PRE-UE1-PRD-MC;Persist Security Info=False;User ID=SQLPortalFA@pregis.com;Password=Aflqslatrop20@#!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;";
                //string strcon = @"Data Source =sql-pre-ue1-prd-mc.database.windows.net;Trusted_Connection=no;Authentication=Active Directory Password;Database=DB-PRE-UE1-PRD-MC;User ID=ag@pregis.com;Password=@OnceUponATime!;";

                using (SqlConnection con = new SqlConnection(strcon))
                {



                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {


                        log.LogInformation("Connection Established");
                    }
                    con.Close();
                    Console.WriteLine("***************DONE**************");
                }
            }
            catch (Exception e)
            {
                log.LogInformation("Connection NOT Established");
                //log.LogInformation(e.Message);

            }
        }
    }
}