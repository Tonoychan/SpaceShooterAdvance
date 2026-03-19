using UnityEngine;

/// <summary>
/// Central manager for all bullet pools. Place on a persistent GameObject (e.g. GameManager).
/// </summary>
public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance { get; private set; }

    [Header("Prefabs")]
    [SerializeField] private LaserBullet laserBulletPrefab;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    [SerializeField] private BossSpecialBullet bossSpecialBulletPrefab;
    [SerializeField] private BossMiniBullet bossMiniBulletPrefab;

    [Header("Pool Sizes")]
    [SerializeField] private int laserPoolSize = 50;
    [SerializeField] private int enemyBulletPoolSize = 30;
    [SerializeField] private int bossSpecialBulletPoolSize = 5;
    [SerializeField] private int bossMiniBulletPoolSize = 20;

    public ObjectPool<LaserBullet> LaserPool { get; private set; }
    public ObjectPool<EnemyBullet> EnemyBulletPool { get; private set; }
    public ObjectPool<BossSpecialBullet> BossSpecialBulletPool { get; private set; }
    public ObjectPool<BossMiniBullet> BossMiniBulletPool { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        var poolParent = new GameObject("BulletPools").transform;
        poolParent.SetParent(transform);

        LaserPool = new ObjectPool<LaserBullet>(laserBulletPrefab, laserPoolSize, poolParent);
        EnemyBulletPool = new ObjectPool<EnemyBullet>(enemyBulletPrefab, enemyBulletPoolSize, poolParent);
        BossSpecialBulletPool = new ObjectPool<BossSpecialBullet>(bossSpecialBulletPrefab, bossSpecialBulletPoolSize, poolParent);
        BossMiniBulletPool = new ObjectPool<BossMiniBullet>(bossMiniBulletPrefab, bossMiniBulletPoolSize, poolParent);
    }
}