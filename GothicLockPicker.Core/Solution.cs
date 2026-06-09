namespace GothicLockPicker;

public record Solution(string End, Dictionary<string, string> Parent, Dictionary<string, Move> MoveTaken, State StartState);