using UnityEngine;

public class BossDeath : BossBaseState
{
    public override void RunState()
    {
        EndGameManager.Resolver.StartResolveSequence();
        gameObject.SetActive(false);
    }
}
