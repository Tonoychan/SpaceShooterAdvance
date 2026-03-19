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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.down * speed;
    }

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    #endregion
}
