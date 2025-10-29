using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHealbarTarget
{
    public NavMeshAgent agent;
    public Transform player;
    public int maxHealth;
    public DamageType weakness;
    public UnitHealth unitHealth;

    [System.NonSerialized] public bool canBeExorcised = false;

    void Awake()
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

    public void Exorcise()
    {
        Destroy(gameObject);
    }

    public UnitHealth GetUnitHealth()
    {
        return unitHealth;
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