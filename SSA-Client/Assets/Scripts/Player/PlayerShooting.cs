using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private Transform shootingPoint;
    
    [SerializeField]
    private Transform leftShootingPoint1;
    [SerializeField]
    private Transform rightShootingPoint1;
    [SerializeField]
    private Transform leftShootingPoint2;
    [SerializeField]
    private Transform rightShootingPoint2;
    [SerializeField]
    private Transform leftShootingPoint3;
    [SerializeField]
    private Transform rightShootingPoint3;

    private int shootUpgradelevel;
    
    [SerializeField]
    private float shootInterval;

    private float resetInterval;
    
    [SerializeField] private AudioSource audioSource;
    
    void Start()
    {
        resetInterval = shootInterval;
    }

    
    void Update()
    {
        shootInterval-= Time.deltaTime;
        if (shootInterval <=0)
        {
            Shoot();
            shootInterval = resetInterval;
        }
    }

    public void IncreaseUpgradelevel(int value)
    {
        shootUpgradelevel += value;
        if (shootUpgradelevel > 4)
        {
            shootUpgradelevel = 4;
        }
    }

    public void DecreaseUpgradelevel()
    {
        shootUpgradelevel -= 1;
        if (shootUpgradelevel < 0)
        {
            shootUpgradelevel = 0;
        }
    }

    void Shoot()
    {
        audioSource.Play();
        switch (shootUpgradelevel)
        {
            case 0:
                Instantiate(laserPrefab, shootingPoint.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(laserPrefab, leftShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, rightShootingPoint1.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(laserPrefab, shootingPoint.position, Quaternion.identity);
                Instantiate(laserPrefab, leftShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, rightShootingPoint1.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(laserPrefab, shootingPoint.position, Quaternion.identity);
                Instantiate(laserPrefab, leftShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, rightShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, leftShootingPoint2.position, leftShootingPoint2.rotation);
                Instantiate(laserPrefab, rightShootingPoint2.position, rightShootingPoint2.rotation);
                break;
            case 4:
                Instantiate(laserPrefab, shootingPoint.position, Quaternion.identity);
                Instantiate(laserPrefab, leftShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, rightShootingPoint1.position, Quaternion.identity);
                Instantiate(laserPrefab, leftShootingPoint2.position, leftShootingPoint2.rotation);
                Instantiate(laserPrefab, rightShootingPoint2.position, rightShootingPoint2.rotation);
                Instantiate(laserPrefab, leftShootingPoint3.position, leftShootingPoint3.rotation);
                Instantiate(laserPrefab, rightShootingPoint3.position, rightShootingPoint3.rotation);
                break;
        }
    }
}
