using UnityEngine;

/// <summary>
/// Enemy variant that moves downward and shoots from two fire points.
/// Damages player on contact and plays hurt/death animations.
/// </summary>
public class PurpleEnemy : Enemy
{
    #region Serialized Fields

    [SerializeField] protected float moveSpeed;
    [SerializeField] private float shootInterval;
    [SerializeField] private Transform LeftFirePoint;
    [SerializeField] private Transform RightFirePoint;
    [SerializeField] private GameObject bulletPrefab;

    #endregion

    #region Private Fields

    private float shootTimer = 0f;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        rigidbody2D.linearVelocity = Vector2.down * moveSpeed;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Instantiate(bulletPrefab, LeftFirePoint.position, Quaternion.identity);
            Instantiate(bulletPrefab, RightFirePoint.position, Quaternion.identity);
            shootTimer = 0f;
        }
    }

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Plays damage animation. Prevents spam if already in damage state.
    /// </summary>
    public override void HurtSequence()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
            return;
        animator.SetTrigger("IsDamage");
    }

    /// <summary>
    /// Adds score, spawns explosion VFX, and destroys the enemy.
    /// </summary>
    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    #endregion
}
