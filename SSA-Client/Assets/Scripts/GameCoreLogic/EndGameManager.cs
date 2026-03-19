using UnityEngine;

/// <summary>
/// Singleton service locator for game resolution, status, and score.
/// Assigns GameSessionService to static Resolver, Status, and Score properties.
/// Persists across scenes via DontDestroyOnLoad.
/// </summary>
public class EndGameManager : MonoBehaviour
{
    #region Static Properties

    public static EndGameManager Instance;
    public static IGameResolver Resolver { get; private set; }
    public static IGameStatusReader Status { get; private set; }
    public static IScoreWriter Score { get; private set; }

    #endregion

    #region Serialized Fields

    [SerializeField] private GameSessionService gameSessionService;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (gameSessionService != null)
        {
            Resolver = gameSessionService;
            Status = gameSessionService;
            Score = gameSessionService;
        }
        else
        {
            Debug.LogError("EndGameManager: Assign GameSessionService in Inspector.");
        }
    }

    #endregion
}
