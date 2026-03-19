using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Central game session service. Implements IGameStatusReader, IGameResolver, and IScoreWriter.
/// Manages score, win/lose resolution, level unlock, and panel transitions.
/// </summary>
public class GameSessionService : MonoBehaviour, IGameStatusReader, IGameResolver, IScoreWriter
{
    #region Constants

    private const string LevelUnlockKey = "LevelUnlock";

    #endregion

    #region Public Properties & Events

    public bool IsGameOver { get; private set; }
    public int CurrentScore { get; private set; }

    public event Action<int> ScoreChanged;
    public event Action GameWon;
    public event Action GameLost;

    #endregion

    #region Serialized Fields

    [SerializeField] private IScorePersistence scorePersistence;

    #endregion

    #region Private Fields

    private PanelController panelController;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (scorePersistence == null)
        {
            scorePersistence = new PlayerPrefsScorePersistence();
        }
    }

    #endregion

    #region Public Methods - Setup

    /// <summary>
    /// Registers the panel controller for win/lose screen activation.
    /// </summary>
    public void SetPanelController(PanelController controller)
    {
        panelController = controller;
    }

    /// <summary>
    /// Sets the score persistence implementation. Defaults to PlayerPrefs if null.
    /// </summary>
    public void SetScorePersistence(IScorePersistence persistence)
    {
        scorePersistence = persistence;
    }

    #endregion

    #region Public Methods - IGameStatusReader / Game State

    /// <summary>
    /// Resets game state for a new run. Clears game over flag and score.
    /// </summary>
    public void ResetGameState()
    {
        IsGameOver = false;
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);
    }

    #endregion

    #region Public Methods - IGameResolver

    /// <inheritdoc />
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

    /// <inheritdoc />
    public void ResolveLose()
    {
        IsGameOver = true;
        SaveAndResetScore();
        panelController?.ActivateLoseScreen();
    }

    /// <inheritdoc />
    public void StartResolveSequence(float delayInSeconds = 2)
    {
        StartCoroutine(ResolveSequenceCoroutine(delayInSeconds));
    }

    /// <inheritdoc />
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

    /// <summary>
    /// Forces lose state and starts resolve sequence. Used when player dies.
    /// </summary>
    public void TriggerLose()
    {
        IsGameOver = true;
        StartResolveSequence(2f);
    }

    #endregion

    #region Public Methods - IScoreWriter

    /// <inheritdoc />
    public void AddScore(int value)
    {
        CurrentScore += value;
        ScoreChanged?.Invoke(CurrentScore);
    }

    /// <inheritdoc />
    public void ResetScore()
    {
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Waits for delay, then resolves game (win or lose based on IsGameOver).
    /// </summary>
    private IEnumerator ResolveSequenceCoroutine(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        ResolveGame();
    }

    /// <summary>
    /// Saves current score to PlayerPrefs, updates high score if applicable, then resets.
    /// </summary>
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

    #endregion
}
