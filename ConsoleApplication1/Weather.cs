using System;
using System.Xml;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace ConsoleApplication
{
    //http://openweathermap.org/appid#use

    //returns all conditions as weather
    public class Weather 
    {

    }

    //conditions gets conditions
    //returns Condition c
    public class Conditions
    {
        public static string API_URL = "api.openweathermap.org/data/2.5/forecast/";
        public static string API_BRIGHTON_ID = "city?id=3333133&";
        public static  string API_KEY = "APPID=80863af25548b3d9c0ad735342ff0965";
        public static string API_STRING = API_URL + API_BRIGHTON_ID + API_KEY;
        
        public string AvgTemp = "";
        public string WindSpeed = "";
        public string Humidity = "";

        dynamic obj;


        public Boolean makeRequest()
        {
            try
            {
                obj = JsonConvert.DeserializeObject(API_STRING);
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }

            if (obj == null)
            {
                return false;
            }
            testAPI();
            return true;
        }
        

        public void testAPI()
        {
            Console.WriteLine(obj.coord.lon + "  " + obj.coord.lat);
            Console.ReadLine();
        }

        /**
         * 
         * These classes represent the weather object
         * 
         **/
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Sys
        {
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
    }
}