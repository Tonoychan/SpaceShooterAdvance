using UnityEngine;

/// <summary>
/// Attack upgrade power-up collectible. Increases player shoot upgrade level by 1 on contact.
/// Plays sound and destroys self. Also destroys when leaving screen.
/// </summary>
public class PowerUpAttack : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private AudioClip audioClip;

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerShooting = other.GetComponent<PlayerShooting>();
            playerShooting.IncreaseUpgradelevel(1);
            AudioSource.PlayClipAtPoint(audioClip, transform.position, 1f);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    #endregion
}
