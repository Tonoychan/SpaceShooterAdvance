using UnityEngine;

/// <summary>
/// Player shield with 3 hit points. Blocks damage when protection is true.
/// On contact: damages enemies, destroys projectiles, or takes hit from boss (instant break).
/// shieldBase[0-2] represent visual tiers (3, 2, 1 hits remaining).
/// </summary>
public class Shield : MonoBehaviour
{
    #region Public Fields

    public bool protection = false;

    #endregion

    #region Serialized Fields

    /*
     * Visual stages for shield durability:
     * [0] = left/outer, [1] = middle, [2] = right/inner
     * All active = 3 hits, two = 2 hits, one = 1 hit, none = broken
     */
    [SerializeField] private GameObject[] shieldBase;

    #endregion

    #region Private Fields

    private int hitsToDestroy;

    #endregion

    #region Unity Messages

    private void OnEnable()
    {
        hitsToDestroy = 3;
        for (var i = 0; i < shieldBase.Length; i++)
        {
            shieldBase[i].SetActive(true);
        }
        protection = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (other.CompareTag("Boss"))
            {
                // Boss contact breaks shield instantly
                hitsToDestroy = 0;
                DamageShield();
                return;
            }

            enemy.TakeDamage(10);
            DamageShield();
        }
        else
        {
            Destroy(other.gameObject);
            DamageShield();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Restores shield to full (3 hits) and updates visual.
    /// </summary>
    public void RepairShield()
    {
        hitsToDestroy = 3;
        UpdateUI();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates which shield tier sprites are visible based on hits remaining.
    /// </summary>
    private void UpdateUI()
    {
        switch (hitsToDestroy)
        {
            case 0:
                for (var i = 0; i < shieldBase.Length; i++)
                {
                    shieldBase[i].SetActive(false);
                }
                break;
            case 1:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(false);
                shieldBase[2].SetActive(false);
                break;
            case 2:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(true);
                shieldBase[2].SetActive(false);
                break;
            case 3:
                shieldBase[0].SetActive(true);
                shieldBase[1].SetActive(true);
                shieldBase[2].SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Decrements hits. Disables shield and sets protection false when depleted.
    /// </summary>
    private void DamageShield()
    {
        hitsToDestroy--;
        if (hitsToDestroy <= 0)
        {
            protection = false;
            gameObject.SetActive(false);
        }

        UpdateUI();
    }

    #endregion
}
