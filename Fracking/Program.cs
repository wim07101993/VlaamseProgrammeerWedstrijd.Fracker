using System;
using System.Collections.Generic;
using System.Linq;

namespace Fracking
{
    internal class Program
    {
        private static readonly string[] Input =
        {
            "2",
            "4",
            "13",
            "*****..***...",
            "*****.*****..",
            "*****.*****..",
            ".***..*****..",
            "5",
            "6",
            "..**..",
            "..*...",
            "..**..",
            "...*..",
            "...*.."
        };


        private static void Main()
        {
            PrintInput();
            var numberOfTests = GetNumberOfTests();
            var maps = ParseMaps(Input, numberOfTests).ToList();
            SimulateMap(maps);

            Console.ReadKey();
        }

        private static void SimulateMap(IReadOnlyList<Map> maps)
        {
            for (var i = 0; i < maps.Count; i++)
            {
                var map = maps[i];
                Console.WriteLine($"Calculating test{i + 1}");
                while (map.MoveNext())
                {
                }

                Console.WriteLine($"{i + 1} {map.Repetitions}");
            }
        }

        private static void PrintInput()
        {
            foreach (var s in Input)
                Console.WriteLine(s);
        }

        private static int GetNumberOfTests()
        {
            var numberOfTests = int.Parse(Input[0]);
            Console.WriteLine($"Number of tests: {numberOfTests}");
            return numberOfTests;
        }

        private static IEnumerable<Map> ParseMaps(IReadOnlyList<string> input, int numberOfTests)
        {
            var testStart = 1;
            for (var i = 0; i < numberOfTests; i++)
            {
                Console.WriteLine($"TEST{i + 1}");

                var rows = int.Parse(input[testStart]);
                Console.WriteLine($"Rows: {rows}");

                var columns = int.Parse(input[testStart + 1]);
                Console.WriteLine($"Columns: {columns}");

                var bs = new List<IEnumerable<bool>>();
                for (var j = 0; j < rows; j++)
                    bs.Add(input[testStart + 2 + j].Select(x => x == '*'));

                var map = new Map(bs);
                yield return map;

                Console.WriteLine("Map:");
                Map.WriteMap(map);

                testStart += rows + 2;
            }
        }
    }
}