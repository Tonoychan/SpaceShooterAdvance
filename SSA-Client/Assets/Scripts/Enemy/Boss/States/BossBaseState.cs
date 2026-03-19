using UnityEngine;

/// <summary>
/// Base class for boss state behaviors. Provides camera reference and play area boundaries.
/// Subclasses implement RunState and optionally override StopState.
/// </summary>
public class BossBaseState : MonoBehaviour
{
    #region Protected Fields

    protected Camera mainCamera;

    /*
     * Boss movement boundaries (viewport-based):
     * maxLeft/maxRight: 0.3 to 0.7 of viewport width
     * maxBottom/maxTop: 0.5 to 0.9 of viewport height
     */
    protected float maxLeft;
    protected float maxRight;
    protected float maxTop;
    protected float maxBottom;

    protected BossController bossController;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        mainCamera = Camera.main;
        bossController = GetComponent<BossController>();
    }

    protected virtual void Start()
    {
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.3f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.7f, 0f)).x;
        maxBottom = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.5f)).y;
        maxTop = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.9f)).y;
    }

    #endregion

    #region Virtual Methods

    /// <summary>
    /// Called when this state becomes active. Override to start state behavior.
    /// </summary>
    public virtual void RunState()
    {
    }

    /// <summary>
    /// Called when leaving this state. Stops all coroutines by default.
    /// </summary>
    public virtual void StopState()
    {
        StopAllCoroutines();
    }

    #endregion
}
