using UnityEngine;
using System.Collections;

public class PlayerBody : MonoBehaviour, IHealbarTarget
{
    public UnitHealth unitHealth;
    public float invulnWindow;

    private bool canBeDamaged = true;

    void Awake()
    {
        unitHealth = new(100,100);
    }

    public void PlayerTakeDmg(int dmg)
    {
        if(canBeDamaged)
        {
            unitHealth.DmgUnit(dmg);
            if (unitHealth.Health <= 0)
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
            StartCoroutine(InvulnIE());
        }
    }
    public void PlayerHeal(int healing)
    {
        unitHealth.HealUnit(healing);
    }

    public UnitHealth GetUnitHealth()
    {
        return unitHealth;
    }

    IEnumerator InvulnIE()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invulnWindow);
        canBeDamaged = true;
    }
}
