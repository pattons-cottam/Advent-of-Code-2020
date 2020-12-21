using System;
using System.Collections.Generic;
using System.IO;

var input = File.ReadAllLines("./input.txt");
// var input = new[] { "nop +0", "acc +1", "jmp +4", "acc +3", "jmp -3", "acc -99", "acc +1", "jmp -4", "acc +6", };

for (int i = 0; i < input.Length; i++)
{
    var operation = input[i].Split(' ')[0];

    if (operation == "acc")
        continue;

    var alteredInput = new string[input.Length];

    Array.Copy(input, alteredInput, input.Length);

    alteredInput[i] = alteredInput[i].Replace(operation, operation == "jmp" ? "nop" : "jmp");

    var acc = TestInput(alteredInput);

    if (acc is not null)
    {
        Console.WriteLine($"Program completed, accumulated value: {acc}");
        break;
    }
}

int? TestInput(string[] input)
{
    var accumulator = 0;
    var position = 0;
    var positionsHit = new List<int>();

    while (true)
    {
        if (position == input.Length)
            return accumulator;

        if (positionsHit.Contains(position))
            return null;
        else
            positionsHit.Add(position);

        var operation = input[position].Split(' ');

        switch (operation[0])
        {
            case "jmp":
                position += int.Parse(operation[1]);
                break;

            case "acc":
                accumulator += int.Parse(operation[1]);
                position += 1;
                break;

            case "nop":
                position += 1;
                break;

            default:
                // invalid operation found
                return null;
        }
    }
}