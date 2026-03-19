using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private float maxLeft;
    private float maxRight;

    private float yPosition;
    private Camera mainCamera;

    [SerializeField] private GameObject[] enemy;
    private float enemySpawmTimer;
    [SerializeField] private float spawnTime;

    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private WinCondition winCondition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundary());
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        enemySpawmTimer+= Time.deltaTime;
        if (enemySpawmTimer >= spawnTime)
        {
            int randomPick = Random.Range(0,enemy.Length);
            Instantiate(enemy[randomPick], 
                new Vector3(Random.Range(maxLeft,maxRight),yPosition,0), 
                Quaternion.identity);
            enemySpawmTimer = 0;
        }
    }

    private IEnumerator SetBoundary()
    {
        yield return new WaitForSeconds(0.5f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0f)).x;
        yPosition = mainCamera.ViewportToWorldPoint(new Vector2(0f,1.1f)).y;
    }

    private void OnDisable()
    {
        if(!winCondition.canSpawnBoss)
            return;
        
        if (bossPrefab != null)
        {
            Vector2 spawnPos = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 1.2f));
            Instantiate(bossPrefab,spawnPos,Quaternion.identity);
        }
    }
}
