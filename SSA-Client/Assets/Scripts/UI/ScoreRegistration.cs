using UnityEngine;
using TMPro;

/// <summary>
/// Subscribes to GameSessionService.ScoreChanged and updates a TextMeshProUGUI with live score.
/// Unsubscribes on destroy to prevent leaks.
/// </summary>
public class ScoreRegistration : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private GameSessionService gameSessionService;

    #endregion

    #region Private Fields

    private TextMeshProUGUI scoreText;
    private bool subscribed;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null) return;

        var service = ResolveGameSessionService();
        if (service != null)
        {
            gameSessionService = service;
            gameSessionService.ScoreChanged += OnScoreChanged;
            subscribed = true;
            scoreText.text = $"Score: {gameSessionService.CurrentScore}";
        }
        else
        {
            scoreText.text = "Score: 0";
        }
    }

    private void OnDestroy()
    {
        if (subscribed && gameSessionService != null)
        {
            gameSessionService.ScoreChanged -= OnScoreChanged;
        }
    }

    /// <summary>
    /// Resolves the active GameSessionService. Prefers EndGameManager's persisted instance
    /// (used when loading from level selection) over the serialized reference.
    /// </summary>
    private GameSessionService ResolveGameSessionService()
    {
        if (EndGameManager.Instance != null && EndGameManager.Score is GameSessionService gs)
            return gs;
        return gameSessionService;
    }

    #endregion

    #region Private Methods

    private void OnScoreChanged(int value)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {value}";
        }
    }

    #endregion
}
