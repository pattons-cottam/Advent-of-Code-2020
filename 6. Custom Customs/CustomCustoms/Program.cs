namespace CustomCustoms
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        static void Main(string[] args)
        {
            var groupAnswers = File.ReadAllLines("./data.txt").Append("");
            var sum = CalculateSumOfGroupAnswers(groupAnswers.ToArray(), int.Parse(args[0]));

            Console.WriteLine(sum);
        }

        public static int CalculateSumOfGroupAnswers(string[] input, int puzzlePart)
        {
            var sanitised = SanitiseGroupAnswers(input, puzzlePart);

            return sanitised
                .Select(g => g.Length)
                .Aggregate((a, b) => a += b);
        }

        public static string[] SanitiseGroupAnswers(string[] input, int puzzlePart)
        {
            var answers = new string[input.Count(i => i == "")];
            var index = 0;
            var groupMembers = 0;

            foreach (var l in input)
            {
                if (l == "")
                {
                    var sanitised = puzzlePart == 1
                        ? PartOne(answers[index])
                        : PartTwo(answers[index], groupMembers);

                    if (sanitised != "")
                    {
                        answers[index] = sanitised;
                        index++;
                    }
                    else
                    {
                        answers[index] = "";
                    }

                    groupMembers = 0;

                    continue;
                }

                answers[index] += l;
                groupMembers++;
            }

            return answers
                .Where(a => a != null)
                .ToArray();
        }

        public static string PartOne(string line)
        {
            return string.Join("",
                    line.ToCharArray()
                        .OrderBy(i => i)
                        .Distinct());
        }

        public static string PartTwo(string line, int groupMembers)
        {
            return string.Join("",
                    line.GroupBy(c => c)
                        .Where(g => g.Count() == groupMembers)
                        .Select(g => g.Key)
                        .OrderBy(i => i));
        }
    }
}
