using System.Text;

namespace GothicLockPicker;

public static class BFS
{
    public static string Traverse(State startState, string targetShort)
    {
        var visited = new HashSet<string>();
        var parent = new Dictionary<string, string>();
        var moveTaken = new Dictionary<string, Move>();

        var queue = new Queue<State>();
        queue.Enqueue(startState);

        var startKey = startState.CurrentStateShort();
        visited.Add(startKey);
        parent[startKey] = null;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var currentKey = current.CurrentStateShort();

            if (currentKey == targetShort)
            {
                return PrintPath(currentKey, parent, moveTaken, startState); 
            }

            foreach (var move in StateGenerator.GetNextMoves(current))
            {
                var nextState = StateGenerator.StateFromMove(current, move);
                var nextKey = nextState.CurrentStateShort();

                if (visited.Add(nextKey))
                {
                    queue.Enqueue(nextState);

                    parent[nextKey] = currentKey;
                    moveTaken[nextKey] = move;
                }
            }
        }

        return "No solution found.";
    }
    
    private static string PrintPath(
        string end,
        Dictionary<string, string> parent,
        Dictionary<string, Move> moveTaken,
        State startState)
    {
        var path = new List<Move>();
        var current = end;

        while (parent[current] != null)
        {
            path.Add(moveTaken[current]);
            current = parent[current];
        }

        path.Reverse();
        
        Move? previousMove = null;
        var sb = new StringBuilder();
        var count = 1;
        
        foreach (var m in path)
        {
            if (previousMove != null && previousMove.Index == m.Index)
            {
                count++;
            }

            if (previousMove != null && previousMove.Index != m.Index)
            {
                var formatCount = new string(previousMove.Direction.ToString()[0], count);
                sb.AppendLine(previousMove.Index + " " + formatCount);
                
                count = 1;
            }
            
            previousMove = m;
        }


        return sb.ToString();
    }
}