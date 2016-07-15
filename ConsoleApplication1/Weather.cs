using System.Xml;

namespace ConsoleApplication
{
    //http://www.dreamincode.net/forums/topic/153911-c%23using-google-weather-api-in-a-c%23-application/

    class Weather 
    {
        //only care about Brighton
        public const string LOCATION = "Brighton, East Sussex";

        //object that holds the SOAP weather deets
        
        

    }
    public class Conditions
    {
        public string AvgTemp = "N/A";
        public string WindSpeed = "N/A";
        public string Humidity = "N/A";

        public static Conditions getCurrentConditions(string LOCATION)
        {
            Conditions c = new Conditions();

            //potential error with the way im loading string if so -> .Load(string.Format())
            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load("http://www.google.com/ig/api?weather=" + LOCATION);


            //looking for XML = "/xml_api_reply/weather/forecast_information/WHATEVER VARIABLE YOU NEED HERE"
            if (xmlConditions.SelectSingleNode("xml_api_reply / weather / problem_cause") != null)
            {
                //bomb out 
                c = null;
            }
            else
            {
                foreach (XmlNode node in xmlConditions.SelectNodes("/xml_api_reply/weather/forecast_conditions"))
                {
                    c.AvgTemp = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/temp_c").Attributes["data"].InnerText;
                    c.WindSpeed = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/wind_conditions").Attributes["data"].InnerText;
                    c.Humidity = xmlConditions.SelectSingleNode("/xml_api_reply/weather/forecast_information/humidity").Attributes["data"].InnerText;
                }
            }
            return c;
        }
    }
}