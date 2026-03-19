using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Spawns random enemies at the top of the screen at configurable intervals.
/// Spawns boss when disabled if WinCondition allows. Boss spawns at top center.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private WinCondition winCondition;

    #endregion

    #region Private Fields

    private float maxLeft;
    private float maxRight;
    private float yPosition;
    private Camera mainCamera;
    private float enemySpawnTimer;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundary());
    }

    private void Update()
    {
        SpawnEnemies();
    }

    #endregion

    #region Unity Messages

    /// <summary>
    /// When spawner is disabled (e.g. level complete), spawn boss if conditions are met.
    /// </summary>
    private void OnDisable()
    {
        if (!winCondition.canSpawnBoss)
            return;

        if (bossPrefab != null)
        {
            Vector2 spawnPos = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Spawns a random enemy from the array at a random X position along the top.
    /// </summary>
    private void SpawnEnemies()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= spawnTime)
        {
            int randomPick = Random.Range(0, enemy.Length);
            Instantiate(enemy[randomPick],
                new Vector3(Random.Range(maxLeft, maxRight), yPosition, 0),
                Quaternion.identity);
            enemySpawnTimer = 0;
        }
    }

    /// <summary>
    /// Calculates spawn boundaries from camera viewport. Delayed to ensure camera is ready.
    /// </summary>
    private IEnumerator SetBoundary()
    {
        yield return new WaitForSeconds(0.5f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0f)).x;
        yPosition = mainCamera.ViewportToWorldPoint(new Vector2(0f, 1.1f)).y;
    }

    #endregion
}
