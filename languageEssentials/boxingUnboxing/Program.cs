using System;
using System.Collections.Generic;

namespace boxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
        // Create an empty List of type object
            List<object> stuff = new List<object>();
        // Add the following values to the list: 7, 28, -1, true, "chair"
            stuff.Add(7);
            stuff.Add(28);
            stuff.Add(-1);
            stuff.Add(true);
            stuff.Add("chair");
        // Loop through the list and print all values (Hint: Type Inference might help here!)
            foreach (var item in stuff){
                Console.WriteLine(item);
            }
        // Add all values that are Int type together and output the sum
            int sum = 0;
            foreach (var item in stuff){
                if (item is int){
                    sum += (int)item;
                }
            }
            Console.WriteLine(sum);
        }
    }
}
