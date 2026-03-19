using System;
using UnityEngine;

public class PowerUpAttack : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerShooting = other.GetComponent<PlayerShooting>();
            playerShooting.IncreaseUpgradelevel(1);
            AudioSource.PlayClipAtPoint(audioClip, transform.position,1f);
            Destroy(gameObject);
        }
    }
    
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
