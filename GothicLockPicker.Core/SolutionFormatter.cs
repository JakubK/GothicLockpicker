using System.Text;

namespace GothicLockPicker;

public static class SolutionFormatter
{
    public static string ToCompactString(Solution? solution)
    {
        if (solution == null)
            return string.Empty;
        
        var path = new List<Move>();
        var current = solution.End;

        while (!string.IsNullOrEmpty(solution.Parent[current]))
        {
            path.Add(solution.MoveTaken[current]);
            current = solution.Parent[current];
        }

        var result = new List<(Move Value, int Count)>();
        path.Reverse();

        foreach (var move in path)
        {
            if (result.Count > 0 && result[^1].Value == move)
            {
                result[^1] = (move, result[^1].Count + 1);
            }
            else
            {
                result.Add((move, 1));
            }
        }

        var sb = new StringBuilder();
        foreach (var (move, count) in result)
        {
            sb.AppendLine(move.Index + " " + move.Direction + " x" + count);
        }
        return sb.ToString().Trim();
    }
    
    
    public static string ToVerboseString(Solution? solution)
    {
        if (solution == null)
            return string.Empty;
        
        var path = new List<Move>();
        var current = solution.End;

        while (!string.IsNullOrEmpty(solution.Parent[current]))
        {
            path.Add(solution.MoveTaken[current]);
            current = solution.Parent[current];
        }

        path.Reverse();

        var chars = new List<string>();
        var prevWsIndex = solution.StartState.Latches.Count - 1;
        
        foreach (var m in path)
        {
            if (prevWsIndex != -1)
            {
                if (m.Index > prevWsIndex)
                {
                    var diff = m.Index - prevWsIndex;
                    for (int i = 0; i < diff; i++)
                    {
                        chars.Add("S");
                    }
                } 
                else if (m.Index < prevWsIndex)
                {
                    var diff = prevWsIndex - m.Index;
                    for (int i = 0; i < diff; i++)
                    {
                        chars.Add("W");
                    }    
                }
            }

            chars.Add(m.Direction.ToString());
            
            prevWsIndex = m.Index;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < chars.Count; i += 4)
        {
            int count = Math.Min(4, chars.Count - i);
            sb.AppendLine(string.Join(" ", chars.GetRange(i, count)));
        }

        return sb.ToString().Trim();
    }
}