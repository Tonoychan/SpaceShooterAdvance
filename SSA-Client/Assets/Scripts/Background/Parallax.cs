using UnityEngine;

/// <summary>
/// Scrolls a sprite downward for parallax background effect.
/// Loops when sprite has scrolled one full height below start position.
/// </summary>
public class Parallax : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private float parallaxSpeed;

    #endregion

    #region Private Fields

    private float spriteHeight;
    private Vector3 startPos;

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        startPos = transform.position;
        spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);

        // Loop when scrolled one full sprite height
        if (transform.position.y < startPos.y - spriteHeight)
        {
            transform.position = startPos;
        }
    }

    #endregion
}
