using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Meteor enemy that falls with random speed and rotation. Can spawn power-ups on death.
/// Damages player on contact.
/// </summary>
public class Meteor : Enemy
{
    #region Serialized Fields

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PowerUps powerUpSpawner;

    #endregion

    #region Private Fields

    private float speed;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rigidbody2D.linearVelocity = Vector2.down * speed;
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            playerStats.PlayerTakeDamage(damage);
            Instantiate(explosionVFX, transform.position, transform.rotation);
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
    /// Adds score, spawns explosion, optionally spawns power-up, then destroys.
    /// </summary>
    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionVFX, transform.position, transform.rotation);
        if (powerUpSpawner != null)
        {
            powerUpSpawner.SpawnPowerUp(transform.position);
        }
        Destroy(gameObject);
    }

    public override void HurtSequence()
    {
        base.HurtSequence();
    }

    #endregion
}
