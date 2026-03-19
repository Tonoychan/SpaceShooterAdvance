using UnityEngine;

/// <summary>
/// Score persistence implementation using Unity PlayerPrefs.
/// Keys: "Score{sceneName}" and "HighScore{sceneName}".
/// </summary>
public class PlayerPrefsScorePersistence : IScorePersistence
{
    #region Public Methods

    /// <inheritdoc />
    public void SaveScore(string sceneName, int score)
    {
        PlayerPrefs.SetInt("Score" + sceneName, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + sceneName, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + sceneName, score);
        }
    }

    /// <inheritdoc />
    public int LoadScore(string sceneName)
    {
        return PlayerPrefs.GetInt("Score" + sceneName, 0);
    }

    /// <inheritdoc />
    public int LoadHighScore(string sceneName)
    {
        return PlayerPrefs.GetInt("HighScore" + sceneName, 0);
    }

    #endregion
}
