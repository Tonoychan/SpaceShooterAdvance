using UnityEngine;

/// <summary>
/// Heal power-up collectible. Restores player health on contact.
/// Plays sound and destroys self. Also destroys when leaving screen.
/// </summary>
public class PowerUpHeal : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private int healAmount;
    [SerializeField] private AudioClip audioClip;

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            playerStats.PlayerAddHealth(healAmount);
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
