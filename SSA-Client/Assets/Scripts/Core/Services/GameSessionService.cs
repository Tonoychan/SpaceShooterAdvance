using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSessionService : MonoBehaviour,IGameStatusReader,IGameResolver,IScoreWriter
{
    private const string LevelUnlockKey = "LevelUnlock";
    
    public bool IsGameOver { get; private set; }
    public int CurrentScore { get; private set; }
    
    public event Action<int> ScoreChanged;
    public event Action GameWon;
    public event Action GameLost;
    
    [SerializeField] private IScorePersistence scorePersistence;
    private PanelController panelController;
    
    public void SetPanelController(PanelController controller)
    {
        panelController = controller;
    }
    
    public void SetScorePersistence(IScorePersistence persistence)
    {
        scorePersistence = persistence;
    }
    
    private void Awake()
    {
        if (scorePersistence == null)
        {
            scorePersistence = new PlayerPrefsScorePersistence();
        }
    }
    
    public void ResetGameState()
    {
        IsGameOver = false;
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);
    }
    
    public void ResolveWin()
    {
        if (IsGameOver) return;
        SaveAndResetScore();
        panelController?.ActivateWinScreen();
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        int currentUnlock = PlayerPrefs.GetInt(LevelUnlockKey, 0);
        if (nextLevel > currentUnlock)
        {
            PlayerPrefs.SetInt(LevelUnlockKey, nextLevel);
        }
    }

    public void ResolveLose()
    {
        IsGameOver = true;
        SaveAndResetScore();
        panelController?.ActivateLoseScreen();
    }

    public void StartResolveSequence(float delayInSeconds = 2)
    {
        StartCoroutine(ResolveSequenceCoroutine(delayInSeconds));
    }
    
    public void AddScore(int value)
    {
        CurrentScore += value;
        ScoreChanged?.Invoke(CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);
    }
    
    private IEnumerator ResolveSequenceCoroutine(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        ResolveGame();
    }
    public void ResolveGame()
    {
        if (IsGameOver)
        {
            ResolveLose();
        }
        else
        {
            ResolveWin();
        }
    }
    
    public void TriggerLose()
    {
        IsGameOver = true;
        StartResolveSequence(2f);
    }
    
    private void SaveAndResetScore()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt("Score" + sceneName, CurrentScore);
        int highScore = PlayerPrefs.GetInt("HighScore" + sceneName, 0);
        if (CurrentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore" + sceneName, CurrentScore);
        }
        CurrentScore = 0;
        ScoreChanged?.Invoke(0);
    }
}
