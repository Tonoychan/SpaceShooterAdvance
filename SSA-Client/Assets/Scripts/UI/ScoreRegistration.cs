using UnityEngine;
using TMPro;

public class ScoreRegistration : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        EndGameManager.endGameManager.RegisterScoreText(textMeshProUGUI);
        textMeshProUGUI.text = "Score: 0";
    }

}
