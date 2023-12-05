namespace AoC_2023 {
    public class Day5 : IProblem {
        public string Name => "Day5";

        public string Part1() {
            var input = File.ReadAllLines("./day5/input");
            var almanac = new Almanac(input);

            return almanac.Seeds.Min(almanac.SeedToLocation).ToString();
        }

        public string Part2() {
            var input = File.ReadAllLines("./day5/input");
            var almanac = new Almanac(input);

            return almanac.JustGetMeTheAnswer().ToString();
        }

        class Almanac {
            public Almanac(string[] input) {
                this.Seeds = input[0].Split(": ")[1].Split(' ').Select(i => long.Parse(i)).ToArray();
                this.SeedRanges = this.Seeds.Chunk(2).Select(g => (start:g.First(), range:g.Last())).ToArray();

                var mapsPtr = 0;
                for (var i = 3; i < input.Length; i++) {
                    var line = input[i];

                    if (line == "") continue;
                    if (line.EndsWith("map:")) {
                        mapsPtr++;
                        continue;
                    }

                    var nums = line.Split(' ').Select(i => long.Parse(i)).ToArray();
                    Maps[mapsPtr].Add((start: nums[1], range: nums[2], dest: nums[0]));
                }
            }

            public long[] Seeds { get; }
            
            public (long start, long range)[] SeedRanges { get; }

            private List<(long start, long range, long dest)>[] Maps = Enumerable.Range(0, 7).Select(_ => new List<(long, long, long)>()).ToArray();

            private long Lookup(List<(long start, long range, long dest)> ranges, long key) {
                var inRange = ranges.FirstOrDefault(r => r.start <= key && key < r.start + r.range);
                if (inRange != default) {
                    return inRange.dest + key - inRange.start;
                }

                return key;
            }
            
            private long ReverseLookup(List<(long start, long range, long dest)> ranges, long key) {
                var inRange = ranges.FirstOrDefault(r => r.dest <= key && key < r.dest + r.range);
                if (inRange != default) return inRange.start + key - inRange.dest;

                return key;
            }

            public long SeedToLocation(long seed) {
                var key = seed;
                foreach (var ranges in Maps) {
                    key = this.Lookup(ranges, key);
                }
                return key;
            }

            public long JustGetMeTheAnswer() {
                var loc = -1L;
                var seed = 0L;
                do {
                    loc++;
                    seed = loc;
                    foreach (var range in Maps.Reverse()) {
                        seed = ReverseLookup(range, seed);
                    }
                } while (!this.SeedRanges.Any(sr => sr.start <= seed && seed < sr.start + sr.range));

                return loc;
            }
        }
    }
}