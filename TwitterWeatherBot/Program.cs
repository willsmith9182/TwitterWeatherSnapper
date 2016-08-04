using System;
using System.CodeDom;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Tweetinvi;
using Tweetinvi.Models;

namespace TwitterWeatherBot
{
    public class WeatherService
    {
        private const string UrlBase = "http://api.openweathermap.org/data/2.5/forecast/city?id={0}&APPID={1}&units=imperial";
        private readonly string _appId;
        private readonly WebClient _webClient;

        public WeatherService(string appId)
        {
            _appId = appId;
            _webClient = new WebClient();
        }

        public City GetWeatherDataForCity(string cityId)
        {
            return JsonConvert.DeserializeObject<City>(_webClient.DownloadString(string.Format(UrlBase, cityId, _appId)));
        }

    }

    public class TwitterService
    {
        private readonly CredentialStrings _creds;

        public TwitterService(CredentialStrings creds)
        {
            _creds = creds;
        }

        public ITweet SendTweet(string message)
        {
            _creds.Authenticate();

            return Tweet.PublishTweet(message);
        }

    }


    class Program
    {


        static void Main(string[] args)
        {
            var twitter = new TwitterService(new CredentialStrings());
            var weather = new WeatherService("80863af25548b3d9c0ad735342ff0965");

            var bot = new WeatherBot(twitter, weather, "3333133");
            var exit = false;
            while (!exit)
            {
                exit = bot.DoTwitterThings();
                Thread.Sleep(TimeSpan.FromMilliseconds(2000));
            }
            Console.Write("Press any key to exit.");
            Console.ReadLine();
        }
    }

    public class WeatherBot
    {
        private readonly TwitterService _twitterService;
        private readonly WeatherService _weatherService;
        private readonly string _cityCode;
        private DateTime nextWeatherTime = DateTime.MinValue;
        private DateTime lastTweedTime = DateTime.MinValue;

        public WeatherBot(TwitterService twitterService, WeatherService weatherService, string cityCode)
        {
            _twitterService = twitterService;
            _weatherService = weatherService;
            _cityCode = cityCode;
        }

        public bool DoTwitterThings()
        {


            try
            {

                if (nextWeatherTime == DateTime.MinValue)
                {
                    var weather = _weatherService.GetWeatherDataForCity(_cityCode);

                    nextWeatherTime = weather.WeatherData.First().TimeOfDay;
                    Console.WriteLine("Weather next scheduled for: {0}", nextWeatherTime.ToString("R"));
                }

                var now = DateTime.Now;
                var comparison = now.Date.AddHours(now.Hour);

                if (comparison >= nextWeatherTime)
                {
                    Console.WriteLine("Time to get the weather: {0}", now.ToString("R"));
                    var weather = _weatherService.GetWeatherDataForCity(_cityCode);
                    var currentData = weather.WeatherData.First();


                    if (currentData.TimeOfDay == comparison)
                    {


                        Console.WriteLine("");
                        Console.WriteLine("Time to tweet the weather: {0}", now.ToString("R"));
                        var tweetContent =
                            string.Format(
                                "The current Temperature in Brighton is {0:00.00}{1}, the Windspeed is {2} MPH and the Humidity is {3}%",
                                currentData.MainData.TempAsCelcius, (char)176, currentData.Wind.Speed,
                                currentData.MainData.Humidity);

                        _twitterService.SendTweet(tweetContent);
                        nextWeatherTime = comparison.AddHours(3);
                    }
                }
                else
                {
                    Console.WriteLine("Waiting for the next poll of weather data next poll in {0} min.", nextWeatherTime.Subtract(now).Minutes);
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Shit went bang.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.StackTrace);
            }
            return true;
        }
    }

    public class CredentialStrings
    {
        //properties in C#
        //hash these Twitter hashed strings with BCrypt (32) with my usual password - to make these hashes of hashes
        //BCrypt will make it (almost) impossible to dehash http://stackoverflow.com/questions/348109/is-double-hashing-a-password-less-secure-than-just-hashing-it-once
        //will need to dehash too, which could be a problem (dehash means compare, BCrypt is cryptographic hash)
        public CredentialStrings()
        {
            this._consumerKey = "K5s3GKFBSIid49rzj0rJgRiXz";
            this._consumerSecret = "VkY604dIFbQQqgX1jwdCynIeZ8SB429M7iGdFZNMQXmfbQ35AZ";
            this._accessToken = "415080340-Ko8m0N0PKwQQFP9zuFoUmdUaZxOI1RGP1argAgcb";
            this._accessTokenSecret = "ZuAs8Mi4CWw0wl0apIzNitan8DIPnr3mb5YbD8dyaLWjQ";
        }

        //special behind the scenes syntatic sugar 
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;
        private bool _authd;

        public void Authenticate()
        {
            if (_authd) return;

            _authd = true;

            Auth.SetUserCredentials(_consumerKey, _consumerSecret, _accessToken, _accessTokenSecret);
        }
    }
}
