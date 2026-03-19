using UnityEngine;

/// <summary>
/// ScriptableObject defining power-up spawn configuration.
/// spawnThreshold: 0-99. Random 0-99 must be >= threshold to spawn (higher = rarer).
/// Used by Meteor.DeathSequence to optionally spawn a random power-up.
/// </summary>
[CreateAssetMenu(fileName = "PowerUps", menuName = "Scriptable Objects/PowerUps")]
public class PowerUps : ScriptableObject
{
    #region Public Fields

    public GameObject[] powerups;
    public int spawnThreshold;

    #endregion

    #region Public Methods

    /// <summary>
    /// Attempts to spawn a random power-up at the given position.
    /// Spawn occurs only if Random(0,100) >= spawnThreshold.
    /// </summary>
    /// <param name="spawnPosition">World position to spawn at.</param>
    public void SpawnPowerUp(Vector3 spawnPosition)
    {
        var randomChance = Random.Range(0, 100);
        if (randomChance >= spawnThreshold)
        {
            var randomIndex = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

    #endregion
}
