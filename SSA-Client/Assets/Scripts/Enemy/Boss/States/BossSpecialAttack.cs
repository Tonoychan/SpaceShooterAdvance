using System.Collections;
using UnityEngine;

public class BossSpecialAttack : BossBaseState
{
    [SerializeField] private float bossSpecialAttackSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject bossSpecialBulletPrefab;
    [SerializeField] private Transform bossSpecialShootingPoint;

    private Vector2 targetPoint;

    public override void RunState()
    {
        StartCoroutine(RunSpecialAttackState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.9f));
    }

    IEnumerator RunSpecialAttackState()
    {
        while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position,targetPoint,
                bossSpecialAttackSpeed  * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Instantiate(bossSpecialBulletPrefab,bossSpecialShootingPoint.position,Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        bossController.ChangeState(BossState.Attack);
    }
}
