using UnityEngine;

public class SpellFunctionHub : MonoBehaviour
{
    public int damagePerShot;
    public void Fire()
    {
        Fire(DamageType.fire);
        AudioManager.Instance.PlayOneShot("skill_infernobeam",0.6f);
    }

    public void Water()
    {
        Fire(DamageType.water);
        AudioManager.Instance.PlayOneShot("skill_jetstream", 0.6f);
    }

    public void Electric()
    {
        Fire(DamageType.lightning);
        AudioManager.Instance.PlayOneShot("skill_thunderbolt", 0.6f);
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
        AudioManager.Instance.PlayOneShot("skill_exorcise", 0.3f);
        AudioManager.Instance.PlayOneShot("bass_drop", 0.8f);
        EnemyManager.Instance.TryExorciseAll();
    }

    public void Fart()
    {

    }
}
