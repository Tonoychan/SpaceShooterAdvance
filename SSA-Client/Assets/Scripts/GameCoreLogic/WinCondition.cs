using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private float timer;

    [SerializeField] private float possibleWinTime;

    [SerializeField] private GameObject[] spawners;
    [SerializeField] private bool hasBoss = false;
    public bool canSpawnBoss = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EndGameManager.endGameManager.isGameOver)
            return;
        
        timer += Time.deltaTime;
        if (timer >= possibleWinTime)
        {
            if (hasBoss == false)
            {
                EndGameManager.endGameManager.StartResolveSequence();
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
}
