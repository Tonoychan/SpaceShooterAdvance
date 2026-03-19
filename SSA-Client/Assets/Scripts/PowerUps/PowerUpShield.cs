using UnityEngine;

/// <summary>
/// Shield power-up collectible. Activates or repairs player shield on contact.
/// Plays sound and destroys self. Also destroys when leaving screen.
/// </summary>
public class PowerUpShield : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private AudioClip audioClip;

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var shieldActivator = other.GetComponent<PlayerShieldActivator>();
            shieldActivator.ActivateShield();
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
