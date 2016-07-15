using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core.Controllers;
using Tweetinvi.Core.Factories;
using Tweetinvi.Core.Helpers;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Tweetinvi.Models.Entities;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {
            CredentialStrings cs = new CredentialStrings();
            WeatherStringBuilder wsb = new WeatherStringBuilder();

            Console.WriteLine("Publishing to twitter...");

            Auth.SetUserCredentials(cs.ConsumerKey, cs.ConsumerSecret, cs.AccessToken, cs.AccessTokenSecret);

            
            m.buildString();

            var  tweet = Tweet.PublishTweet(sb.ToString());

            if (tweet.IsTweetPublished)
            {
                Console.WriteLine("Successful");
            }
                     

            Console.WriteLine("Any key to Exit...");
            Console.ReadLine();

        }


        public void buildString()
        {

        }
    }

    class CredentialStrings
    {
        //properties in C#
        //hash these Twitter hashed strings with BCrypt (32) with my usual password - to make these hashes of hashes
        //BCrypt will make it (almost) impossible to dehash http://stackoverflow.com/questions/348109/is-double-hashing-a-password-less-secure-than-just-hashing-it-once
        //will need to dehash too, which could be a problem (dehash means compare, BCrypt is cryptographic hash)
        public CredentialStrings()
        {
            this.ConsumerKey = "K5s3GKFBSIid49rzj0rJgRiXz";
            this.ConsumerSecret = "VkY604dIFbQQqgX1jwdCynIeZ8SB429M7iGdFZNMQXmfbQ35AZ";
            this.AccessToken = "415080340-Ko8m0N0PKwQQFP9zuFoUmdUaZxOI1RGP1argAgcb";
            this.AccessTokenSecret = "ZuAs8Mi4CWw0wl0apIzNitan8DIPnr3mb5YbD8dyaLWjQ";
        }

        //special behind the scenes syntatic sugar 
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string AccessToken { get; private set; }
        public string AccessTokenSecret { get; private set; }


    }
}
