using System;
using UnityEngine;

public class PurpleEnemy : Enemy
{
    [SerializeField]
    protected float moveSpeed;

    private float shootTimer=0f;
    [SerializeField]
    private float shootInterval;
    
    [SerializeField] private Transform LeftFirePoint;
    [SerializeField] private Transform RightFirePoint;
    
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        rigidbody2D.linearVelocity = Vector2.down * moveSpeed;
    }

    void Update()
    {
        shootTimer+= Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Instantiate(bulletPrefab, LeftFirePoint.position, Quaternion.identity);
            Instantiate(bulletPrefab, RightFirePoint.position, Quaternion.identity);
            shootTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public override void HurtSequence()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Dmg"))
            return;
        animator.SetTrigger("IsDamage");
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
