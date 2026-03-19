using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Boss Attack state. Moves randomly within bounds and fires from multiple points.
/// Transitions to SpecialAttack after a random duration (5-10 seconds).
/// </summary>
public class BossShooting : BossBaseState
{
    #region Serialized Fields

    [SerializeField] private float bossSpeed;
    [SerializeField] private float bossShootRate;
    [SerializeField] private GameObject bossBulletPrefab;
    [SerializeField] private Transform[] boosShootingPoints;

    #endregion

    #region Public Methods

    public override void RunState()
    {
        StartCoroutine(RunShootingState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    #endregion

    #region Private Methods

    private IEnumerator RunShootingState()
    {
        var shootTimer = 0f;
        var fireStateTimer = 0f;
        var fireStateExitTime = Random.Range(5f, 10f);
        var nextTargetPosition = new Vector2(Random.Range(maxLeft, maxRight),
            Random.Range(maxBottom, maxTop));

        while (fireStateTimer <= fireStateExitTime)
        {
            // Move toward target, pick new target when reached
            if (Vector2.Distance(transform.position, nextTargetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    nextTargetPosition, bossSpeed * Time.deltaTime);
            }
            else
            {
                nextTargetPosition = new Vector2(Random.Range(maxLeft, maxRight),
                    Random.Range(maxBottom, maxTop));
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= bossShootRate)
            {
                for (int i = 0; i < boosShootingPoints.Length; i++)
                {
                    BulletPoolManager.Instance.EnemyBulletPool.Get(boosShootingPoints[i].position,
                        Quaternion.identity);
                }
                shootTimer = 0;
            }

            yield return new WaitForEndOfFrame();
            fireStateTimer += Time.deltaTime;
        }

        bossController.ChangeState(BossState.SpecialAttack);
    }

    #endregion
}
