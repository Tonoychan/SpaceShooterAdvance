using System;
using UnityEngine;

/// <summary>
/// Player laser projectile. Moves in transform.up direction and damages enemies on contact.
/// Destroys itself when hitting an enemy or leaving the screen.
/// </summary>
public class LaserBullet : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private Rigidbody2D rigidBody;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        rigidBody.linearVelocity = transform.up * speed;
    }

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);
        ReleaseToPool();
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
            BulletPoolManager.Instance.LaserPool.Release(this);
        else
            Destroy(gameObject);
    }
    #endregion
}
