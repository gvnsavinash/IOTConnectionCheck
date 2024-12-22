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
        public static void Run([IoTHubTrigger("sql-pres-ue1s-prsd-msc",ConsumerGroup ="test", Connection = "Conn")] EventData message, ILogger log)
        {
            log.LogInformation($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.Body.Array)}");

            try
            {
                //parsing the input json data

                ## Note All the Server names and UserID and Passwords are Fictional 

                // Providing SQL credentials
                //string strcon = @"Data Source =sql-pres-ue1s-prsd-msc.database.windows.net;Trusted_Connection=no;Authentication=Active Directory Password;Database=DatabaseName;User ID=UserID;Password=India@2022;";

                 //----SQL SERVER Authentication Connection Strings---
                //string strcon = @"Server = tcp:sql - pre - ue1 - prd - mc.database.windows.net,1433; Initial Catalog = DB - PRE - UE1 - PRD - MC; Persist Security Info = False; User ID = UserID; Password ={ your_password}; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
                //string strcon = @"Server=tcp:sql-pres-ue1s-prsd-msc.database.windows.net;Initial Catalog=DatabaseName;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                //------------------

                string strcon = @"Server=tcp:sql-pres-ue1s-prsd-msc.database.windows.net,1433;Initial Catalog=DatabaseName;Persist Security Info=False;User ID=;Password=Aflqslatrop20@#!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password;";
                //string strcon = @"Data Source =sql-pres-ue1s-prsd-msc.database.windows.net;Trusted_Connection=no;Authentication=Active Directory Password;Database=DatabaseName;User ID=UserID;Password=@OnceUponATime!;";

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
