namespace GothicLockPicker;

public class Latch
{
    public int Size { get; set; }
    public int State { get; set; }

    public List<int> PositiveInfluenceLatches { get; set; } = new();
    public List<int> NegativeInfluenceLatches { get; set; } = new();
}