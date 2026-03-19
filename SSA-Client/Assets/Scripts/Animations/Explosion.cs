using UnityEngine;

/// <summary>
/// Simple explosion VFX. Destroys itself after 1 second.
/// Attach to explosion prefab used by player and enemies.
/// </summary>
public class Explosion : MonoBehaviour
{
    #region Unity Lifecycle

    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    #endregion
}
