using UnityEngine;

/// <summary>
/// Boss Death state. Triggers game win resolution and deactivates the boss.
/// </summary>
public class BossDeath : BossBaseState
{
    #region Public Methods

    public override void RunState()
    {
        EndGameManager.Resolver.StartResolveSequence();
        gameObject.SetActive(false);
    }

    #endregion
}
