/* Copyright 2018 */
/* Cody Rose */

using System;

namespace USC_Map
{
    public class USCMap
    {
        internal static Database buildings = new Database();
        public static void Main(string[] args)
        {
            Console.WriteLine("-=-= Starting USC Map Program =-=-\n\tBy: Cody Rose");
            buildings.PopulateDictionary();
        }
    }
}