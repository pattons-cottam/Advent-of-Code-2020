using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("./input.txt");
// var input = File.ReadAllLines("./test.txt");

PartTwo(input);

static void PartOne(string[] input)
{
    var invalidNumber = XMASDataProcessor.FindFirstInvalidNumber(input.ToLongArray(), 25);
    Console.WriteLine($"first invalid number: {invalidNumber}");
}

static void PartTwo(string[] input)
{
    var encryptionWeakness = XMASDataProcessor.FindEncryptionWeakness(input.ToLongArray(), 1212510616);
    Console.WriteLine($"encryption weakness: {encryptionWeakness}");
}

public static class XMASDataProcessor
{
    public static long FindEncryptionWeakness(long[] data, long target)
    {
        var currentBatch = new List<long>();

        for (int i = 0; i < data.Length; i++)
        {
            long runningTotal = 0;
            var index = i;

            while (runningTotal < target)
            {
                currentBatch.Add(data[index]);
                runningTotal += data[index];
                index++;
            }

            if (runningTotal == target)
                break;
            else
                currentBatch.Clear(); // running total must be greater
        }

        var ordered = currentBatch.OrderBy(i => i).ToArray();

        return ordered.First() + ordered.Last();
    }

    public static long FindFirstInvalidNumber(long[] data, int preambleLength)
    {
        // i = the index of the value we are looking for
        for (int i = 0; i < data.Length; i++)
        {
            var set = data.Skip(i).Take(preambleLength);
            var target = data[i + preambleLength];

            if (!FoundMatchingPair(set, target))
                return target;
        }

        throw new Exception("No invalid number found");
    }

    private static bool FoundMatchingPair(IEnumerable<long> set, long target)
    {
        foreach (var d in set)
        {
            if (set.Contains(target - d))
                return true;
        }

        return false;
    }
}

public static class StringExtensions
{
    public static long[] ToLongArray(this string[] input)
    {
        var l = new List<long>();

        foreach (var i in input)
            l.Add(long.Parse(i));

        return l.ToArray();
    }
}