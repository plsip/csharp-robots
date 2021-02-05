using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RealtorScraper.Models;
using RealtorScraper.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RealtorScraper
{
    class Program
    {
        static string city;
        static string stateAbbr;
        static bool isHeadless = false;

        static void Main(string[] args)
        {
            string inputParameters = Environment.GetEnvironmentVariable("inputParameters");

            Console.WriteLine(inputParameters);

            dynamic parsedInputParameters = JObject.Parse(inputParameters);

            Console.WriteLine($"Input Params: {parsedInputParameters}");

            var city = parsedInputParameters["City"];
            var stateAbbr = parsedInputParameters["State_Abbreviation"];

            Console.WriteLine($"City: {city}");
            Console.WriteLine($"State: {stateAbbr}");

            // Scrape Realtor.com
            //List<RealtorModel> realtors;
            using (WebScraper scraper = new WebScraper())
            {
                ////    realtors = scraper.GetRealtors(city, stateAbbr);
            }
            
            //// Clean Data
            //realtors = RemoveInvalidLocations(realtors);
            //realtors = RemoveNonAgents(realtors);
            //realtors = RemoveIncompleteRealtors(realtors);
            //realtors = RemoveDuplicateRealtors(realtors);
            //realtors = CleanRealtorNames(realtors);


            ////// Store Data In .csv
            ////ExportToCSV(realtors);
            //string outputData = JsonConvert.SerializeObject(realtors.ToArray());
            //Environment.SetEnvironmentVariable("output", outputData);
        }

        static List<RealtorModel> RemoveInvalidLocations(List<RealtorModel> realtors)
        {
            List<RealtorModel> realtorsToRemove = new List<RealtorModel>();

            foreach (var realtor in realtors)
            {
                if (realtor.City.ToLower() != city.ToLower() ||
                    realtor.State.ToLower() != stateAbbr.ToLower())
                {
                    realtorsToRemove.Add(realtor);
                }
            }

            realtorsToRemove.ForEach(x => realtors.Remove(x));

            return realtors;
        }

        static List<RealtorModel> RemoveNonAgents(List<RealtorModel> realtors)
        {
            List<RealtorModel> realtorsToRemove = new List<RealtorModel>();

            foreach (var realtor in realtors)
            {
                if (realtor.Name.ToLower().Contains("group") ||
                    realtor.Name.ToLower().Contains("team") ||
                    realtor.Name.ToLower().Contains("estate") ||
                    realtor.Name.ToLower().Contains("investment") ||
                    realtor.Name.ToLower().Contains("realty"))
                {
                    realtorsToRemove.Add(realtor);
                }
            }

            realtorsToRemove.ForEach(x => realtors.Remove(x));

            return realtors;
        }

        static List<RealtorModel> RemoveIncompleteRealtors(List<RealtorModel> realtors)
        {
            List<RealtorModel> realtorsToRemove = new List<RealtorModel>();

            foreach (var realtor in realtors)
            {
                if (string.IsNullOrEmpty(realtor.Name) ||
                    string.IsNullOrWhiteSpace(realtor.Name) ||
                    string.IsNullOrEmpty(realtor.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(realtor.PhoneNumber))
                {
                    realtorsToRemove.Add(realtor);
                }
            }

            realtorsToRemove.ForEach(x => realtors.Remove(x));

            return realtors;
        }

        static List<RealtorModel> RemoveDuplicateRealtors(List<RealtorModel> realtors)
        {
            List<RealtorModel> realtorsToRemove = new List<RealtorModel>();

            foreach (var realtor in realtors)
            {
                foreach (var realtorToCheck in realtors)
                {
                    if (realtorToCheck != realtor &&
                        realtorToCheck.PhoneNumber == realtor.PhoneNumber)
                    {
                        realtorsToRemove.Add(realtor);
                    }
                }
            }

            realtorsToRemove.ForEach(x => realtors.Remove(x));

            return realtors;
        }

        static List<RealtorModel> CleanRealtorNames(List<RealtorModel> realtors)
        {
            foreach (var realtor in realtors)
            {
                if (realtor.Name.Contains(","))
                {
                    realtor.Name = realtor.Name.Split(',')[0].Trim();
                }
                if (realtor.Name.Contains("-"))
                {
                    realtor.Name = realtor.Name.Split('-')[0].Trim();
                }
            }

            return realtors;
        }

        static void ExportToCSV<T>(List<T> realtors)
        {
            string fileName = $"{ city.ToLower() }_{ stateAbbr.ToLower() }__" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            var basePath = AppDomain.CurrentDomain.BaseDirectory + @"Data\";
            Directory.CreateDirectory(basePath); // Creates directory if not exists

            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var info = typeof(T).GetProperties();

            if (File.Exists(finalPath) == false)
            {
                var file = File.Create(finalPath);
                file.Close();

                StringBuilder sb = new StringBuilder();
                var header = "";

                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + ", ";
                }

                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);

                using (TextWriter sw = new StreamWriter(finalPath, true))
                {
                    sw.Write(sb.ToString());
                }
            }

            foreach (var realtor in realtors)
            {
                StringBuilder sb = new StringBuilder();
                var line = "";

                foreach (var prop in info)
                {
                    line += prop.GetValue(realtor, null) + ", ";
                }

                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);

                using (TextWriter sw = new StreamWriter(finalPath, true))
                {
                    sw.Write(sb.ToString());
                }
            }
        }
    }
}