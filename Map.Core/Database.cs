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
        public Dictionary<string, Building> buildingDB = new Dictionary<string, Building>();

        public void PopulateDictionary()
        {
            string file = File.ReadAllText(@"./buildings.json");
            var values = JsonConvert.DeserializeObject<Dictionary<string, Building>>(file);
            foreach (KeyValuePair<string, Building> kvp in values)
            {
                buildingDB.Add(kvp.Key, kvp.Value);
            }
        }

        public List<Building> FindBuildings (string Name)
        {
            // Convert to lowercase for comparison purposes
            string name = Name.ToLower();
            List<Building> result = new List<Building>();
            // Loop through all of the Buildings in the database
            foreach (KeyValuePair<string, Building> kvp in buildingDB)
            {
                // Compare the name of the building with the input
                if (kvp.Value.Name.ToLower().Contains(name))
                {
                    result.Add(buildingDB[kvp.Key]);
                    break;
                }

                // Compare the code for the building with the input
                if (kvp.Value.Code.ToLower().Contains(name))
                {
                    result.Add(buildingDB[kvp.Key]);
                    break;
                }
            }

            return result;
        }
    }
}