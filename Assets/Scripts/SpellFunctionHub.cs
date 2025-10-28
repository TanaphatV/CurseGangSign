using UnityEngine;

public class SpellFunctionHub : MonoBehaviour
{
    public int damagePerShot;
    public void Fire()
    {
        Fire(DamageType.fire); 
    }

    public void Water()
    {
        Fire(DamageType.water);
    }

    public void Electric()
    {
        Fire(DamageType.lightning);
    }

    private void Fire(DamageType damageType)
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width/2.0f,Screen.height/2.0f)),out RaycastHit hitInfo))
        {
            if(hitInfo.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damageType,damagePerShot);
            }
        }
    }

    public void Exorcise()
    {

    }

    public void Fart()
    {

    }
}
