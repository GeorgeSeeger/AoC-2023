namespace AoC_2023 {
    public interface IProblem {
        string Name { get; }

        string Part1();

        string Part2();
    }
}