namespace GothicLockPicker;

public static class Bfs
{
    public static Solution? Traverse(State startState, string targetShort)
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
                
                return new(currentKey, parent, moveTaken, startState); 
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

        return null;
    }
}