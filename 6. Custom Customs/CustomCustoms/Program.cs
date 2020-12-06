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
            var sum = CalculateSumOfGroupAnswers(groupAnswers.ToArray());

            Console.WriteLine(sum);
        }

        public static int CalculateSumOfGroupAnswers(string[] input)
        {
            var sanitised = SanitiseGroupAnswers(input);

            return sanitised
                .Select(g => g.Length)
                .Aggregate((a, b) => a += b);
        }

        public static string[] SanitiseGroupAnswers(string[] input)
        {
            var answers = new string[input.Count(i => i == "")];
            var index = 0;

            foreach (var l in input)
            {
                if (l == "")
                {
                    answers[index] = string.Join("",
                        answers[index]
                            .ToCharArray()
                            .OrderBy(i => i)
                            .Distinct());

                    index++;
                    continue;
                }

                answers[index] += l;
            }

            return answers;
        }
    }
}
