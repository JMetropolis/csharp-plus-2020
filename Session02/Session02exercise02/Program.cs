using System;

namespace Session02exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to enter your name? (y/n)");
            var key = Console.ReadKey();

            if (key.KeyChar == 'n')
                return;

            var name = Console.ReadLine();

            try
            {
                Console.WriteLine("Enter your name:");
                name = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Write("Please enter Name in the Correct Format");
                if (ex.Message != null)
                    Console.WriteLine("Hello, " + name);
            }

            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
