using UnityEngine;

/// <summary>
/// Boss health and damage handling. Extends Enemy base. On death, triggers BossDeath state.
/// </summary>
public class BossStats : Enemy
{
    #region Serialized Fields

    [SerializeField] private BossController bossController;

    #endregion

    #region Unity Messages

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
        }
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
    /// Adds score, triggers death state, spawns explosion, then boss is deactivated by BossDeath.
    /// </summary>
    public override void DeathSequence()
    {
        base.DeathSequence();
        bossController.ChangeState(BossState.Death);
        Instantiate(explosionVFX, transform.position, transform.rotation);
    }

    #endregion
}
