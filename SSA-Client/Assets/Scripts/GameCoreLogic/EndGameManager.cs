using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager endGameManager;
    public static IGameResolver Resolver { get; private set; }
    public static IGameStatusReader Status { get; private set; }
    public static IScoreWriter Score { get; private set; }
    
    [SerializeField] private GameSessionService gameSessionService;
    
    private void Awake()
    {
        if (endGameManager == null)
        {
            endGameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        if (gameSessionService != null)
        {
            Resolver = gameSessionService;
            Status = gameSessionService;
            Score = gameSessionService;
        }
        else
        {
            Debug.LogError("EndGameManager: Assign GameSessionService in Inspector.");
        }
    }
}
