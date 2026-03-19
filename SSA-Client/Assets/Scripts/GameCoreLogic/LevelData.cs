using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelId;           // For PlayerPrefs keys: "Score" + levelId
    public int levelIndex;           // For unlock progression (0, 1, 2...)
    public float possibleWinTime;
    public bool hasBoss;
    public float enemySpawnTime;
    public float meteorSpawnTime;
}
