using System;

namespace Session03exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ange ett antal siffror, separerat ned kommatecken.");
            var input = Console.ReadLine();
            var inputArray = input.Split(new [] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var number in inputArray)
            {
                Console.WriteLine("Värdet är " + number);
            }
        }
    }
}
