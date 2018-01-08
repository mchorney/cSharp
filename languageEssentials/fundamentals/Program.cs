using System;

namespace fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
        // Print all the valeus between 1- 255
        //  for(int i = 1; i < 256; i++)
        //     {
        //         Console.WriteLine(i);       
        //     }


        //Print values divisible by 3 and 5 but not both
        /* 
        for(int i = 1; i < 101; i ++)
            {
                if(!(i % 5 == 0 && i % 3 == 0))
                {
                     if(i % 5 == 0 || i % 3 == 0)
                    {
                        Console.WriteLine(i);
                    }
                }
            } 
        */
        for(int i = 1; i < 101; i ++)
            {
                System.Console.WriteLine(i);
                if(i % 5 == 0 && i % 3 == 0)
                {
                     Console.WriteLine("FizzBuzz");
                }
                else if(i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else if(i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
            }
        }
    }
}
