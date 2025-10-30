using UnityEngine;
using UnityEngine.AI;
using AYellowpaper.SerializedCollections;
using System.Collections;

public class Enemy : MonoBehaviour, IHealbarTarget
{
    public NavMeshAgent agent;
    public PlayerBody player;
    public int maxHealth;
    public float atkRange;
    public DamageType weakness;
    public UnitHealth unitHealth;
    public int damage;

    public GameObject stunMarker;

    public GameObject normalSprite;
    public GameObject deadSprite;
    public ParticleSystem exorciseParticle;
    public SerializedDictionary<DamageType, ParticleSystem> damageParticles;

    [System.NonSerialized] public bool canBeExorcised = false;

    private bool isBeingKnocked = false;
    private float originalAcceleration;
    private float originalSpeed;

    void Awake()
    {
        unitHealth = new(maxHealth,maxHealth);
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBody>();
        originalAcceleration = agent.acceleration;
        originalSpeed = agent.speed;
    }

    void Update()
    {
        if (isBeingKnocked)
            return;
        Vector3 playerPosition = player.transform.position;
        agent.SetDestination(playerPosition);

        if(Vector3.Distance(playerPosition,transform.position) <= atkRange)
        {
            player.PlayerTakeDmg(damage);
        }
    }

    public void TakeDamage(DamageType damageType, int damage)
    {
        var _damageToTake = damage;
        if (damageType == weakness)
            _damageToTake = damage * DamageMultiplier.normal;
        else
            _damageToTake = damage * DamageMultiplier.weak;

        unitHealth.Health -= _damageToTake;
        damageParticles[damageType].Stop();
        damageParticles[damageType].Play();

        if (unitHealth.Health <= 0)
        {
            canBeExorcised = true;
        }
    }

    Coroutine knockRoutine;
    public void KnockBack(Vector3 vector, float force)
    {
        if (knockRoutine != null)
            StopCoroutine(knockRoutine);
        knockRoutine = StartCoroutine(KnockbackIE(vector, force));
    }

    IEnumerator KnockbackIE(Vector3 vector, float force)
    {
        isBeingKnocked = true;
        agent.speed = force/2.0f;
        agent.acceleration = force;
        agent.SetDestination(transform.position + (vector * 10));
        stunMarker.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        agent.isStopped = true;
        yield return new WaitForSeconds(0.9f);
        stunMarker.SetActive(false);
        agent.speed = originalSpeed;
        agent.acceleration = originalAcceleration;
        isBeingKnocked = false;
        agent.isStopped = false;
    }

    public void Exorcise()
    {
        StartCoroutine(ExorciseIE());
    }

    private IEnumerator ExorciseIE()
    {
        agent.isStopped = true;
        exorciseParticle.Play();
        deadSprite.SetActive(true);
        normalSprite.SetActive(false);
        yield return new WaitUntil(() => !exorciseParticle.IsAlive(true));
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