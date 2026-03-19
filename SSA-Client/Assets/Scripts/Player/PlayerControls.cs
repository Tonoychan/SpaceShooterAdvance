using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

/// <summary>
/// Handles touch-based player movement. Converts screen touch to world position and clamps
/// movement within play area boundaries. Uses offset for smooth drag-to-move behavior.
/// </summary>
public class PlayerControls : MonoBehaviour
{
    #region Private Fields

    private Camera mainCamera;
    private Vector3 offset;

    // Play area boundaries (viewport-based, set in SetBoundary)
    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBottom;

    #endregion

    #region Unity Lifecycle

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SetBoundary());
    }

    private void Update()
    {
        #region Touch Handling

        if (Touch.activeTouches.Count > 0)
        {
            Touch touch = Touch.activeTouches[0];
            Vector2 screenPos = touch.screenPosition;

            // Guard against invalid touch positions
            if (!float.IsFinite(screenPos.x) || !float.IsFinite(screenPos.y))
                return;

            float camZ = mainCamera.transform.position.z;
            Vector3 touchPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -camZ));

            if (Touch.activeTouches[0].phase == TouchPhase.Began)
            {
                offset = touchPos - transform.position;
            }

            if (Touch.activeTouches[0].phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);
            }

            if (Touch.activeTouches[0].phase == TouchPhase.Stationary)
            {
                transform.position = new Vector3(touchPos.x - offset.x, touchPos.y - offset.y, 0);
            }

            // Clamp position to play area
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, maxLeft, maxRight),
                Mathf.Clamp(transform.position.y, maxBottom, maxTop),
                0f);
        }

        #endregion
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Calculates play area boundaries from camera viewport. Delayed to ensure camera is ready.
    /// </summary>
    private IEnumerator SetBoundary()
    {
        yield return new WaitForSeconds(0.2f);

        maxLeft = mainCamera.ViewportToWorldPoint(new Vector2(0.15f, 0f)).x;
        maxRight = mainCamera.ViewportToWorldPoint(new Vector2(0.85f, 0f)).x;
        maxBottom = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.05f)).y;
        maxTop = mainCamera.ViewportToWorldPoint(new Vector2(0f, 0.9f)).y;
    }

    #endregion
}
