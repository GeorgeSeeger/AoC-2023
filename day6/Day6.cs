namespace AoC_2023 {
    public class Day6 : IProblem {
        public string Name => "Day6";

        public string Part1() {
            var input = File.ReadAllLines("./day6/input");
            var raceRecords = ParsePt1(input);
            return raceRecords.Select(rr => Enumerable.Range(0, rr.time).Select(tHeld => {
                var speed = tHeld;
                var time = rr.time - tHeld;
                return speed * time > rr.dist ? 1 : 0;
            }).Sum())
            .Aggregate((agg, n) => agg * n)
            .ToString();
        }

        private (int time, int dist)[] ParsePt1(string[] input) {
            var times = input[0].Split(": ")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n));
            var dist = input[1].Split(": ")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n));
            return times.Zip(dist, (t, d) => (t, d)).ToArray();
        }

        public string Part2() {
            var input = File.ReadAllLines("./day6/input");
            var raceRecord = input.Select(line => long.Parse(line.Split(": ")[1].Replace(" ", ""))).ToArray();
            var (time, distance) = (raceRecord[0], raceRecord[1]);

            var speedLowerBound = Math.Ceiling((time - Math.Sqrt(time * time - 4 * distance)) / 2);
            var timeUpperBound = Math.Floor((time + Math.Sqrt(time * time - 4 * distance)) / 2);
            
            return (timeUpperBound - speedLowerBound + 1).ToString();
        }
    }
}