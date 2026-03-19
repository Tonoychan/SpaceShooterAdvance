using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Updates level select buttons with locked/unlocked state and icons.
/// Uses PlayerPrefs "LevelUnlock" to determine which levels are playable.
/// </summary>
public class ButtonIcons : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite unlockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private LevelData[] levelDataArray;
    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (levelButtons == null || levelDataArray == null || levelDataArray.Length == 0)
            return;

        var unlockedLevel = PlayerPrefs.GetInt("LevelUnlock", 0);
        var count = Mathf.Min(levelButtons.Length, levelDataArray.Length);

        for (var i = 0; i < count; i++)
        {
            if (levelDataArray[i].levelIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.sprite = unlockedIcon;
                var textButton = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i + 1).ToString();
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.sprite = lockedIcon;
                levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }

    #endregion
}
