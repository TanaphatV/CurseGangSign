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
            StartCoroutine(InvulnIE());
        }
    }
    public void PlayerHeal(int healing)
    {
        unitHealth.HealUnit(healing);
    }

    public UnitHealth GetUnitHealth()
    {
        Debug.Log("Returnn unity");
        return unitHealth;
    }

    IEnumerator InvulnIE()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invulnWindow);
        canBeDamaged = true;
    }
}
