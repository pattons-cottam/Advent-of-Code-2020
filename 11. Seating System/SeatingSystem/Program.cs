using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Action<string> p = input => Console.WriteLine(input.ToString());

PartOne();

void PartOne()
{
    var initialLayout = CreateJaggedArray("./test.txt");

    var layout = new WaitingArea(initialLayout);

    layout.UpdateLayout();
}

char[][] CreateJaggedArray(string fileName)
{
    var input = File.ReadAllLines("./test.txt");
    var jaggedArray = new char[input.Length][];

    for (var i = 0; i < input.Length; i++)
        jaggedArray[i] = input[i].ToCharArray();

    return jaggedArray;
}

public class WaitingArea
{
    public WaitingArea(char[][] initialLayout)
    {
        this.initialLayout = initialLayout;
        this.newLayout = new char[initialLayout.Length][];
    }

    public enum OccupancyType
    {
        Empty = 'L',
        Occupied = '#',
        Floor = '.'
    }

    private char[][] initialLayout;
    private char[][] newLayout;

    public char[][] CurrentLayout => newLayout;

    public void UpdateLayout()
    {
        for (var i = 0; i < initialLayout.Length; i++)
        {
            if (newLayout[i] == null)
                newLayout[i] = new char[initialLayout[0].Length];

            for (var j = 0; j < initialLayout[0].Length; j++)
            {
                // set the matching position in the newlayout to the updated version of the current value
                // newLayout[i][j] = (char)CalculateNewOccupancyType(initialLayout[i][j], i, j);
                newLayout[i][j] = CalculateNewOccupancyType(initialLayout[i][j], i, j);
            }
        }
    }

    // public OccupancyType CalculateNewOccupancyType(char currentState, int i, int j)
    public char CalculateNewOccupancyType(char currentState, int i, int j)
    {
        // var stateAsEnum = Enum.Parse<OccupancyType>($"{currentState}");

        // if (stateAsEnum == OccupancyType.Floor)
        if (currentState == '.')
            return currentState;
            // return OccupancyType.Floor;

        var neighbourPositions = new int[][]
        {
            new []{i-1, j-1},
            new []{i, j-1},
            new []{i+1, j-1},
            new []{i-1, j},
            new []{i+1, j},
            new []{i-1, j+1},
            new []{i, j+1},
            new []{i+1, j+1}
        };

        var neighbours = new List<char>();

        foreach (var p in neighbourPositions)
        {
            try
            {
                neighbours.Add(initialLayout[p[0]][p[1]]);
            }
            catch
            {
                // is this wrong? this feels wrong, but looks a lot nicer than a massive if statement;
                continue;
            }
        }

        // switch (stateAsEnum)
        switch (currentState)
        {
            // case OccupancyType.Occupied:
            case '#':
                if (neighbours.Count(n => n == '#') > 3)
                    return 'L';
                    // return OccupancyType.Empty;
                break;

            case 'L':
                if (!neighbours.Any(n => n == '#'))
                    return '#';
                    // return OccupancyType.Occupied;
                break;
        }

        // return stateAsEnum;
        return currentState;
    }
}