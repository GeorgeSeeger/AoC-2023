using System.Collections.Generic;
using System.Linq;

namespace AoC_2023 {
    public static class EnumerableExtensions {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> collection, int chunkSize) {
            for (var i = 0; i < collection.Count(); i+= chunkSize) {
                yield return collection.Skip(i).Take(chunkSize);
            }
        }

        public static IEnumerable<(char val, int x, int y)> Neighbours(this string[] array, int x, int y) {
            return Neighbours(array.Select(l => l.ToCharArray()).ToArray(), x, y);
        }

        public static IEnumerable<(T val, int x, int y)> Neighbours<T>(this T[][] array, int x, int y) {
            for (var j = Math.Max(0, y - 1); j < Math.Min(y + 2, array.Length); j++) {
                for (var i = Math.Max(0, x - 1); i < Math.Min(x + 2, array[j].Length); i++) {
                    if (i == 0 && j == 0) continue;
                    yield return (array[j][i], i, j);
                }
            }
        }
    }
}