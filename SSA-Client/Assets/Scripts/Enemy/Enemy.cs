using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    protected float health;
    [SerializeField] 
    protected Rigidbody2D rigidbody2D;
    [SerializeField] 
    protected float damage;
    [SerializeField]
    protected Animator animator;

    [SerializeField] 
    protected GameObject explosionVFX;
    
    [SerializeField]
    public int scoreValue;
    
    protected IScoreWriter scoreWriter;
        
    protected virtual void Awake()
    {
        if (EndGameManager.endGameManager != null)
        {
            scoreWriter = EndGameManager.Score;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();
        if (health <= 0)
        {
            DeathSequence();
        }
    }

    public virtual void HurtSequence()
    {
        
    }

    public virtual void DeathSequence()
    {
        scoreWriter?.AddScore(scoreValue);
    }
}
