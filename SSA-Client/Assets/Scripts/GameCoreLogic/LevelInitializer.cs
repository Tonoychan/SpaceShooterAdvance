using UnityEngine;

/// <summary>
/// Applies LevelData to scene objects when GameplayScene loads.
/// Runs after FadeCanvas loads the scene; components read from LevelData instead of serialized fields.
/// </summary>
public class LevelInitializer : MonoBehaviour
{
    private void Awake()
    {
        LevelData levelData = FadeCanvas.CurrentLevelData;
        if (levelData == null)
        {
            Debug.LogWarning("LevelInitializer: No CurrentLevelData set. Level-specific config will not be applied.");
            return;
        }

        // Inject LevelData into WinCondition
        var winCondition = FindObjectOfType<WinCondition>();
        if (winCondition != null)
        {
            winCondition.SetLevelData(levelData);
        }

        // Inject spawn times into spawners
        var enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.SetSpawnTime(levelData.enemySpawnTime);
        }

        var meteorSpawner = FindObjectOfType<MeteorSpawner>();
        if (meteorSpawner != null)
        {
            meteorSpawner.SetSpawnTime(levelData.meteorSpawnTime);
        }

        // Reset game state for new run
        if (EndGameManager.Resolver is GameSessionService gameSession)
        {
            gameSession.ResetGameState();
        }

        // Optional: clear after setup so it's not held indefinitely
        // FadeCanvas.CurrentLevelData = null;
    }
}