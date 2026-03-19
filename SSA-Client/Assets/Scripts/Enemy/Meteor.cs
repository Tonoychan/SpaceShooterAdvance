using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Meteor : Enemy
{
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField] 
    private PowerUps powerUpSpawner;

    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rigidbody2D.linearVelocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    public override void DeathSequence()
    {
        base.DeathSequence();
        Instantiate(explosionVFX,transform.position, transform.rotation);
        if (powerUpSpawner != null)
        {
            powerUpSpawner.SpawnPowerUp(transform.position);
        }
        Destroy(gameObject);
    }

    public override void HurtSequence()
    {
        base.HurtSequence();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerStats playerStats = collider.GetComponent<PlayerStats>();
            playerStats.PlayerTakeDamage(damage);
            Instantiate(explosionVFX,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
