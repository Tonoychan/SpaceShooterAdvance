using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossSpecialBullet : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float speed;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject miniBulletRef;
    [SerializeField] private Transform[] spawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = Vector2.down * speed;
        StartCoroutine(ExplodeBullet());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    IEnumerator ExplodeBullet()
    {
        var randomExplodeTime = Random.Range(2f, 3f);
        yield return new WaitForSeconds(randomExplodeTime);
        for (var i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(miniBulletRef, spawnPoints[i].position, spawnPoints[i].rotation);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
