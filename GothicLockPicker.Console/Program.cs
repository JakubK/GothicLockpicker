using GothicLockPicker;

var state = await LockReader.ReadFromFileAsync(args[0]);
Console.WriteLine(BFS.Traverse(state, state.TargetStateShort()));