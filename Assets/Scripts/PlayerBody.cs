using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public UnitHealth unitHealth;

    public void PlayerTakeDmg(int dmg)
    {
        GameManager.Instance._playerHealth.DmgUnit(dmg);
    }
    public void PlayerHeal(int healing)
    {
        GameManager.Instance._playerHealth.HealUnit(healing);
    }
}
