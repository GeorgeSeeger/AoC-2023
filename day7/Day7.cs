namespace AoC_2023 {
    public class Day7 : IProblem {
        public string Name => "Day7";

        public string Part1() {
            var input = File.ReadAllLines("./day7/input");
            var hands = Parse(input);
            
            return hands.OrderBy(h => ScoreCards(h.cards))
                        .ThenBy(h => h.cards, new FirstHighestCardComparer("23456789TJQKA"))
                        .Select((hand, rank) => hand.bet * (rank + 1)).Sum().ToString();
        }

        public string Part2() {
            var input = File.ReadAllLines("./day7/input");
            var hands = Parse(input);

            return hands.OrderBy(h => ScoreCardsWithJoker(h.cards))
                        .ThenBy(h => h.cards, new FirstHighestCardComparer("J23456789TQKA"))
                        .Select((hand, rank) => hand.bet * (rank + 1)).Sum().ToString();
        }

        const string NonJokers = "23456789TQKA";

        private static int ScoreCardsWithJoker(string cards) {
            if (!cards.Contains('J')) {
                return ScoreCards(cards);
            }

            return NonJokers.Select(c => ReplaceFirst(cards, 'J', c)).Max(ScoreCardsWithJoker);
        }

        private static string ReplaceFirst(string str, char replace, char with) {
            var idx = str.IndexOf(replace);
            
            if (idx == -1) return str;
            return str.Substring(0, idx) + with + str.Substring(idx + 1);
        }

        private static (string cards, long bet)[] Parse(string[] input) {
            return input.Select(l => l.Split(" "))
                        .Select(a => (cards: a[0], bet: long.Parse(a[1])))
                        .ToArray();
        }

        private static int ScoreCards(string cards) {
            var groups = cards.GroupBy(c => c);
            var distinct = groups.Count();
            switch (distinct) { 
                case 1: // five card
                    return 6;
                case 2: 
                    if (groups.Any(g => g.Count() == 4)) { // four card
                        return 5;
                    }
                    // full house
                    return 4;
                case 3: 
                    if (groups.Any(g => g.Count() == 3)) { // three card
                        return 3;
                    }
                    // two pair
                    return 2;
                case 4: // one pair
                     return 1;
                case 5: // high card
                    return 0;
                default: 
                    throw new InvalidOperationException();
            }
        }

        class FirstHighestCardComparer : IComparer<string> {
            private readonly string cards;

            public FirstHighestCardComparer(string cards) {
                this.cards = cards;
            }

            public int Compare(string? x, string? y) {
                if (x == null || y == null) return 0;
                for (var i = 0; i < x.Length; i++) {
                    if (cards.IndexOf(x[i]) < cards.IndexOf(y[i])) 
                        return -1;
                    if (cards.IndexOf(x[i]) > cards.IndexOf(y[i])) 
                        return 1;
                }

                return 0;
            }
        }
    }
}