namespace GothicLockPicker;

public class Latch
{
    public int Size { get; set; }
    public int State { get; set; }

    public List<int> PositiveInfluenceLatches { get; set; } = new();
    public List<int> NegativeInfluenceLatches { get; set; } = new();

    public Latch Clone()
    {
        var clonedLatch = new Latch();

        clonedLatch.Size = Size;
        clonedLatch.State = State;
            
        foreach (var neg in NegativeInfluenceLatches)
        {
            clonedLatch.NegativeInfluenceLatches.Add(neg);
        }
            
        foreach (var pos in PositiveInfluenceLatches)
        {
            clonedLatch.PositiveInfluenceLatches.Add(pos);
        }

        return clonedLatch;
    }
}