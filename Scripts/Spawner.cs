using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab, bossPrefab;
    private float spawnRate;
    private bool isGameActive, bossPhase;
    private List<GameObject> enemies = new List<GameObject>();
    private int maxEnemyCount = 20;
    int remainCount;
    void Start()
    {
        remainCount = Main.enemiesToKill;
        FirstSpawn(); // кэшируем сразу на старте всех врагов и отключаем 
        spawnRate = Main.spawnRate;
        isGameActive = true;
        GlobalEventManager.OnGameOver.AddListener(GameOver);
        GlobalEventManager.OnEnemyKilled.AddListener(EnemyKilled);
        //GlobalEventManager.OnSpawnBoss.AddListener(SpawnBoss);
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        while(isGameActive && !bossPhase)
        {
            //if (spawnRate >= 1.8f)
            //    spawnRate -= 0.005f;
            foreach (var enemy in enemies)
            {
                if(!enemy.activeInHierarchy)
                {
                    enemy.transform.position = transform.position;
                    enemy.transform.rotation = enemyPrefab.transform.rotation;

                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().ForceStart();
                    
                    break;
                }
            }
            yield return new WaitForSeconds(spawnRate);
            //if (maxEnemyCount > 0)
            //{
            //    maxEnemyCount--;
            //    var newEnemy = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation, transform);
            //    enemies.Add(newEnemy);
            //}

        }
    }
    void FirstSpawn()
    {
        var boss = Instantiate(bossPrefab, transform.position, bossPrefab.transform.rotation, transform);
        //boss.SetActive(false);
        for (int i = 0; i < maxEnemyCount; i++)
        {
            var newEnemy = Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation, transform);
            enemies.Add(newEnemy);
            //newEnemy.SetActive(false);
        }
    }
    //void SpawnBoss()
    //{
    //    if (!bossPhase)
    //    {
    //        bossPhase = true;
    //        Instantiate(bossPrefab, transform.position, bossPrefab.transform.rotation, transform);
    //    }
    //}
    void GameOver()
    {
        isGameActive = false;
    }
    void EnemyKilled()
    {
        remainCount--;
        if (remainCount <= 0)
        {
            foreach (var enemy in enemies)
                enemy.SetActive(false);
            bossPhase = true;
            GlobalEventManager.SendSpawnBoss();
        }
    }
}
