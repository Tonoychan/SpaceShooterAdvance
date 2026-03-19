using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float health;
    
    [SerializeField]
    private Animator playerAnimator;
    [SerializeField] 
    private Image healthBar;
    [SerializeField] 
    private GameObject explosionVFX;
    [SerializeField]
    private Sheild shield;
    
    private bool canPlayAnim = true;
    private PlayerShooting _playerShooting;
    
    private IGameResolver gameResolver;
    
     void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = health / maxHealth;
        _playerShooting = GetComponent<PlayerShooting>();
        
        if (EndGameManager.endGameManager != null)
        {
            gameResolver = EndGameManager.Resolver;
            if (gameResolver is GameSessionService gs)
            {
                gs.ResetGameState();
            }
        }
    }
     
    public void PlayerTakeDamage(float damage)
    {
        if(shield.protection)
            return;
        
        health -= damage;
        healthBar.fillAmount = health / maxHealth;
        if (canPlayAnim)
        {
            playerAnimator.SetTrigger("IsDamage");
            StartCoroutine(AntiSpamAnimation());
        }
        _playerShooting.DecreaseUpgradelevel();
        if (health <= 0)
        {
            if (gameResolver is GameSessionService gs)
            {
                gs.TriggerLose();
            }
            else
            {
                gameResolver?.StartResolveSequence(2f);
            }
            Instantiate(explosionVFX,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void PlayerAddHealth(float healAmount)
    {
        health += healAmount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.fillAmount = health / maxHealth;
    }

    private IEnumerator AntiSpamAnimation()
    {
        canPlayAnim = false;
        yield return new WaitForSeconds(0.15f);
        canPlayAnim = true;
    }
}
