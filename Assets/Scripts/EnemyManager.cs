using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance => _instance;
    private static EnemyManager _instance;

    [SerializeField] private List<Enemy> enemyPrefab;

    public List<Enemy> activeEnemies;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        activeEnemies = new();
        SpawnEnemy();
    }

    private void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], null);
        enemy.transform.position = Vector3.zero;
        activeEnemies.Add(enemy);
    }

    public void TryExorciseAll()
    {
        List<int> toDelete = new();
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(activeEnemies[i].canBeExorcised)
                toDelete.Add(i);
        }
        toDelete.Sort();
        for (int i = toDelete.Count - 1; i >= 0; i--)
        {
            activeEnemies[i].Exorcise();
            activeEnemies.RemoveAt(i);
        }
    }

}
