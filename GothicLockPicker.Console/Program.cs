using GothicLockPicker;

var state = await LockReader.ReadFromFileAsync(args[0]);
var solution = Bfs.Traverse(state, state.TargetStateShort());
if (solution != null)
{
    Console.WriteLine(SolutionFormatter.ToVerboseString(solution));
}
else
{
    Console.WriteLine("Solution not found.");
}
