/// <summary>
/// Contract for writing and tracking score during gameplay.
/// </summary>
public interface IScoreWriter
{
    /// <summary>Current score value.</summary>
    int CurrentScore { get; }

    /// <summary>Adds value to the current score.</summary>
    /// <param name="value">Points to add.</param>
    void AddScore(int value);

    /// <summary>Resets the score to zero.</summary>
    void ResetScore();
}
