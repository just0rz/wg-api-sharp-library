using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WGSharpAPI;
using WGSharpAPI.Interfaces;

namespace SampleWGSharpAPI
{
    class Program
    {
        /// <summary>
        /// The application id is provided to you by WarGaming.
        /// Go to developer's room and create an application.
        /// You will receive an application id and you will then be able to make calls to WG's API using this library
        /// </summary>
        private const string applicationId = "demo"; // you should replace this

        static void Main(string[] args)
        {

            ShowSearchPlayersSample();

        }

        public static void ShowSearchPlayersSample()
        {
            // we initialize the WGApplication object by calling the most basic constructor which needs you to supply your application id
            IWGApplication wgApplication = new WGApplication(applicationId);

            // The response will be of the WGResponse<T> where T is the type that you will get

            // e.g. calling the SearchPlayers method will return a WGResponse<List<Player>>

            var playersQueryResult = wgApplication.SearchPlayers("Just0rz");

            if (playersQueryResult.Status != "ok")
            {
                Console.WriteLine("There was an error when trying to retrieve the items");

                return;
            }

            if (playersQueryResult.Count <= 0)
            {
                Console.WriteLine("Query yielded no results.");

                return;
            }

            foreach (var player in playersQueryResult.Data)
            {
                Console.WriteLine("AccountID: {0}, Nickname: {1}", player.AccountId, player.Nickname);
            }

            Console.ReadKey();
        }
    }
}
