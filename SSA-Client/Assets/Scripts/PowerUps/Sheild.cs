using System;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    private int hitsToDestroy;
    public bool protection = false;
    
    [SerializeField] private GameObject[] shieldBase;

    private void OnEnable()
    {
        hitsToDestroy = 3;
        for(var i=0; i<shieldBase.Length; i++)
        {
            shieldBase[i].SetActive(true);
        }
        protection = true;
    }

    private void UpdateUI()
    {
        switch (hitsToDestroy)
        {
            case 0:
                for(var i=0; i<shieldBase.Length; i++)
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

    public void RepairShield()
    {
        hitsToDestroy = 3;
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (other.CompareTag("Boss"))
            {
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
}
