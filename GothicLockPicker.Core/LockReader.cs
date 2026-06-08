namespace GothicLockPicker;

public static class LockReader
{
    /// <summary>
    /// Path to text file containing encoded Lock
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static async Task<State> ReadFromFileAsync(string path)
    {
        var data = await File.ReadAllLinesAsync(path);
        var state = new State();

        foreach (var line in data)
        {
            if (line.Contains("|"))
            {
                // Latch info
                var latchLine = line.Split("|");
                var size = int.Parse(latchLine[0].Trim());
                var startPos = int.Parse(latchLine[1].Trim());
        
                state.Latches.Add(new Latch
                {
                    Size = size,
                    State = startPos
                });
            } 
            else if (line.Contains("+"))
            {
                // Positive Constrain
                var constrainLine = line.Split("+");
                var moveOnIndex = int.Parse(constrainLine[0].Trim());
                var influenceOnIndex = int.Parse(constrainLine[1].Trim());
        
                state.Latches[moveOnIndex].PositiveInfluenceLatches.Add(influenceOnIndex);
            } 
            else if (line.Contains("-"))
            {
                // Negative Constrain
                var constrainLine = line.Split("-");
                var moveOnIndex = int.Parse(constrainLine[0].Trim());
                var influenceOnIndex = int.Parse(constrainLine[1].Trim());
        
                state.Latches[moveOnIndex].NegativeInfluenceLatches.Add(influenceOnIndex);
            }
        }

        return state;
    }
}