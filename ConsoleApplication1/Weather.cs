using System;
using System.Xml;

namespace ConsoleApplication
{
    //http://www.dreamincode.net/forums/topic/153911-c%23using-google-weather-api-in-a-c%23-application/

    public class Weather 
    {


    }
    public class Conditions
    {
        public string AvgTemp = "";
        public string WindSpeed = "";
        public string Humidity = "";

        public Conditions getCurrentConditions()
        {
            Conditions c = new Conditions();

            //potential error with the way im loading string if so -> .Load(string.Format())
            XmlDocument xmlConditions = new XmlDocument();

            try
            {
                xmlConditions.Load("http://www.google.com/ig/api?weather=bn1");
            }
            catch(Exception e)
            {
                e.StackTrace.ToString();
            }

            //looking for XML = "/xml_api_reply/weather/forecast_information/WHATEVER VARIABLE YOU NEED HERE"
            if (xmlConditions.SelectSingleNode("xml_api_reply / weather / problem_cause") == null)
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

        public string getAvgTemp()
        {
            return this.AvgTemp;
        }

        public string getWindSpeed()
        {
            return this.WindSpeed;
        }

        public string getHumidity()
        {
            return this.Humidity;
        }
    }
}