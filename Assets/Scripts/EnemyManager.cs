using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance => _instance;
    private static EnemyManager _instance;

    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private List<Enemy> enemyPrefab;

    public List<Enemy> activeEnemies;

    public int enemiesKilled;

    private int targetEnemyCount = 1;

    private int step;
    private int killStepTrack;

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
        step = 0;
        targetEnemyCount = 1;
        killStepTrack = 0;
        activeEnemies = new();
        SpawnEnemy();
    }
    //1 2 4 7 11 16

    private void Update()
    {
        targetEnemyCount = Mathf.FloorToInt(0.5f * (Mathf.Pow((float)step, 2) - (float)step + 2.0f));
        if (activeEnemies.Count < targetEnemyCount)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        var enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], null);
        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
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
            enemiesKilled++;
            activeEnemies[i].Exorcise();
            killStepTrack++;
            activeEnemies.RemoveAt(i);
        }

        if (killStepTrack >= targetEnemyCount)
        {
            step++;
            killStepTrack = 0;
        }
    }

}
