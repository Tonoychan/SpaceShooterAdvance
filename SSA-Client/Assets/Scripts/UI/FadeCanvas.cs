using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton canvas that handles fade-in/fade-out transitions when loading scenes.
/// FadeIn runs on Start; FaderLoadString triggers FadeOut then loads scene.
/// </summary>
public class FadeCanvas : MonoBehaviour
{
    #region Static Fields

    public static FadeCanvas fadeCanvas;

    #endregion

    #region Serialized Fields

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float changeValue;
    [SerializeField] private float waitTime;
    [SerializeField] private bool fadeStarted;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (fadeCanvas == null)
        {
            fadeCanvas = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Starts fade-out, loads the level by name, then fades in.
    /// </summary>
    /// <param name="levelName">Scene name to load.</param>
    public void FaderLoadString(string levelName)
    {
        StartCoroutine(FadeOut(levelName));
    }

    #endregion

    #region Private Methods

    private IEnumerator FadeIn()
    {
        fadeStarted = false;
        while (canvasGroup.alpha > 0)
        {
            if (fadeStarted)
                yield break;

            canvasGroup.alpha -= changeValue;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator FadeOut(string levelName)
    {
        if (fadeStarted)
            yield break;

        fadeStarted = true;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += changeValue;
            yield return new WaitForSeconds(waitTime);
        }
        SceneManager.LoadScene(levelName);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn());
    }

    #endregion
}
