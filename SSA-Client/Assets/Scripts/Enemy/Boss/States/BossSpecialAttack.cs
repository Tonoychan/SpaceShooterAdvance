using System.Collections;
using UnityEngine;

/// <summary>
/// Boss Special Attack state. Moves to top center, fires special bullet, waits, then returns to Attack.
/// </summary>
public class BossSpecialAttack : BossBaseState
{
    #region Serialized Fields

    [SerializeField] private float bossSpecialAttackSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject bossSpecialBulletPrefab;
    [SerializeField] private Transform bossSpecialShootingPoint;

    #endregion

    #region Private Fields

    private Vector2 targetPoint;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        targetPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.9f));
    }

    #endregion

    #region Public Methods

    public override void RunState()
    {
        StartCoroutine(RunSpecialAttackState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    #endregion

    #region Private Methods

    private IEnumerator RunSpecialAttackState()
    {
        // Move to top center
        while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint,
                bossSpecialAttackSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        if (BulletPoolManager.Instance != null)
            BulletPoolManager.Instance.BossSpecialBulletPool.Get(bossSpecialShootingPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        bossController.ChangeState(BossState.Attack);
    }

    #endregion
}
