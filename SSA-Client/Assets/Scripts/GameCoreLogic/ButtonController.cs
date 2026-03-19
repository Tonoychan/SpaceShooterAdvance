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
    /// Loads a level by LevelData with fade transition.
    /// </summary>
    public void LoadLevel(LevelData levelData)
    {
        FadeCanvas.fadeCanvas.FaderLoadLevel(levelData);
    }

    /// <summary>
    /// Restarts the current level with fade transition.
    /// </summary>
    public void RestartLevel()
    {
        // CHANGE: FaderLoadString(SceneManager.GetActiveScene().name)
        // TO: FaderLoadLevel(FadeCanvas.fadeCanvas.CurrentLevelData)
        FadeCanvas.fadeCanvas.FaderLoadLevel(FadeCanvas.CurrentLevelData);
    }

    #endregion
}
