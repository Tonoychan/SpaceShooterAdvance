using UnityEngine;

/// <summary>
/// Controls win/lose screen visibility. Registers with GameSessionService on Start.
/// Activates appropriate screen when game resolves.
/// </summary>
public class PanelController : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameSessionService gameSessionService;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        var service = ResolveGameSessionService();
        if (service != null)
        {
            service.SetPanelController(this);
        }
    }

    /// <summary>
    /// Resolves the active GameSessionService. Prefers EndGameManager's persisted instance
    /// (used when loading from level selection) over the serialized reference.
    /// </summary>
    private GameSessionService ResolveGameSessionService()
    {
        if (EndGameManager.Instance != null && EndGameManager.Resolver is GameSessionService gs)
            return gs;
        return gameSessionService;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the win screen and makes canvas fully visible.
    /// </summary>
    public void ActivateWinScreen()
    {
        canvasGroup.alpha = 1;
        winScreen.SetActive(true);
    }

    /// <summary>
    /// Shows the lose screen and makes canvas fully visible.
    /// </summary>
    public void ActivateLoseScreen()
    {
        canvasGroup.alpha = 1;
        loseScreen.SetActive(true);
    }

    #endregion
}
