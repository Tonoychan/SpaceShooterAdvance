using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Displays last score and high score from PlayerPrefs when enabled.
/// Used on level select or end screens.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    #endregion

    #region Unity Messages

    private void OnEnable()
    {
        var score = PlayerPrefs.GetInt("Score" + (FadeCanvas.CurrentLevelData?.levelId ?? "Unknown"), 0);
        scoreText.text = $"Score: {score}";

        var highScore = PlayerPrefs.GetInt("HighScore" + (FadeCanvas.CurrentLevelData?.levelId ?? "Unknown"), 0);
        highScoreText.text = $"High Score: {highScore}";
    }

    #endregion
}
