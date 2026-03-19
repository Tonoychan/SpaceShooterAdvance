using UnityEngine;

/// <summary>
/// Mini-bullet spawned by BossSpecialBullet. Moves in transform.up direction.
/// Damages player on contact. Destroys when leaving screen.
/// </summary>
public class BossMiniBullet : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rb;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        rb.linearVelocity = transform.up * speed;
    }

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            ReleaseToPool();
        }
    }

    private void OnBecameInvisible()
    {
        ReleaseToPool();
    }

    #endregion
    
    #region Private Methods
    
    private void ReleaseToPool()
    {
        if (BulletPoolManager.Instance != null)
            BulletPoolManager.Instance.BossMiniBulletPool.Release(this);
        else
            Destroy(gameObject);
    }
    
    #endregion
}
