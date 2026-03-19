using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void LoadLevelString(string sceneName)
    {
        FadeCanvas.fadeCanvas.FaderLoadString(sceneName);
    }

    public void LoadLevelInt(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void RestartLevel()
    {
        FadeCanvas.fadeCanvas.FaderLoadString(SceneManager.GetActiveScene().name);
    }
}
