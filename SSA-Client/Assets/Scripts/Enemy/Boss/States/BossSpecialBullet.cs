using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Boss special projectile. Moves downward, rotates, then explodes into mini-bullets
/// after a random delay (2-3 seconds). Damages player on contact.
/// </summary>
public class BossSpecialBullet : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject miniBulletRef;
    [SerializeField] private Transform[] spawnPoints;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        rb.linearVelocity = Vector2.down * speed;
        StartCoroutine(ExplodeBullet());
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
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

    #region Private Methods

    /// <summary>
    /// Waits random time (2-3s), spawns mini-bullets at each spawn point, then destroys self.
    /// </summary>
    private IEnumerator ExplodeBullet()
    {
        var randomExplodeTime = Random.Range(2f, 3f);
        yield return new WaitForSeconds(randomExplodeTime);
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(miniBulletRef, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        Destroy(gameObject);
    }

    #endregion
}
