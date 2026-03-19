using UnityEngine;

/// <summary>
/// Tracks elapsed time and determines win condition. When possibleWinTime is reached:
/// - If no boss: resolves game as win and disables spawners.
/// - If has boss: sets canSpawnBoss so EnemySpawner spawns boss on disable.
/// </summary>
public class WinCondition : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float possibleWinTime;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private bool hasBoss = false;

    #endregion

    #region Public Fields

    public bool canSpawnBoss = false;

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
        if (gameStatus != null && gameStatus.IsGameOver)
            return;

        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            if (hasBoss == false)
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
