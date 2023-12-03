namespace AoC_2023 {
    public class Day3 : IProblem {
        public string Name => "Day3";

        public string Part1() {
            var input = File.ReadAllLines("./day3/input");
            var numbers = input.Select((line, y) => ParseLine(input, line, y));

            return numbers.SelectMany(p => p).Where(p => p.isPart).Sum(p => p.num).ToString();
        }

        private (int num, bool isPart, (int x, int y) nearestAsterisk)[] ParseLine(string[] input, string line, int lineNum) {
            var partNum = (num: 0, isPart: false, nearestAsterisk: (0, 0));
            var nums = new List<(int, bool, (int, int))>();

            for (var i = 0; i < line.Length; i++) {
                var c = line[i];
                if ('0' <= c && c <= '9') {
                    var num = 10 * partNum.num + c - '0';
                    partNum = (num,
                    partNum.isPart || input.Neighbours(i, lineNum).Any(c => !char.IsNumber(c.val) && c.val != '.'),
                    partNum.nearestAsterisk);

                    if (partNum.nearestAsterisk == default) {
                        var nearest = input.Neighbours(i, lineNum).FirstOrDefault(c => c.val == '*');
                        if (nearest != default) {
                            partNum = (partNum.num, partNum.isPart, (nearest.x, nearest.y));
                        }
                    }
                } else {
                    if (partNum != default) {
                        nums.Add(partNum);
                    }

                    partNum = default;
                }
            }

            if (partNum != default) {
                nums.Add(partNum);
            }

            return nums.ToArray();
        }

        public string Part2() {
            var input = File.ReadAllLines("./day3/input");
            var parts = input.Select((line, y) => ParseLine(input, line, y));

            return parts.SelectMany(p => p)
                        .GroupBy(p => p.nearestAsterisk)
                        .Where(g => g.Count() == 2)
                        .Select(g => g.Aggregate(1, (agg, p) => agg * p.num))
                        .Sum()
                        .ToString();
        }
    }

}