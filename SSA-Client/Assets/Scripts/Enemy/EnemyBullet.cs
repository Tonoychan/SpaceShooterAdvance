using System;
using UnityEngine;

/// <summary>
/// Enemy projectile. Moves downward and damages the player on contact.
/// Destroys itself when hitting the player or leaving the screen.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float speed;
    [SerializeField] private float damage;

    #endregion

    #region Private Fields

    private Rigidbody2D rb;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rb.linearVelocity = Vector2.down * speed;
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
            BulletPoolManager.Instance.EnemyBulletPool.Release(this);
        else
            Destroy(gameObject);
    }
    
    #endregion
}
