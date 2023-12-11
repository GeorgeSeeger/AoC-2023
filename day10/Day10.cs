using System.Xml.Schema;

namespace AoC_2023 {
    public class Day10 : IProblem {
        public string Name => "Day10";

        public string Part1() {
            var input = File.ReadAllLines("./day10/input");
            var start = input.SelectMany((l, y) => l.Select((c, x) => (c, y, x))).Single(val => val.c == 'S');
            var pos = (start.y, start.x);

            var pipes = new[] { North, South, East, West }
                                    .Select(dir => (dir, pos: (y: pos.y + dir.dy, x: pos.x + dir.dx)))
                        .Where(step => GetNextDirection(GetPipe(input, step.pos), step.dir) != default)
                        .ToArray();
            var pipePath = Follow(input, pipes.First(), pos);

            return ((pipePath.Count()) / 2).ToString();
        }

        public string Part2() {
            var isTest = true;
            var path = isTest ? "./day10/test2" : "./day10/input";
            var input = File.ReadAllLines(path);
            var start = input.SelectMany((l, y) => l.Select((c, x) => (c, y, x))).Single(val => val.c == 'S');
            var pos = (start.y, start.x);

            var stepsFromStart = new[] { North, South, East, West }
                                                .Select(dir => (dir, pos: (y: pos.y + dir.dy, x: pos.x + dir.dx)));
            var pipes = stepsFromStart
                        .Where(step => GetNextDirection(GetPipe(input, step.pos), step.dir) != default)
                        .ToArray();
            var pipePath = Follow(input, pipes.First(), pos).ToHashSet();
            // todo replace start piece
            var actualStartPipe = isTest ? '7' : 'J';
            input[start.y] = input[start.y].Replace('S', actualStartPipe);

            // todo flash fill if not on pipe path
            var enclosedCounter = 0;
            // for (var y = 0; y < input.Length; y++) {
            //     var insideLoop = false;
            //     for (var x = 0; x < input[y].Length; x++) {
            //         if (pipePath.Contains((y,x))) {
            //             var piece = GetPipe(input, (y, x));
            //             if (piece == 'F' || piece == 'L') {
            //                 insideLoop = true;
            //             } else if (piece == '7' || piece == 'J') {
            //                 insideLoop = false;
            //             } else if (piece == '-') {
            //                 continue;
            //             } 
            //             else if (piece == '|') {
            //                 insideLoop = !insideLoop;
            //             }
            //         }
                    
            //          else if (insideLoop) {
            //             enclosedCounter++;
            //         }
            //     }
            // }

            return enclosedCounter.ToString();
        }

        private IEnumerable<(int y, int x)> Follow(string[] input, ((int dy, int dx) dir, (int y, int x) pos) step, (int y, int x) end) {
            var path = new List<(int, int)>();
            while (step.pos != end) {
                path.Add(step.pos);
                step.dir = GetNextDirection(GetPipe(input, step.pos), step.dir);
                step.pos = (step.pos.y + step.dir.dy, step.pos.x + step.dir.dx);
            }

            path.Add(end);
            return path;
        }

        private static char GetPipe(string[] input, (int y, int x) p) => input[p.y][p.x];

        private static (int dy, int dx) GetNextDirection(char c, (int dy, int dx) dir) {
            switch (c) {
                case 'J' when dir == East: return North;
                case 'J' when dir == South: return West;
                case 'F' when dir == North: return East;
                case 'F' when dir == West: return South;
                case 'L' when dir == West: return North;
                case 'L' when dir == South: return East;
                case '7' when dir == East: return South;
                case '7' when dir == North: return West;
                case '|' when dir == South: return South;
                case '|' when dir == North: return North;
                case '-' when dir == East: return East;
                case '-' when dir == West: return West;
            }

            return default;
        }

        static (int dy, int dx) North => (-1, 0);
        static (int dy, int dx) East => (0, 1);
        static (int dy, int dx) South => (1, 0);
        static (int dy, int dx) West => (0, -1);
    }

}