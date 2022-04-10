using System;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;


namespace testInfluxDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I per inviare, altro per uscire");
            var Vin= Console.ReadLine();

            if (Vin == "I" || Vin == "i") { 
                
                
                InvioDati(124.4, 1.5);
                
            }

              

           
        }

        static async Task InvioDati(double Temp, double Press)
        {

            var influxDbClient = InfluxDBClientFactory.Create("http://192.168.0.54:8086"); //IP server Influx, no credenziali accesso

            //
            // Write Data
            //
            var writeApiAsync = influxDbClient.GetWriteApiAsync();
            string measurement = "mysensor"; //"measurement", ovvero ciò che sarebbe la "tabella se SQL"
            string Lbl1 = "Temperatura";    //field
            string Lbl2 = "Pressione";      //field
            
            //Timestamp epoch aggiungo automaticamente con precisione WritePrecision
           
            string daInviare = measurement + " " + Lbl1 + "=" + Temp.ToString() + "," + Lbl2 + "=" + Press.ToString();

            try
            {
                await writeApiAsync.WriteRecordAsync(daInviare, WritePrecision.S,
                   "PROVA", "my-org");
                Console.WriteLine("Hello World!");
            }
            catch
            {

                Console.WriteLine("ERROR !");
            }

        }




    }
}
