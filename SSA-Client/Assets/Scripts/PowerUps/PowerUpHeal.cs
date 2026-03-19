using System;
using UnityEngine;

public class PowerUpHeal : MonoBehaviour
{
    [SerializeField]
    private int healAmount;
    [SerializeField] private AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            playerStats.PlayerAddHealth(healAmount);
            AudioSource.PlayClipAtPoint(audioClip, transform.position,1f);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
