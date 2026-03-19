using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] MeteorPrefab;
    [SerializeField] 
    private float spawnTime;
    private float spawnTimer=0f;
    private int index;
    private float maxLeft;
    private float maxRight;

    private float yPosition;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundary());
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            index = Random.Range(0, MeteorPrefab.Length);
            var go= Instantiate(MeteorPrefab[index], 
                new Vector3(Random.Range(maxLeft,maxRight),yPosition,-5),
                Quaternion.Euler(0,0,Random.Range(0f,360f)));
            var randomSize = Random.Range(0.9f,1.2f);
            go.transform.localScale = new Vector3(randomSize, randomSize, 0f);
            spawnTimer = 0f;
        }
    }
    
    private IEnumerator SetBoundary()
    {
        yield return new WaitForSeconds(0.5f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0f)).x;
        yPosition = mainCamera.ViewportToWorldPoint(new Vector2(0f,1.1f)).y;

    }
}
