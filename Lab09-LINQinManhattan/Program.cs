using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Lab09_LINQinManhattan.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Lab09_LINQinManhattan
{
    class Program
    {
        private static List<Property> Properties = new List<Property>();
        private static string JSONPath = "../../../Assets/data.json";

        static void Main(string[] args)
        {
            LoadPropertiesFromJSON();
            OutputAllNeighborhoods();
        }

        static void LoadPropertiesFromJSON()
        {
            try
            {
                JObject JSONData = JObject.Parse(File.ReadAllText(JSONPath));
                //IEnumerable<JToken> properities = data.SelectTokens("$..properties");
                IEnumerable<JToken> JSONProperties = JSONData.SelectTokens("$..properties");
                foreach (JToken oneProperty in JSONProperties)
                {
                    Property newProperty = oneProperty.ToObject<Property>();
                    Properties.Add(newProperty);
                }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("Error getting data from JSON file");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static void OutputAllNeighborhoods()
        {

        }
    }
}
