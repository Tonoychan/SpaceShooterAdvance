/// <summary>
/// Contract for reading game status. Used to check if the game has ended.
/// </summary>
public interface IGameStatusReader
{
    /// <summary>True when the game has ended (player lost).</summary>
    bool IsGameOver { get; }
}
