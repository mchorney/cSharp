using System;
using System.Collections.Generic;

namespace collections_practice
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numArray = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            string[] strArray = {"Tim", "Martin", "Nikki", "Sara"};

            bool[] boolArray = {true, false, true, false, true, false, true, false, true, false};

            for (int i = 1; i < 11; i++) 
            {
                int[] myArr = new int[10];
                for (int j = 1; j < 11; j++)
                {
                    myArr[j-1] = i * j;
                }
                //Console.WriteLine("[" + string.Join(",", myArr) + "]");
            }

            List<string> flavors = new List<string>();
            flavors.Add("Oreo");
            flavors.Add("Mint chocolate chip");
            flavors.Add("Salted carmel");
            flavors.Add("Cookie dough");
            flavors.Add("Chocolate chip");

            //Console.WriteLine(flavors.Count);

            //Console.Write(flavors[2]);

            flavors.RemoveAt(2);

            //Console.WriteLine(flavors.Count);

            Dictionary<string,string> dict = new Dictionary<string,string>();

            for (int k = 0; k < 4; k++) 
            {
                dict.Add(strArray[k], null);
            }

            Random rand = new Random();
            List<string> keys = new List<string>(dict.Keys);
            foreach (var key in keys)
            {
                dict[key] = flavors[rand.Next(0, 3)];
            }
            
            foreach (KeyValuePair<string, string> entry in dict) 
            {
                Console.WriteLine(entry.Key + "-" + entry.Value);
            }

        }
    }
}
