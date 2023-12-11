using System.Data;

namespace AoC_2023 {
    public class Day11 : IProblem {
        public string Name => "Day11";

        public string Part1() {
            var input = File.ReadAllLines("./day11/input");
            var galaxyLocations = Parse(input);
            var expanded = Expanded(input, 2);

            return SumSmallestDistances(galaxyLocations, expanded).ToString();
        }

        public string Part2() {
              var input = File.ReadAllLines("./day11/input");
            var galaxyLocations = Parse(input);
            var expanded = Expanded(input, 1_000_000);

            return SumSmallestDistances(galaxyLocations, expanded).ToString();
        }

        private static long SumSmallestDistances((int y, int x)[] galaxyLocations, (Dictionary<int, int> RowDistance, Dictionary<int, int> ColDistance) expanded) {
            var sumOfDistances = 0L;
            var compared = new HashSet<((int, int), (int, int))>();
            
            foreach (var galaxyA in galaxyLocations) {
                foreach (var galaxyB in galaxyLocations) {
                    if (galaxyA == galaxyB) continue;
                    if (compared.Contains((galaxyA, galaxyB)) || compared.Contains((galaxyB, galaxyA))) continue;

                    var distanceBetween =
                        Enumerable.Range(Math.Min(galaxyA.y, galaxyB.y) + 1, Math.Abs(galaxyA.y - galaxyB.y))
                        .Select(y => expanded.RowDistance.TryGetValue(y, out var d) ? d : 1).Sum()
                        + Enumerable.Range(Math.Min(galaxyA.x, galaxyB.x) + 1, Math.Abs(galaxyA.x - galaxyB.x))
                        .Select(x => expanded.ColDistance.TryGetValue(x, out var d) ? d : 1).Sum();
                    sumOfDistances += distanceBetween;
                    compared.Add((galaxyA, galaxyB));
                }
            }

            return sumOfDistances;
        }

        private (int y, int x)[] Parse(string[] input) {
            return input.SelectMany((l, y) => l.Select((c, x) => (c, y, x)))
                .Where(p => p.c == '#')
                .Select(p => (p.y, p.x))
                .ToArray();
        }

        private (Dictionary<int, int> RowDistance, Dictionary<int, int> ColDistance) Expanded(string[] input, int expandBy = 2) {
            var rows = new Dictionary<int, int>();
            var cols = new Dictionary<int, int>();
            for (var y = 0; y < input.Length; y++) {
                if (input[y].All(c => c == '.'))
                    rows.Add(y, expandBy);
            }

            for (var x = 0; x < input[0].Length; x++) {
                if (input.All(l => l[x] == '.')) {
                    cols.Add(x, expandBy);
                }
            }

            return (rows, cols);
        }
    }

}