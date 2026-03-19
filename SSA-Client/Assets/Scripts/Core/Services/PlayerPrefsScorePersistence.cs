using UnityEngine;

public class PlayerPrefsScorePersistence :IScorePersistence
{
    public void SaveScore(string sceneName, int score)
    {
        PlayerPrefs.SetInt("Score" + sceneName, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + sceneName, 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + sceneName, score);
        }
    }

    public int LoadScore(string sceneName)
    {
        return PlayerPrefs.GetInt("Score" + sceneName, 0);
    }

    public int LoadHighScore(string sceneName)
    {
        return PlayerPrefs.GetInt("HighScore" + sceneName, 0);
    }
}
