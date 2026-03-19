using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonIcons : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Sprite unlockedIcon;
    [SerializeField] private Sprite lockedIcon;
    [SerializeField] private int firstLevelBuildIndex;

    private void Awake()
    {
        var unlockedLevel = PlayerPrefs.GetInt
            (EndGameManager.endGameManager.lvlUnlock, firstLevelBuildIndex);
        for (var i = 0; i < levelButtons.Length; i++)
        {
            if (i + firstLevelBuildIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].image.sprite = unlockedIcon;
                var textButton = 
                    levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textButton.text = (i+1).ToString();
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].image.sprite = lockedIcon;
                levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().enabled = false;
            }
        }
    }
}
