using UnityEngine;

/// <summary>
/// Tracks elapsed time and determines win condition. When possibleWinTime is reached:
/// - If no boss: resolves game as win and disables spawners.
/// - If has boss: sets canSpawnBoss so EnemySpawner spawns boss on disable.
/// </summary>
public class WinCondition : MonoBehaviour
{
    #region Serialized Fields
    
    [SerializeField] private LevelData levelData;
    [SerializeField] private GameObject[] spawners;

    #endregion

    #region Public Fields

    public bool canSpawnBoss = false;

    #endregion

    #region Public Methods

    public void SetLevelData(LevelData data)
    {
        levelData = data;
    }

    #endregion

    #region Private Fields

    private float timer;
    private IGameStatusReader gameStatus;
    private IGameResolver gameResolver;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        if (EndGameManager.Instance != null)
        {
            gameStatus = EndGameManager.Status;
            gameResolver = EndGameManager.Resolver;
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (levelData == null)
            return;

        if (gameStatus != null && gameStatus.IsGameOver)
            return;

        timer += Time.deltaTime;
        if (timer >= levelData.possibleWinTime)
        {
            if (levelData?.hasBoss == false)
            {
                gameResolver?.ResolveGame();
            }
            else
            {
                canSpawnBoss = true;
            }

            foreach (var spawner in spawners)
            {
                spawner.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

    #endregion
}
