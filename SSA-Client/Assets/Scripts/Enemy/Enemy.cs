using UnityEngine;

/// <summary>
/// Base class for all enemies. Handles health, damage, death, and score registration.
/// Subclasses override HurtSequence and DeathSequence for custom behavior.
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] protected float health;
    [SerializeField] protected Rigidbody2D rigidbody2D;
    [SerializeField] protected float damage;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject explosionVFX;
    [SerializeField] public int scoreValue;

    #endregion

    #region Protected Fields

    protected IScoreWriter scoreWriter;

    #endregion

    #region Unity Lifecycle

    protected virtual void Awake()
    {
        if (EndGameManager.Instance != null)
        {
            scoreWriter = EndGameManager.Score;
        }
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Applies damage to the enemy. Calls HurtSequence, then DeathSequence if health reaches zero.
    /// </summary>
    /// <param name="damage">Amount of damage to apply.</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();
        if (health <= 0)
        {
            DeathSequence();
        }
    }

    #endregion

    #region Virtual Methods

    /// <summary>
    /// Called when enemy takes damage. Override for custom hurt behavior (e.g. animation).
    /// </summary>
    public virtual void HurtSequence()
    {
    }

    /// <summary>
    /// Called when enemy health reaches zero. Adds score and can be overridden for VFX/cleanup.
    /// </summary>
    public virtual void DeathSequence()
    {
        scoreWriter?.AddScore(scoreValue);
    }

    #endregion
}
