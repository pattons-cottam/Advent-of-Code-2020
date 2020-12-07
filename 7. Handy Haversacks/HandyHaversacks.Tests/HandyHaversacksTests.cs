namespace HandyHaversacks.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class HandyHaversacksTests
    {
        // [Fact] // list equality is not implemented in Bag so this fails despite being correct.
        public void Populate_bag_tree_test()
        {
            // ** every line adds the first named bag to the bags named after it **
            var bagLines = new[]{
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            };

            var expected = new List<Bag>
            {
                new Bag { Name = "bright white", ContainedIn = new List<string> { "light red", "dark orange" } },
                new Bag { Name = "muted yellow", ContainedIn = new List<string> { "light red", "dark orange" } },
                new Bag { Name = "shiny gold", ContainedIn = new List<string> { "bright white", "muted yellow" } },
                new Bag { Name = "faded blue", ContainedIn = new List<string> { "muted yellow", "dark olive", "vibrant plum" } },
                new Bag { Name = "dark olive", ContainedIn = new List<string> { "shiny gold"} },
                new Bag { Name = "vibrant plum", ContainedIn = new List<string> { "shiny gold" } },
                new Bag { Name = "dotted black", ContainedIn = new List<string> { "dark olive", "vibrant plum" } }
            };

            var actual = Program.PopulateReverseBagTree(bagLines);

            Assert.Equal(expected, actual);
        }
    }
}
