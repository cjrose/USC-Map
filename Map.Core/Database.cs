/* Copyright 2018 */
/* Cody Rose */

using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace USC_Map
{
    public class Database
    {
        public static Dictionary<string, Building> buildingDB = new Dictionary<string, Building>();

        public static void PopulateDictionary()
        {
            string file = File.ReadAllText(@"./buildings.json");
            var values = JsonConvert.DeserializeObject<Dictionary<string, Building>>(file);
            foreach (KeyValuePair<string, Building> kvp in values)
            {
                buildingDB.Add(kvp.Key, kvp.Value);
            }
        }

        public static List<Building> FindBuildings (string Name)
        {
            // Convert to lowercase for comparison purposes
            string name = Name.ToLower();
            List<Building> result = new List<Building>();

            if (!name.Contains("-"))
            {
                // Loop through all of the Buildings in the database
                foreach (KeyValuePair<string, Building> kvp in buildingDB)
                {
                    // Compare the name of the building with the input
                    if (kvp.Value.Name.ToLower().Contains(name))
                    {
                        result.Add(buildingDB[kvp.Key]);
                        continue;
                    }

                    // Compare the code for the building with the input
                    if (kvp.Value.Code.ToLower().Contains(name))
                    {
                        result.Add(buildingDB[kvp.Key]);
                        continue;
                    }
                }
            } else
            {
                string[] s = name.Split('-');

                foreach(KeyValuePair<string, Building> kvp in buildingDB)
                {
                    if (kvp.Value.Name.ToLower().Contains(s[0]))
                    {
                        result.Add(buildingDB[kvp.Key]);
                        continue;
                    }

                    if (kvp.Value.Code.ToLower().Contains(s[1]))
                    {
                        result.Add(buildingDB[kvp.Key]);
                        continue;
                    }
                }
            }
            

            return result;
        }

        public static Building FindBuildingMatch(string Name)
        {
            string name = Name.ToLower();

            if (!name.Contains("-"))
            {
                // I think they should always include it now?
            }
            else
            {
                string[] s = name.Split('-');

                foreach(var kvp in buildingDB)
                {
                    if (kvp.Value.Name.ToLower().Equals(s[0]))
                    {
                        return kvp.Value;
                    }
                    if (kvp.Value.Code.ToLower().Equals(s[1]))
                    {
                        return kvp.Value;
                    }
                }
            }
            return null;
        }
    }
}