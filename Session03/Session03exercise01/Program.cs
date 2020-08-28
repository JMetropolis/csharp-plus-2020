using System;

namespace Session03exercise01
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integerValues = new [] { 1, 2, 3 };
            var integerValueName = nameof(integerValues);

            for (int i = 0; i < integerValues.Length; i++)
            {
                var value = integerValues[i];

                Console.WriteLine($"Index {i} i arrayen {integerValueName} har värdet: {value}.");
            }

            var doWhileIndex = 0;

            do
            {
                var value = integerValues[doWhileIndex];

                Console.WriteLine($"Do while Index {doWhileIndex} i arrayen {integerValueName} har värdet {value}.");

                doWhileIndex++;
            }
            while (doWhileIndex < integerValues.Length);

            var whileIndex = 0;

            while (++whileIndex < integerValues.Length);
            {
                var value = integerValues[whileIndex];

                Console.WriteLine($"While Index {whileIndex} i arrayen {integerValueName} har värdet {value}.");

                whileIndex++;
            }
        }
    }
}
