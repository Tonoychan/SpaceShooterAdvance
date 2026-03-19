using UnityEngine;

/// <summary>
/// Handles player weapon firing. Supports 5 upgrade levels (0-4) with different shot patterns.
/// Uses interval-based shooting with configurable fire rate.
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform shootingPoint;

    /*
     * Shooting points for upgrade levels 1-4:
     * Level 1: left/right pair 1
     * Level 2: center + pair 1
     * Level 3: center + pair 1 + angled pair 2
     * Level 4: center + pairs 1, 2, and 3
     */
    [SerializeField] private Transform leftShootingPoint1;
    [SerializeField] private Transform rightShootingPoint1;
    [SerializeField] private Transform leftShootingPoint2;
    [SerializeField] private Transform rightShootingPoint2;
    [SerializeField] private Transform leftShootingPoint3;
    [SerializeField] private Transform rightShootingPoint3;

    [SerializeField] private float shootInterval;
    [SerializeField] private AudioSource audioSource;

    #endregion

    #region Private Fields

    private int shootUpgradeLevel;
    private float resetInterval;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        resetInterval = shootInterval;
    }

    private void Update()
    {
        shootInterval -= Time.deltaTime;
        if (shootInterval <= 0)
        {
            Shoot();
            shootInterval = resetInterval;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Increases the shoot upgrade level. Max level is 4.
    /// </summary>
    /// <param name="value">Amount to add to upgrade level.</param>
    public void IncreaseUpgradelevel(int value)
    {
        shootUpgradeLevel += value;
        if (shootUpgradeLevel > 4)
        {
            shootUpgradeLevel = 4;
        }
    }

    /// <summary>
    /// Decreases the shoot upgrade level by 1. Min level is 0.
    /// Called when player takes damage.
    /// </summary>
    public void DecreaseUpgradelevel()
    {
        shootUpgradeLevel -= 1;
        if (shootUpgradeLevel < 0)
        {
            shootUpgradeLevel = 0;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Spawns laser(s) based on current upgrade level. Plays shoot sound.
    /// </summary>
    private void Shoot()
    {
        if (BulletPoolManager.Instance == null) return;
        audioSource.Play();
        var pool = BulletPoolManager.Instance.LaserPool;
        switch (shootUpgradeLevel)
        {
            case 0:
                pool.Get(shootingPoint.position, Quaternion.identity);
                break;
            case 1:
                pool.Get(leftShootingPoint1.position, Quaternion.identity);
                pool.Get(rightShootingPoint1.position, Quaternion.identity);
                break;
            case 2:
                pool.Get(shootingPoint.position, Quaternion.identity);
                pool.Get(leftShootingPoint1.position, Quaternion.identity);
                pool.Get(rightShootingPoint1.position, Quaternion.identity);
                break;
            case 3:
                pool.Get(shootingPoint.position, Quaternion.identity);
                pool.Get(leftShootingPoint1.position, Quaternion.identity);
                pool.Get(rightShootingPoint1.position, Quaternion.identity);
                pool.Get(leftShootingPoint2.position, leftShootingPoint2.rotation);
                pool.Get(rightShootingPoint2.position, rightShootingPoint2.rotation);
                break;
            case 4:
                pool.Get(shootingPoint.position, Quaternion.identity);
                pool.Get(leftShootingPoint1.position, Quaternion.identity);
                pool.Get(rightShootingPoint1.position, Quaternion.identity);
                pool.Get(leftShootingPoint2.position, leftShootingPoint2.rotation);
                pool.Get(rightShootingPoint2.position, rightShootingPoint2.rotation);
                pool.Get(leftShootingPoint3.position, leftShootingPoint3.rotation);
                pool.Get(rightShootingPoint3.position, rightShootingPoint3.rotation);
                break;
        }
    }

    #endregion
}
