using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        var score = PlayerPrefs.GetInt("Score"+SceneManager.GetActiveScene().name,0);
        scoreText.text =$"Score: {score}";
        
        var highScore = PlayerPrefs.GetInt("HighScore" + SceneManager.GetActiveScene().name,0);
        highScoreText.text =$"High Score: {highScore}";
    }
}
