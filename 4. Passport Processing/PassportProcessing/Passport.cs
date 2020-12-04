using System.Linq;
using System.Text.RegularExpressions;

namespace PassportProcessing
{
    public class Passport
    {
        public Passport(string passportString)
        {
            SetIsValid(passportString);
        }

        public bool IsValid = true;

        private string[] mandatoryFields = new string[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid"
            };

        public bool SatisfiesBasicValidity(string passportString)
        {
            return passportString
                .Split(' ')
                .Select(f => f.Split(':')[0])
                .Intersect(mandatoryFields)
                .Count() == 7;
        }

        public void SetIsValid(string passportString)
        {
            if (!this.SatisfiesBasicValidity(passportString))
            {
                this.IsValid = false;
                return;
            }

            var fields = passportString
                .Split(' ')
                .ToDictionary(
                    k => k.Split(':')[0],
                    v => v.Split(':')[1]);

            foreach (var field in fields)
            {
                switch (field.Key)
                {
                    case "byr":
                        if (int.Parse(field.Value) < 1920 || int.Parse(field.Value) > 2002)
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "iyr":
                        if (int.Parse(field.Value) < 2010 || int.Parse(field.Value) > 2020)
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "eyr":
                        if (int.Parse(field.Value) < 2020 || int.Parse(field.Value) > 2030)
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "hgt":
                        if (!Regex.IsMatch(field.Value, @"\b([\d]+)(cm|in)\b")
                            ||
                            (field.Value.Contains("cm")
                            && (int.Parse(field.Value.Replace("cm", "")) < 150
                                || int.Parse(field.Value.Replace("cm", "")) > 193))
                            ||
                            (field.Value.Contains("in")
                                && (int.Parse(field.Value.Replace("in", "")) < 59
                                    || int.Parse(field.Value.Replace("in", "")) > 76))
                            )
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "hcl":
                        if (!Regex.IsMatch(field.Value, @"(#([A-Za-z0-9]{6}))"))
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "ecl":
                        if (!new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }
                            .Contains(field.Value))
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;

                    case "pid":
                        if (!Regex.IsMatch(field.Value, @"\b([0-9]{9})\b"))
                        {
                            this.IsValid = false;
                            return;
                        }
                        break;
                }
            }

        }
    }
}