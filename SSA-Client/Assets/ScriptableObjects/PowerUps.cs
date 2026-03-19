using UnityEngine;

[CreateAssetMenu(fileName = "PowerUps", menuName = "Scriptable Objects/PowerUps")]
public class PowerUps : ScriptableObject
{
    public GameObject[] powerups;
    public int spawnThreshold;

    public void SpawnPowerUp(Vector3 spawnPosition)
    {
        var randomChance = Random.Range(0, 100);
        if (randomChance >= spawnThreshold)
        {
            var randomIndex = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
