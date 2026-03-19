using System;
using UnityEngine;

/// <summary>
/// Boss state machine. Manages transitions between Enter, Attack, SpecialAttack, and Death states.
/// Each state is handled by a separate BossBaseState component.
/// </summary>
public enum BossState
{
    Enter,
    Attack,
    SpecialAttack,
    Death
}

public class BossController : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private BossEnter bossEnter;
    [SerializeField] private BossShooting bossShooting;
    [SerializeField] private BossSpecialAttack bossSpecialAttack;
    [SerializeField] private BossDeath bossDeath;

    [SerializeField] private bool debugSkipToState;
    [SerializeField] private BossState state;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        ChangeState(BossState.Enter);

        if (debugSkipToState)
        {
            ChangeState(state);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Transitions to the specified boss state. Stops all active states before Death.
    /// </summary>
    /// <param name="state">Target state to transition to.</param>
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

    #endregion
}
