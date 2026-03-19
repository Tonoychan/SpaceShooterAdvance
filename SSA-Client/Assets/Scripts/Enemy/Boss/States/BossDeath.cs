using UnityEngine;

public class BossDeath : BossBaseState
{
    public override void RunState()
    {
        EndGameManager.endGameManager.StartResolveSequence();
        gameObject.SetActive(false);
    }
}
