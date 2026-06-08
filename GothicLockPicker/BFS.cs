namespace GothicLockPicker;

public static class BFS
{
    public static void Traverse(State startState, string targetShort)
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
                PrintPath(currentKey, parent, moveTaken, startState);
                return;
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

        Console.WriteLine("No solution found.");
    }
    
    private static void PrintPath(
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

        Console.WriteLine($"Solution in {path.Count} moves:");

        var index = 0;
        var debugState = startState.Clone();

        var chars = new List<string>();

        var prevWSIndex = startState.Latches.Count - 1;
        foreach (var m in path)
        {
            index++;
            debugState = StateGenerator.StateFromMove(debugState, m);

            if (prevWSIndex != -1)
            {
                if (m.Index > prevWSIndex)
                {
                    var diff = m.Index - prevWSIndex;
                    for (int i = 0; i < diff; i++)
                    {
                        chars.Add("S");
                    }
                } 
                else if (m.Index < prevWSIndex)
                {
                    var diff = prevWSIndex - m.Index;
                    for (int i = 0; i < diff; i++)
                    {
                        chars.Add("W");
                    }    
                }
            }
            // Console.WriteLine($"{index} Index {m.Index}, Direction {m.Direction} -> {debugState.CurrentStateShort()}");

            chars.Add(m.Direction.ToString());
            
            prevWSIndex = m.Index;
        }
        
        for (int i = 0; i < chars.Count; i += 4)
        {
            int count = Math.Min(4, chars.Count - i);
            Console.WriteLine(string.Join(" ", chars.GetRange(i, count)));
        }

        Console.WriteLine(chars.Count);
    }
}