namespace AoC_2023 {
    using System.Linq;

    public class Day9 : IProblem {
        public string Name => "Day9";

        public string Part1() {
            var input = Parse(File.ReadAllLines("./day9/input"));

            return input.Select(PredictNext).Sum().ToString();
        }
        public string Part2() {
            var input = Parse(File.ReadAllLines("./day9/input"));

            return input.Select(PredictPrev).Sum().ToString();
        }

        private IEnumerable<int[]> Parse(string[] input) => input.Select(line => line.Split(' ').Select(i => int.Parse(i)).ToArray());

        private static int PredictPrev(int[] vals) {
            var terms = new List<int[]>();
            var diffed = Differentiate(vals);
            while (diffed.Any(i => i != 0)) {
                terms.Add(diffed);
                diffed = Differentiate(diffed);
            }

            return terms.Prepend(vals)
                        .Select(t => t.First())
                        .Reverse()
                        .Aggregate((acc, i) => i - acc);
        }
        private static int PredictNext(int[] vals) {
            var terms = new List<int[]>();
            var diffed = Differentiate(vals);
            while (diffed.Any(i => i != 0)) {
                terms.Add(diffed);
                diffed = Differentiate(diffed);
            }

            return vals.Last() + terms.Sum(t => t.Last());
        }

        private static int[] Differentiate(int[] vals) {
            return vals.Select((n, i) => (n, i)).Aggregate(new List<int>(), (acc, pair) => {
                if (pair.i > 0)
                    acc.Add(pair.n - vals[pair.i - 1]);
                return acc;
            }).ToArray();
        }
    }
}