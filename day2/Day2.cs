using AoC_2023;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Day2 : IProblem {
    public string Name => "Day2";

    public string Part1() {
        var input = File.ReadAllLines("./day2/input");
        var games = input.Select((line, i) => (rounds: Parse(line), id: i + 1));

        return games.Where(game => game.rounds.All(r => r.R <= 12 && r.G <= 13 && r.B <= 14 )).Sum(g => g.id).ToString();
    }

    private (int R, int B, int G)[] Parse(string line) {
        var rounds = line.Split(": ")[1].Split("; ");
        return rounds.Select(round =>
           (R: red.IsMatch(round) ? int.Parse(red.Match(round).Groups[1].Value) : 0,
            B: blue.IsMatch(round) ? int.Parse(blue.Match(round).Groups[1].Value) : 0,
            G: green.IsMatch(round) ? int.Parse(green.Match(round).Groups[1].Value) : 0))
            .ToArray();

    }
    
    private Regex red = new(@"(\d+) red");
    private Regex blue = new(@"(\d+) blue");
    private Regex green = new(@"(\d+) green");

    public string Part2() {
        var input = File.ReadAllLines("./day2/input");
        var games = input.Select((line, i) => (rounds: Parse(line), id: i + 1));

        var minOfEachColor = games.Select(g => (minRed: g.rounds.Max(r => r.R), minGreen: g.rounds.Max(r => r.G), minBlue: g.rounds.Max(r => r.B)));

        return minOfEachColor.Select(m => m.minRed * m.minGreen * m.minBlue).Sum().ToString();
    }
}
