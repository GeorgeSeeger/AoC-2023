namespace AoC_2023 {
    public class Day12 : IProblem {
        public string Name => "Day12";

        public string Part1() {
            var input = File.ReadAllLines("./day12/test").Select(Parse);
            
            return input.Select(line => Solve(line.springs, line.working.First(), line.working.Skip(1))).Sum().ToString();
        }

        private int Solve(IEnumerable<char> springs, int? v, IEnumerable<int> working) {
            if (!springs.Any()) return 0;
            if (!working.Any()) return 0;
            if (!v.HasValue) return 0;

            switch (springs.First()) {
                case '#':
                    return Solve(springs.Skip(1), v - 1, working);
                case '.':
                    return Solve(springs.Skip(1), v, working);
                case '?':
                    return Solve(springs.Skip(1).Prepend('.'), v, working.Skip(1))
                        + Solve(springs.Skip(1).Prepend('#'), v, v == 1 ? working.Skip(1) : working);
            }

            throw new ArgumentOutOfRangeException();
        }

        private (string springs, int[] working) Parse(string line) {
            var sides = line.Split(' ');
            var working = sides[1].Split(',').Select(i => int.Parse(i)).ToArray();
            return (sides[0], working);
        }

        public string Part2() {
            throw new NotImplementedException();
        }
    }
}
