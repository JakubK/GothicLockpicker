using GothicLockPicker;

var state = await LockReader.ReadFromFileAsync(args[0]);
var solution = Bfs.Traverse(state, state.TargetStateShort());
if (solution != null)
{
    if (args.Contains("--compact"))
    {
        Console.WriteLine(SolutionFormatter.ToCompactString(solution));
    }
    else
    {
        Console.WriteLine(SolutionFormatter.ToVerboseString(solution));
    }
}
else
{
    Console.WriteLine("Solution not found.");
}
