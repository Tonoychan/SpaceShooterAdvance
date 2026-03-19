using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    
    [SerializeField] private GameSessionService gameSessionService;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameSessionService != null)
        {
            gameSessionService.SetPanelController(this);
        }
    }

    public void ActivateWinScreen()
    {
        canvasGroup.alpha = 1;
        winScreen.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        canvasGroup.alpha = 1;
        loseScreen.SetActive(true);
    }

}
