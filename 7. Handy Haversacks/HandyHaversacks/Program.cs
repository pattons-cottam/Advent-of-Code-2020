namespace HandyHaversacks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Program
    {
        static void Main(string[] args)
        {
            PartOne();
        }

        private static void PartOne()
        {
            var bagLines = File.ReadAllLines("./data.txt");

            bags = PopulateReverseBagTree(bagLines);

            var shinyGoldBag = bags.Single(b => b.Name == "shiny gold");

            FindOutermostBag(shinyGoldBag);

            Console.WriteLine(bagsContainingGold.Count());
        }

        private static List<Bag> bags;

        private static List<Bag> bagsContainingGold = new List<Bag>();

        private static int bagCount = 0;

        public static void FindOutermostBag(Bag bag)
        {
            foreach (var containingBagName in bag.ContainedIn)
            {
                if (!bagsContainingGold.Any(b => b.Name == containingBagName))
                {
                    if (!bags.Any(b => b.Name == containingBagName))
                    {
                        // this is a top level bag
                        bagsContainingGold.Add(new Bag { Name = containingBagName });
                        continue;
                    }

                    var containingBag = bags.Single(b => b.Name == containingBagName);
                    bagsContainingGold.Add(containingBag);

                    FindOutermostBag(containingBag);
                }
            }
        }

        public static List<Bag> PopulateReverseBagTree(string[] bagLines)
        {
            var bags = new List<Bag>();

            foreach (var line in bagLines)
            {
                var split = line.Split("contain");
                var bagName = split[0].Replace(" bags ", "");

                if (!split[1].Contains(" no ")) // if it contains no bags, it is no use
                {
                    foreach (Match namedBag in Regex.Matches(split[1], "([a-z]+ ){2}"))
                    {
                        var namedBagName = namedBag.Value.TrimEnd();

                        // if a bag already exists, add this bag to the list of bags it is contained in
                        if (bags.Any(b => b.Name == namedBagName))
                        {
                            bags.Single(b => b.Name == namedBagName)
                                .ContainedIn
                                .Add(bagName);
                        }
                        else
                        {
                            // otherwise create a new bag and add this one to its contained in
                            bags.Add(new Bag
                            {
                                Name = namedBagName,
                                ContainedIn = new List<string> { bagName }
                            });
                        }
                    }
                }
            }

            return bags;
        }
    }

    public class Bag
    {
        public string Name;

        public List<string> ContainedIn;
    }
}
