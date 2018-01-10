using System;

namespace human
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Human John = new Human("John");
            Human Adam = new Human("Adam");

            Console.WriteLine(Adam.health);
            John.Attack(Adam);
            Console.WriteLine(Adam.health);
        }
    }
}
       
