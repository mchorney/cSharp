using System;

namespace wizard
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Wizard Gandalf = new Wizard("Gandalf");
            Console.WriteLine(Gandalf.dexterity);
            Console.WriteLine(Gandalf.health);
            Console.WriteLine(Gandalf.intelligence);
            
        }
    }
}