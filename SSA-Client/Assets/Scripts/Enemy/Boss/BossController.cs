using System;
using UnityEngine;

public enum BossState
{
    Enter,
    Attack,
    SpecialAttack,
    Death
}

public class BossController : MonoBehaviour
{
    [SerializeField] private BossEnter bossEnter;
    [SerializeField] private BossShooting bossShooting;
    [SerializeField] private BossSpecialAttack bossSpecialAttack;
    [SerializeField] private BossDeath bossDeath;
    
    [SerializeField] private bool test;
    [SerializeField] private BossState state;

    private void Start()
    {
        ChangeState(BossState.Enter);
        
        if (test)
        {
            ChangeState(state);
        }
    }


    public void ChangeState(BossState state)
    {
        switch (state)
        {
            case BossState.Enter:
                bossEnter.RunState();
                break;
            case BossState.Attack:
                bossShooting.RunState();
                break;
            case BossState.SpecialAttack:
                bossSpecialAttack.RunState();
                break;
            case BossState.Death:
                bossEnter.StopState();
                bossShooting.StopState();
                bossSpecialAttack.StopState();
                bossDeath.RunState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

}
