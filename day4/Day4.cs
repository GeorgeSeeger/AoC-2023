namespace AoC_2023 {
    public class Day4 : IProblem {
        public string Name => "Day4";

        public string Part1() {
            var input = File.ReadAllLines("./day4/input").Select(ParseCard);
            return input.Sum(card => Score(card.entries.Count(e => card.winners.Contains(e)))).ToString();
            
            int Score(int n) {
                return n == 0 ? 0 : n == 1 ? 1 : 1 << n - 1;
            }
        }

        public string Part2() {
            var input = File.ReadAllLines("./day4/input").Select(ParseCard);
            var copies = input.Select(c => 1).ToArray();

            foreach (var card in input) {
                var cardWinners = card.entries.Count(e => card.winners.Contains(e));
                for (var i = card.id; i < card.id + cardWinners; i++) {
                    copies[i] += 1 * copies[card.id - 1];
                }
            }

            return copies.Sum().ToString();
        }

        private static (int id, HashSet<int> winners, int[] entries) ParseCard(string line, int i) {
            var nums = line.Split(": ")[1].Split(" | ").Select(str => str.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n))).ToArray();
            return (id: i + 1, winners: nums[0].ToHashSet(), entries: nums[1].ToArray());
        }

        private 

    }
}