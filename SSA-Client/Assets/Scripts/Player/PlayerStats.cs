using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages player health, damage handling, and death. Handles health bar UI, damage animation,
/// and explosion VFX. Integrates with game resolution and shield protection.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float maxHealth;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject explosionVFX;
    [SerializeField] private Shield shield;

    #endregion

    #region Private Fields

    private float health;
    private bool canPlayAnim = true;
    private PlayerShooting playerShooting;
    private IGameResolver gameResolver;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
        playerShooting = GetComponent<PlayerShooting>();

        if (EndGameManager.Instance != null)
        {
            gameResolver = EndGameManager.Resolver;
            if (gameResolver is GameSessionService gs)
            {
                gs.ResetGameState();
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Applies damage to the player. Ignores damage when shield protection is active.
    /// Triggers lose condition or resolve sequence when health reaches zero.
    /// </summary>
    /// <param name="damage">Amount of damage to apply.</param>
    public void PlayerTakeDamage(float damage)
    {
        if (shield.protection)
            return;

        health -= damage;
        healthBar.fillAmount = health / maxHealth;

        // Play damage animation with anti-spam cooldown
        if (canPlayAnim)
        {
            playerAnimator.SetTrigger("IsDamage");
            StartCoroutine(AntiSpamAnimation());
        }

        playerShooting.DecreaseUpgradelevel();

        if (health <= 0)
        {
            if (gameResolver is GameSessionService gs)
            {
                gs.TriggerLose();
            }
            else
            {
                gameResolver?.StartResolveSequence(2f);
            }
            Instantiate(explosionVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Restores health up to maxHealth. Clamps excess healing.
    /// </summary>
    /// <param name="healAmount">Amount of health to restore.</param>
    public void PlayerAddHealth(float healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.fillAmount = health / maxHealth;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Prevents damage animation from spamming. Cooldown: 0.15 seconds.
    /// </summary>
    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }

    #endregion
}
