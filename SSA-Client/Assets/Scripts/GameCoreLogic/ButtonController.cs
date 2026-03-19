using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles UI button actions for loading levels and restarting.
/// Uses FadeCanvas for fade transitions when loading by name.
/// </summary>
public class ButtonController : MonoBehaviour
{
    #region Public Methods

    /// <summary>
    /// Loads a level by scene name with fade transition.
    /// </summary>
    /// <param name="sceneName">Name of the scene to load.</param>
    public void LoadLevelString(string sceneName)
    {
        FadeCanvas.fadeCanvas.FaderLoadString(sceneName);
    }

    /// <summary>
    /// Loads a level by build index. No fade transition.
    /// </summary>
    /// <param name="sceneIndex">Build index of the scene.</param>
    public void LoadLevelInt(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Restarts the current level with fade transition.
    /// </summary>
    public void RestartLevel()
    {
        FadeCanvas.fadeCanvas.FaderLoadString(SceneManager.GetActiveScene().name);
    }

    #endregion
}
