using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endGameManager;
    public bool isGameOver = false;

    PanelController panelController;
    private TextMeshProUGUI scoreText;
    private int score;
    public readonly string lvlUnlock = "LevelUnlock";
    private void Awake()
    {
        if (endGameManager == null)
        {
            endGameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartResolveSequence()
    {
        StopCoroutine(nameof(ResolveSequence));
        StartCoroutine(ResolveSequence());
    }

    IEnumerator ResolveSequence()
    {
        yield return new WaitForSeconds(2f);
        ResolveGame();
    }

    public void ResolveGame()
    {
        if (isGameOver == false)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        ScoreSet();
        panelController.ActivateWinScreen();
        var nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel > PlayerPrefs.GetInt(lvlUnlock, 0))
        {
            PlayerPrefs.SetInt(lvlUnlock, nextLevel);
        }
    }

    public void LoseGame()
    {
        ScoreSet();
        panelController.ActivateLoseScreen();
    }

    public void RegisterPanelController(PanelController _panelController)
    {
        panelController = _panelController;
    }

    public void RegisterScoreText(TextMeshProUGUI _scoreText)
    {
        scoreText = _scoreText;
    }

    public void UpdateScoreText(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = $"Score: "+score;
    }
    
    private void ScoreSet()
    {
        PlayerPrefs.SetInt("Score"+SceneManager.GetActiveScene().name, score);
        int highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name,0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + SceneManager.GetActiveScene().name, score);
        }

        score = 0;
    }
}
