/// <summary>
/// Contract for persisting and loading scores (e.g. PlayerPrefs, cloud).
/// </summary>
public interface IScorePersistence
{
    /// <summary>Saves the score for the given scene. Updates high score if applicable.</summary>
    void SaveScore(string sceneName, int score);

    /// <summary>Loads the last score for the given scene.</summary>
    int LoadScore(string sceneName);

    /// <summary>Loads the high score for the given scene.</summary>
    int LoadHighScore(string sceneName);
}
