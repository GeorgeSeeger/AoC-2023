using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AoC_2023 {
    public class Day8 : IProblem {
        public string Name => "Day8";

        public string Part1() {
            var input = File.ReadAllLines("./day8/input");
            var instr = input[0];
            var nodes = Parse(input);

            var steps = CountStepsToEnd(instr, nodes, "AAA", l => l == "ZZZ");

            return steps.ToString();
        }

        public string Part2() {
            var input = File.ReadAllLines("./day8/input");
            var instr = input[0];
            var nodes = Parse(input);

            var starts = nodes.Keys.Where(l => l[2] == 'A');
            var endsAfter = starts.Select(st => (long)CountStepsToEnd(instr, nodes, st, l => l[2] == 'Z'));

            var lcm = endsAfter.Aggregate(LowestCommonMultiple);
            return lcm.ToString();
        }

        private static int CountStepsToEnd(string instr, Dictionary<string, (string left, string right)> nodes, string start, Func<string, bool> isAtEnd) {
            var i = 0;
            var steps = 0;
            while (!isAtEnd(start)) {
                var direction = instr[i++ % instr.Length];
                if (direction == 'L') {
                    start = nodes[start].left;
                } else {
                    start = nodes[start].right;
                }
                steps++;
            }

            return steps;
        }

        private Dictionary<string, (string left, string right)> Parse(string[] input) {
            var matcher = new Regex(@"\(([A-Z0-9]{3}), ([A-Z0-9]{3})\)");
            return input.Skip(2).ToDictionary(line => line.Split(" = ")[0], line => {
                var nodes = matcher.Match(line.Split(" = ")[1]).Groups;
                return (nodes[1].Value, nodes[2].Value);
            });
        }

        private static long LowestCommonMultiple(long a, long b) {
            return (a / GreatestCommonFactor(a, b)) * b;
        }

        private static long GreatestCommonFactor(long a, long b) {
            while (b != 0) {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}