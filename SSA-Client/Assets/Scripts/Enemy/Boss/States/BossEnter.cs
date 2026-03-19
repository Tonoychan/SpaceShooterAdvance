using System.Collections;
using UnityEngine;

/// <summary>
/// Boss Enter state. Moves boss from spawn point to center-top of screen.
/// Transitions to Attack state when arrival threshold is reached.
/// </summary>
public class BossEnter : BossBaseState
{
    #region Serialized Fields

    [SerializeField] private float speed;

    #endregion

    #region Private Fields

    private Vector2 enterPoint;

    #endregion

    #region Unity Lifecycle

    protected override void Start()
    {
        base.Start();
        enterPoint = mainCamera.ViewportToWorldPoint(new Vector2(0.5f, 0.7f));
    }

    #endregion

    #region Public Methods

    public override void RunState()
    {
        StartCoroutine(RunEnterState());
    }

    public override void StopState()
    {
        base.StopState();
    }

    #endregion

    #region Private Methods

    private IEnumerator RunEnterState()
    {
        while (Vector2.Distance(transform.position, enterPoint) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, enterPoint, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        bossController.ChangeState(BossState.Attack);
    }

    #endregion
}
