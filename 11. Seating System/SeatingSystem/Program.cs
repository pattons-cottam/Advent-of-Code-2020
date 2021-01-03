using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


PartOne();

// Action<string> print = input => Console.WriteLine(input.ToString());

void PartOne()
{
    // var initialLayout = CreateJaggedArray("./test.txt");
    var initialLayout = CreateJaggedArray("./input.txt");

    var waitingArea = new WaitingArea(initialLayout);

    waitingArea.RunSimulation();

    Console.WriteLine($"there are {waitingArea.OccupiedSeats} occupied seats");    
}

char[][] CreateJaggedArray(string fileName)
{
    var input = File.ReadAllLines(fileName);
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

    private char[][] initialLayout;
    public char[][] newLayout;
    private bool positionsChanged;

    public char[][] CurrentLayout => this.newLayout;

    public int OccupiedSeats
    {
        get 
        {
            return this.newLayout.Sum(l => l.Count(m => m == '#'));
        }
    }


    public void RunSimulation()
    {
        this.positionsChanged = true;

        while (this.positionsChanged)
        {
            UpdateLayout();
            Array.Copy(this.newLayout, this.initialLayout, this.newLayout.Length);
        }
    }

    public void UpdateLayout()
    {
        this.positionsChanged = false;

        for (var i = 0; i < this.initialLayout.Length; i++)
        {
            this.newLayout[i] = new char[this.initialLayout[0].Length];

            for (var j = 0; j < this.initialLayout[0].Length; j++)
            {
                // set the matching position in the newlayout to the updated version of the current value
                this.newLayout[i][j] = CalculateNewOccupancyType(this.initialLayout[i][j], i, j);
            }
        }
    }

    private char CalculateNewOccupancyType(char currentState, int i, int j)
    {
        if (currentState == '.')
            return currentState;

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
                neighbours.Add(this.initialLayout[p[0]][p[1]]);
            }
            catch
            {
                // is this wrong? this feels wrong, but looks a lot nicer than a massive if statement;
                continue;
            }
        }

        switch (currentState)
        {
            case '#':
                if (neighbours.Count(n => n == '#') > 3)
                {
                    this.positionsChanged = true;
                    return 'L';
                }
                break;

            case 'L':
                if (!neighbours.Any(n => n == '#'))
                {
                    this.positionsChanged = true;
                    return '#';
                }
                break;
        }

        return currentState;
    }
}