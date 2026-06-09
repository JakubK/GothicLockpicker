namespace GothicLockPicker.Tests;

public class LockPickerTests
{
    [Test]
    public async Task SolvesCavalornTower()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/CavalornTower.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/CavalornTower.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = BFS.Traverse(state, state.TargetStateShort()).Trim();
        
        Assert.That(solution == expectedSolution);
    }
    
    [Test]
    public async Task SolvesYberionChest()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/YberionChest.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/YberionChest.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = BFS.Traverse(state, state.TargetStateShort()).Trim();
        
        Assert.That(solution == expectedSolution);
    }
}