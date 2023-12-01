using System.Text.RegularExpressions;

namespace AoC_2023 {
    public class Day1 : IProblem
    {
        public string Name => "Day1";

        public string Part1() {
            var input = File.ReadAllLines("./day1/input");
            return input.Select(line => {
                var first = line.First(c => '1' <= c && c <= '9') - '0';
                var second = line.Last(c => '1' <= c && c <= '9') - '0';
                return 10 * first + second;
            }).Sum().ToString();
        }

        public string Part2() {
            var words = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            var input = File.ReadAllLines("./day1/input");
            return input.Select(line => {
                var search = words.Select(w => (word: w, index: line.IndexOf(w), lastIndex: line.LastIndexOf(w)));
                var first = words.IndexOf(search.Where(p => p.index >= 0).MinBy(p => p.index).word) % 10;
                var last = words.IndexOf(search.Where(p => p.lastIndex >= 0).MaxBy(p => p.lastIndex).word) % 10;

                return 10 * first + last;
            }).Sum().ToString();
        }
    }
}