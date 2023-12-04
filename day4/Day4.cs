namespace AoC_2023 {
    public class Day4 : IProblem {
        public string Name => "Day4";

        public string Part1() {
            var input = File.ReadAllLines("./day4/input").Select(ParseCard);
            return input.Sum(card => Score(card.winningNumbers)).ToString();
            
            int Score(int n) {
                return n == 0 ? 0 : 1 << n - 1;
            }
        }

        public string Part2() {
            var input = File.ReadAllLines("./day4/input").Select(ParseCard);
            var copies = input.Select(c => 1).ToArray();

            foreach (var (id, winningNumbers) in input) {
                for (var i = id; i < id + winningNumbers; i++) {
                    copies[i] += copies[id - 1];
                }
            }

            return copies.Sum().ToString();
        }

        private static (int id, int winningNumbers) ParseCard(string line, int i) {
            var nums = line.Split(": ")[1].Split(" | ").Select(str => str.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n))).ToArray();
            var winners = nums[0].ToHashSet();
            return (id: i + 1, winningNumbers: nums[1].Count(n => winners.Contains(n)));
        }
    }
}