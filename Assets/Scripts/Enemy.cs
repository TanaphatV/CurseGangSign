using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public int maxHealth;
    public DamageType weakness;
    public UnitHealth unitHealth;

    [System.NonSerialized] public bool canBeExorcised = false;

    void Start()
    {
        unitHealth = new(maxHealth,maxHealth);
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 playerPosition = player.position;

        agent.SetDestination(playerPosition);
    }

    public void TakeDamage(DamageType damageType, int damage)
    {
        Debug.Log("Take damage");
        var _damageToTake = damage;
        if (damageType == weakness)
            _damageToTake = damage * DamageMultiplier.normal;
        else
            _damageToTake = damage * DamageMultiplier.weak;

        unitHealth.Health -= _damageToTake;
        if (unitHealth.Health <= 0)
        {
            canBeExorcised = true;
        }
    }

    private void Die()
    {

    }
}

public class DamageMultiplier
{
    public const int normal = 2;
    public const int weak = 1;
}

public enum DamageType
{
    fire,
    water,
    lightning
}