namespace GothicLockPicker;

public class State
{
    public List<Latch> Latches { get; set; } = new();

    public string CurrentStateShort()
    {
        var result = new List<string>();
        foreach (var l in Latches)
        {
            result.Add(l.State.ToString());
        }

        return string.Join("|", result);
    }

    public string TargetStateShort()
    {
        var result = new List<string>();
        foreach (var l in Latches)
        {
            var midIndex = l.Size / 2;
            result.Add(midIndex.ToString());
        }

        return string.Join("|", result);
    }

    public State Clone()
    {
        var clone = new State();
        foreach (var latchToClone in Latches)
        {
            var clonedLatch = latchToClone.Clone();
            clone.Latches.Add(clonedLatch);
        }

        return clone;
    }
}