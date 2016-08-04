using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitterWeatherBot
{



    [JsonObject(MemberSerialization.OptIn)]
    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class City
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("coord")]
        public Coord Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("list")]
        public List<WeatherItem> WeatherData { get; set; }
    }

    [JsonObject]
    public class WeatherItem
    {
        [JsonProperty("dt_txt")]
        //[JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime TimeOfDay { get; set; }

        [JsonProperty("main")]
        public Main MainData { get; set; }

        [JsonProperty("weather")]
        public List<Weather> WeatherList { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

    }

    [JsonObject(MemberSerialization.OptIn)]
    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string MainDesc { get; set; }
        [JsonProperty("description")]
        public string SubDesc { get; set; }
    }
    [JsonObject]
    public class Main
    {
        [JsonIgnore]
        public double TempAsCelcius { get { return 5.0 / 9.0 * (Temp - 32); } }


        [JsonProperty("temp")]
        public double Temp { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        [JsonProperty("pressure")]
        public double Pressure { get; set; }
        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }
        [JsonProperty("grnd_level")]
        public double GrndLevel { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("temp_kf")]
        public double TempKf { get; set; }
    }
    [JsonObject]
    public class Wind
    {
        /// <summary>
        /// MPH
        /// </summary>
        [JsonProperty("speed")]
        public double Speed { get; set; }

        /// <summary>
        /// Wind direction, degrees (meteorological)
        /// </summary>
        [JsonProperty("deg")]
        public double Direction { get; set; }
    }

}