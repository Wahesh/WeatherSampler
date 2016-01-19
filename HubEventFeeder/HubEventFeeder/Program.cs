using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using Newtonsoft.Json;

namespace HubEventFeeder
{
    class Program
    {

        //Replace the connection String with your connection and the event hub with your event hub name.
        const string  connectionString = "Endpoint=sb://maheshbus.servicebus.windows.net/;SharedAccessKeyName=mahesh;SharedAccessKey=n75sRkARkVx9vda9PaA6q7n+rFBY83asM5zIj3DIQNI=";
        const string eventHubName = "maheshhub";


        static void Main(string[] args)

        {


            //You can fetch your data from weather API or any Device. Fetching Data is not the key concern here. 
            //This is just a fake Data. The Weather API donot return data every minute or second. So we can not get relative large variety and  
            //and volume in the data. So I have tried to randomize the weather. 
            //While Randomizing the weather, I looked at hourly forcast at any weather forcast site and randomize values around that value, 
            //by adding a slight fluctuation to the value by generating a random number. 


            Dictionary<int, int> weatherDictionary = new Dictionary<int, int>();
            weatherDictionary.Add(20, 8);
            weatherDictionary.Add(21, 7);
            weatherDictionary.Add(22, 6);
            weatherDictionary.Add(23, 5);
            weatherDictionary.Add(0, 5);
            weatherDictionary.Add(1, 4);
            weatherDictionary.Add(2, 4);
            weatherDictionary.Add(3, 4);
            weatherDictionary.Add(4, 4);
            weatherDictionary.Add(5, 3);
            weatherDictionary.Add(6, 3);
            weatherDictionary.Add(7, 3);
            weatherDictionary.Add(8, 3);
            weatherDictionary.Add(9, 4);
            weatherDictionary.Add(10, 11);
            weatherDictionary.Add(11, 12);
            weatherDictionary.Add(13, 12);
            weatherDictionary.Add(14, 12);
            weatherDictionary.Add(15, 13);
            weatherDictionary.Add(16, 14);
            weatherDictionary.Add(17, 15);
            weatherDictionary.Add(18, 4);
            weatherDictionary.Add(19, 13);

            Random rnd = new Random();  
          

            data mydata = new data();
            while (true)

            {
                float fluctuation = (rnd.Next(1, 10));
                fluctuation = fluctuation / 10;
                float nowtemperature = weatherDictionary[Convert.ToInt16(DateTime.Now.Hour)] + fluctuation ;

                mydata.ID = Guid.NewGuid().ToString();
                mydata.tempertature = nowtemperature;
                mydata.when = DateTime.Now;
                mydata.location = "KTM";
                mydata.hour = weatherDictionary[Convert.ToInt16(DateTime.Now.Hour)];
 
                var eventHub = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
                eventHub.SendAsync(new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mydata))));
                Console.WriteLine(JsonConvert.SerializeObject(mydata));
                Thread.Sleep(100);
            }



         
               
        }

        public class data {
            public int hour { get; set; }
            public string ID { get; set; }
            public float tempertature { get; set; }
             public DateTime when{ get; set; }
            public string location{ get; set; }
        }

    }
}
