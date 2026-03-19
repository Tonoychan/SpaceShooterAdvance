using System;
using UnityEngine;

public class BossBaseState : MonoBehaviour
{
    
    protected Camera mainCamera;

    protected float maxLeft;
    protected float maxRight;
    protected float maxTop;
    protected float maxBottom;
    
    protected BossController bossController;

    private void Awake()
    {
        mainCamera =  Camera.main;
        bossController = GetComponent<BossController>();
    }

    protected virtual void Start()
    {
        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.3f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.7f, 0f)).x;

        maxBottom = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.5f)).y;
        maxTop = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.9f)).y;
    }

    public virtual void RunState()
    {
        
    }

    public virtual void StopState()
    {
        StopAllCoroutines();
    }


}
