using System;
using UnityEngine;

public class BossStats : Enemy
{
    [SerializeField] private BossController bossController;

    public override void HurtSequence()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
            return;
        animator.SetTrigger("IsDamage");
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        bossController.ChangeState(BossState.Death);
        Instantiate(explosionVFX, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
        }
    }
}
