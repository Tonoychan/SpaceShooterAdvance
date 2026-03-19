using UnityEngine;
using TMPro;

public class ScoreRegistration : MonoBehaviour
{
    [SerializeField] private GameSessionService gameSessionService;
    private TextMeshProUGUI scoreText;
    private bool subscribed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null) return;
        if (gameSessionService != null)
        {
            gameSessionService.ScoreChanged += OnScoreChanged;
            subscribed = true;
            scoreText.text = $"Score: {gameSessionService.CurrentScore}";
        }
        else
        {
            scoreText.text = "Score: 0";
        }
    }
    
    private void OnScoreChanged(int value)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {value}";
        }
    }
    private void OnDestroy()
    {
        if (subscribed && gameSessionService != null)
        {
            gameSessionService.ScoreChanged -= OnScoreChanged;
        }
    }
}
