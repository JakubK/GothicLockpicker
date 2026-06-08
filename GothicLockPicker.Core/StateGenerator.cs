namespace GothicLockPicker;

public static class StateGenerator
{
    /// <summary>
    /// Produces new state based on input state and move
    /// Assuming the move is valid
    /// </summary>
    /// <param name="inputState"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    public static State StateFromMove(State inputState, Move move)
    {
        var clone = inputState.Clone();

        var masterLatch = clone.Latches[move.Index];
        
        if (move.Direction == MoveDirection.A)
        {
            masterLatch.State++;
            foreach (var posIndex in masterLatch.PositiveInfluenceLatches)
            {
                clone.Latches[posIndex].State++;
            }
            foreach (var negIndex in masterLatch.NegativeInfluenceLatches)
            {
                clone.Latches[negIndex].State--;
            }
        }
        else
        {
            masterLatch.State--;
            foreach (var posIndex in masterLatch.PositiveInfluenceLatches)
            {
                clone.Latches[posIndex].State--;
            }
            foreach (var negIndex in masterLatch.NegativeInfluenceLatches)
            {
                clone.Latches[negIndex].State++;
            }
        }

        return clone;
    }
    
    public static List<Move> GetNextMoves(State inputState)
    {
        // for each latch l in the input state:
        // for each direction d (A,D):
        // if l on d will not move l out of bounds
        // AND influenced moves i will not be out of bounds
        // THEN (l,d) is a new MOVE
        
        var dirs = new List<MoveDirection>
        {
            MoveDirection.A,
            MoveDirection.D
        };

        var legalMoves = new List<Move>();
        for (var l = 0; l < inputState.Latches.Count; l++)
        {
            foreach (var d in dirs)
            {
                var move = new Move(l, d);
                if (CanMoveLatch(inputState, new Move(l, d)))
                {
                    legalMoves.Add(move);
                }
            }
        }

        return legalMoves;
    }

    /// <summary>
    /// Checks if it's possible to move a Latch and the ones connected to it
    /// Connected slave latches will be evaluated without their connections 
    /// </summary>
    /// <param name="inputState"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    public static bool CanMoveLatch(State inputState, Move move)
    {
        var canMoveNonInfluence = CanMoveLatchWithoutCheckingInfluence(inputState, move);
        if (!canMoveNonInfluence)
        {
            return false;
        }

        var l = inputState.Latches[move.Index];
        
        foreach (var posIndex in l.PositiveInfluenceLatches)
        {
            var posMove = move with { Index = posIndex };
            if (!CanMoveLatchWithoutCheckingInfluence(inputState, posMove))
            {
                return false;
            }
        }
        
        foreach (var negIndex in l.NegativeInfluenceLatches)
        {
            var negDir = move.Direction == MoveDirection.D ? MoveDirection.A : MoveDirection.D;
            var negMove = new Move(negIndex, negDir);
            if (!CanMoveLatchWithoutCheckingInfluence(inputState, negMove))
            {
                return false;
            }
        }
        
        return true;
    }
    
    
    /// <summary>
    /// Checks if it's possible to move given Latch without evaluating any Latches connected to it.
    /// </summary>
    /// <param name="inputState"></param>
    /// <param name="move"></param>
    /// <returns></returns>
    public static bool CanMoveLatchWithoutCheckingInfluence(State inputState, Move move)
    {
        if (move.Direction == MoveDirection.D)
        {
            return inputState.Latches[move.Index].State > 0;
        }
        
        return inputState.Latches[move.Index].State < inputState.Latches[move.Index].Size - 1;
    }
}