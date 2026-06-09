# GothicLockPicker

Helper .NET Console Application for saving time solving Gothic Remake Locks.
It also lets you save skill points on something more valuable than lockpicking.

## Usage

```bash
./GothicLockPicker.Console CavalornTower.txt 
```

Where `GothicLockPicker.Console` is compiled binary of the app, and CavalornTower.txt is encoded lock data representing the entire puzzle.

## Format

Each line is either a declaration of Latch, or a declaration of relationship between existing Latches.

Declaration of Latch starts with number of holes in it and its starting position from left counting from 0, both values separated by '|'.

Declaration of Relationship starts with master Latch index counting from 0 (topmost latch is 0) and then comes slave Latch index.
If moving master latch moves slave latch in the same direction, then separator is '+', if the direction is opposite, then its '-'.

### Format Example

```
7|0
7|0
7|1
7|1
7|5
1-0
1-3
1+2
```

Lock consists of 5 latches, each has 7 holes. 2 topmost latches are at position 0
Moving Latch1 to any direction moves Latch0 to the opposite.
Moving Latch1 to any direction moves Latch2 to the same direction.

## Output
Solution is always a set of W,A,S,D keys to press to solve the puzzle. Groupped in fours to not get lost in the middle.

### Example output

Below you'll find solution for lock to the tower nearby Cavalorn Camp.
Each line starts with latch Id (starting from topmost latch as 0) and a letters to press while on them.

```
2 D
3 A
2 D
3 A
2 D
3 AAA
4 A
1 D
0 D
4 A
1 D
0 D
4 A
1 D
4 A
1 D
4 A
1 D
4 A
1 D
4 A
1 D
4 A
1 D
4 AAA
5 D
0 D
5 D
0 D
5 D
0 D
```

