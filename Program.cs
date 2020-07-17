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
            OutputAllNeighborhoodsWithSequentialQueries();
            OutputAllNeighborhoodsWithNamesUniqueOneQuery();
            OutputAllNeighborhoodsWithNamesUniqueUsingQuerySyntax();
        }

        /// <summary>
        /// Loads all the properties at JSONPath into the Properties property.
        /// </summary>
        static void LoadPropertiesFromJSON()
        {
            try
            {
                JObject JSONData = JObject.Parse(File.ReadAllText(JSONPath));
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

        /// <summary>
        /// <para>Outputs to the console the following:</para>
        /// <para>1.) All the neighborhoods with no filter</para>
        /// <para>2.) All the neighborhoods with non-empty, non-null, names</para>
        /// <para>3.) All the unique neighborhoods with non-empty, non-null, names.</para>
        /// </summary>
        static void OutputAllNeighborhoodsWithSequentialQueries()
        {
            var allNeighborhoods = Properties
                                    .Select(prop => new { prop.Neighborhood });
            Console.WriteLine("Total neighborhoods in dataset: {0}", allNeighborhoods.Count());
            foreach (var oneNeighborhood in allNeighborhoods)
            {
                Console.WriteLine(oneNeighborhood.Neighborhood);
            }
            Console.WriteLine();

            var allNeighborhoodsWithNames = allNeighborhoods
                                            .Where(prop => !String.IsNullOrEmpty(prop.Neighborhood));
            Console.WriteLine("Neighborhoods with names in dataset: {0}", allNeighborhoodsWithNames.Count());
            foreach (var oneNeighborhood in allNeighborhoodsWithNames)
            {
                Console.WriteLine(oneNeighborhood.Neighborhood);
            }
            Console.WriteLine();

            var allNeighborhoodsWithNamesUnique = allNeighborhoodsWithNames
                                                    .Distinct();
            Console.WriteLine("Unique neighborhoods with names in dataset: {0}", allNeighborhoodsWithNamesUnique.Count());
            foreach (var oneNeighborhood in allNeighborhoodsWithNamesUnique)
            {
                Console.WriteLine(oneNeighborhood.Neighborhood);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Outputs to the console all the neighborhoods with non-empty, non-null, names that are unique. Uses a single LINQ Method statement.
        /// </summary>
        static void OutputAllNeighborhoodsWithNamesUniqueOneQuery()
        {
            var allNeighborhoodsWithNamesUnique = Properties
                                                    .Select(prop => new { prop.Neighborhood })
                                                    .Where(prop => !String.IsNullOrEmpty(prop.Neighborhood))
                                                    .Distinct();
            Console.WriteLine("Using a single LINQ Method statement, unique neighborhoods with names in dataset: {0}", allNeighborhoodsWithNamesUnique.Count());
            foreach (var oneNeighborhood in allNeighborhoodsWithNamesUnique)
            {
                Console.WriteLine(oneNeighborhood.Neighborhood);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Outputs to the console all the neighborhoods with non-empty, non-null, names that are unique. Uses a single LINQ Query statement.
        /// </summary>
        static void OutputAllNeighborhoodsWithNamesUniqueUsingQuerySyntax()
        {
            var allNeighborhoodsWithNamesUniqueUsingLINQ = (from prop in Properties
                                                            where !String.IsNullOrEmpty(prop.Neighborhood)
                                                            select new { prop.Neighborhood }).Distinct();
            Console.WriteLine("Using a single LINQ Query statement, unique neighborhoods with names in dataset: {0}", allNeighborhoodsWithNamesUniqueUsingLINQ.Count());
            foreach (var oneNeighborhood in allNeighborhoodsWithNamesUniqueUsingLINQ)
            {
                Console.WriteLine(oneNeighborhood.Neighborhood);
            }
            Console.WriteLine();
        }
    }
}
