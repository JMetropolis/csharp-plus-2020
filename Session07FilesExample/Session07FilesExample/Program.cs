using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Session07FilesExample
{
	class Program
	{
		// Dokumentets data i raderna är separerade med ;
		const string _separator = ";";

		static void Main(string[] args)
		{
			// Läsa in en fils textinnehåll
			// FileStream för att läsa filens innehåll som bytes från början till slut
			using (FileStream stream = File.Open("maxfritid.csv", FileMode.Open))
			{
				// StreamReader för att konvertera bytes till tecken
				using (StreamReader reader = new StreamReader(stream))
				{
					// Här kan du få ut all text på en gång
					string fileContent = reader.ReadToEnd();
				}
			}

			// Går att skriva så här istället, men med mindre kontroll
			// Bra för testning
			string simpleReadFileContent = File.ReadAllText("maxfritid.csv");

			Stream manuallyDisposedStream = File.Open("maxFritid.csv", FileMode.Open);

			// Logik här

			manuallyDisposedStream.Dispose();

			// Definiera en lista som vi kan lagra produktdatat i
			List<Product> products = new List<Product>(10);

			using (FileStream stream = File.Open("maxfritid.csv", FileMode.Open))
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					string line = reader.ReadLine();
					while (line != null)
					{
						// Logik för raden
						string[] columns = line.Split(_separator);

						try
						{
							Product product = CreateProduct(columns);

							// Placera behandling av produkten efter inläsningen, i samma try-sats
							// För att undvika behandling av ogiltig data
							products.Add(product);
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}

						line = reader.ReadLine();
					}
				}
			}

			// Sortera en lista via CompareTo på klassen
			products.Sort();

			// Sortera en lista med en comparer
			products.Sort(new ProductNameComparer());

			// Sortera en lista med egen comparer via LINQ
			IEnumerable<Product> productsSortedByName = products.OrderBy(x => x, new ProductNameComparer()); ;

			// Sortera en lista via LINQ helt och hållet
			productsSortedByName = products.OrderBy(p => p.ProductName)
											.ThenBy(products => products.Id)
											.ThenByDescending(p => p.ProductSupplier)
											.ToList(); // <- För att genomföra sorteringen till en variabel,
													   // Annars körs sorteringen varje gång man loopar innehåll

			PrintProducts(products);
		}

		static void PrintProducts(IEnumerable<Product> products)
		{
			foreach (Product product in products)
			{
				Console.WriteLine($"{product.Id}: {product.ProductName}");
			}
		}

		static Product CreateProduct(string[] columns)
		{
			return new Product
			{
				Id = Convert.ToInt32(columns[0]),
				ProductNumber = columns[2],
				ProductName = columns[3],
				ProductBrand = ConvertToNullableInt(columns[4]),
				ProductSupplier = columns[5],
				ProductSynonyms = ConvertToArray(columns[6])
			};
		}

		// Konvertera till int
		static int ConvertToInt(string input)
		{
			int.TryParse(input, out int result);

			return result;
		}

		// Konvertera till nullable int (int?)
		static int? ConvertToNullableInt(string input)
		{
			if (int.TryParse(input, out int result))
			{
				return result;
			}
			else
			{
				return null;
			}
		}

		// Konvertera till sträng array
		static string[] ConvertToArray(string input)
		{
			if (input == null)
				return new string[0];

			return input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
