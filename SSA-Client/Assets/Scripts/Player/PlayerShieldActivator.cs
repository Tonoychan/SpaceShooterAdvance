using UnityEngine;

/// <summary>
/// Activates or repairs the player's shield when a shield power-up is collected.
/// If shield is inactive, enables it. If already active, repairs to full hits.
/// </summary>
public class PlayerShieldActivator : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Shield shield;

    #endregion

    #region Public Methods

    /// <summary>
    /// Activates the shield or repairs it if already active.
    /// Called by PowerUpShield when player collects the power-up.
    /// </summary>
    public void ActivateShield()
    {
        if (shield.gameObject.activeSelf == false)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.RepairShield();
        }
    }

    #endregion
}
