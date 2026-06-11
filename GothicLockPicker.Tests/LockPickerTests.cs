namespace GothicLockPicker.Tests;

public class LockPickerTests
{
    [Test]
    public async Task SolvesCavalornTower()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/CavalornTower.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/CavalornTower.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = SolutionFormatter.ToVerboseString(Bfs.Traverse(state, state.TargetStateShort())!);
        
        Assert.That(solution == expectedSolution);
    }
    
    [Test]
    public async Task SolvesYberionChest()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/YberionChest.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/YberionChest.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = SolutionFormatter.ToVerboseString(Bfs.Traverse(state, state.TargetStateShort())!);
        
        Assert.That(solution == expectedSolution);
    }
    
    [Test]
    public async Task SolvesCavalornTower_Compact()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/CavalornTower.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/CavalornTower_Compact.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = SolutionFormatter.ToCompactString(Bfs.Traverse(state, state.TargetStateShort())!);
        
        Assert.That(solution == expectedSolution);
    }
    
    [Test]
    public async Task SolvesYberionChest_Compact()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/YberionChest.txt");
        var expectedSolution = await File.ReadAllTextAsync("Solutions/YberionChest_Compact.txt");
        expectedSolution = expectedSolution.Trim();
        
        var solution = SolutionFormatter.ToCompactString(Bfs.Traverse(state, state.TargetStateShort())!);
        
        Assert.That(solution == expectedSolution);
    }
    
    [Test]
    public async Task WhenNoSolution_ReturnsEmpty()
    {
        var state = await LockReader.ReadFromFileAsync("Levels/NoSolution.txt");
        var solution = SolutionFormatter.ToVerboseString(Bfs.Traverse(state, state.TargetStateShort())!);
        
        Assert.That(solution == string.Empty);
    }
}