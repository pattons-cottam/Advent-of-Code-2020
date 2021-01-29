using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

PartTwo();

void PartOne()
{
    var input = File.ReadAllLines("./input.txt");
    Console.WriteLine($"Jolt differential: {Adapter.FindJoltDifferential(input.ToIntArray())}");
}

void PartTwo()
{
    var input = File.ReadAllLines("./input.txt");

    var totalArrangements = Adapter.CalculateTotalArrangements(input.ToIntArray());

    Console.WriteLine($"total arrangements: {totalArrangements}");
}

public static class Adapter
{
    public static long CalculateTotalArrangements(int[] adapters)
    {
        var tuples = Adapter.CreateTreeTuples(adapters);

        for (int i = tuples.Length - 1; i >= 0; i--)
        {
            if (tuples[i].children is not null)
            {
                foreach (var c in tuples[i].children)
                {
                    tuples[i].value += tuples.Single(t => t.index == c).value;
                }
            }
        }

        return tuples.First().value;
    }

    public static (int index, int[] children, long value)[] CreateTreeTuples(int[] adapters)
    {
        var treeTuples = new List<(int index, int[] children, long value)>();
        var ordered = adapters.Append(0).OrderBy(a => a).ToArray();
        ordered = ordered.Append(ordered.Last() + 3).ToArray();
        var adapterCount = ordered.Length;

        for (int i = 0; i < adapterCount; i++)
        {
            if (i < adapterCount - 3 && ordered[i + 3] == ordered[i] + 3)
                treeTuples.Add((index: ordered[i], children: new[] { ordered[i + 1], ordered[i + 2], ordered[i + 3] }, value: 0));
            else if (i < adapterCount - 2 && ordered[i + 2] == ordered[i] + 2)
                treeTuples.Add((index: ordered[i], children: new[] { ordered[i + 1], ordered[i + 2] }, value: 0));
            else if (i < adapterCount - 1)
                treeTuples.Add((index: ordered[i], children: new[] { ordered[i + 1] }, value: 0));
            else
                treeTuples.Add((index: ordered[i], children: null, value: 1)); // laptop is always 3 above highest
                                                                               // treeTuples.Add((index: ordered[i], children: new[] { ordered.Last() + 3 }, value: 1)); // laptop is always 3 above highest
        }

        return treeTuples.ToArray();
    }

    public static int FindJoltDifferential(int[] adapters)
    {
        var ordered = adapters.Append(0).OrderBy(a => a).ToArray();
        var dOne = 0;
        var dThree = 0;

        for (int i = 1; i < ordered.Length; i++)
        {
            if (ordered[i] - ordered[i - 1] == 1)
                dOne++;
            else
                dThree++;
        }

        return dOne * (dThree + 1);
    }
}

static class StringExtension
{
    public static int[] ToIntArray(this string[] input)
    {
        var l = new List<int>();

        foreach (var i in input)
            l.Add(int.Parse(i));

        return l.ToArray();
    }
}