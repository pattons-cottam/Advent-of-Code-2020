using System;
using System.IO;
using System.Linq;

namespace PassportProcessing
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var validPassports = CreatePassportStrings(GetInput())
                  .Select(s => new Passport(s))
                  .Count(p => p.IsValid);

            Console.WriteLine($"valid passports: {validPassports}");
        }

        public static string[] CreatePassportStrings(string[] input)
        {
            var passports = new string[input.Count(i => i == "") + 1];
            var index = 1;

            foreach (var l in input)
            {
                if (l == "")
                {
                    index++;
                    continue;
                }

                passports[index - 1] += passports[index - 1] == null
                    ? l
                    : $" {l}";
            }

            return passports;
        }

        private static string[] GetInput()
        {
            return File.ReadAllLines("./data.txt");
        }
    }
}
