using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public UnitHealth unitHealth;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerTakeDmg(10);
            Debug.Log(GameManager.Instance._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerHeal(10);
            Debug.Log(GameManager.Instance._playerHealth.Health);
        }        
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.Instance._playerHealth.DmgUnit(dmg);
    }
        private void PlayerHeal(int healing)
    {
        GameManager.Instance._playerHealth.HealUnit(healing);
    }
}
