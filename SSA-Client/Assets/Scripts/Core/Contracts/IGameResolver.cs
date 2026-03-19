/// <summary>
/// Contract for resolving game end state (win or lose).
/// Handles screen transitions and score persistence.
/// </summary>
public interface IGameResolver
{
    /// <summary>Resolves the game as a win. Shows win screen and unlocks next level.</summary>
    void ResolveWin();

    /// <summary>Resolves the game as a loss. Shows lose screen.</summary>
    void ResolveLose();

    /// <summary>Starts the resolve sequence after a delay. Determines win/lose based on game state.</summary>
    /// <param name="delayInSeconds">Delay before resolving. Default: 2 seconds.</param>
    void StartResolveSequence(float delayInSeconds = 2f);

    /// <summary>Resolves the game immediately based on current IsGameOver state.</summary>
    void ResolveGame();
}
