using UnityEngine;

public class PlayerShieldActivator : MonoBehaviour
{
    [SerializeField] private Sheild sheild;

    public void ActivateShield()
    {
        if(sheild.gameObject.activeSelf==false)
        {
            sheild.gameObject.SetActive(true);
        }
        else
        {
            sheild.RepairShield();
        }
    }
}
