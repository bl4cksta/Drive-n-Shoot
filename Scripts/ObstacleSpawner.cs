using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject roadblockPrefab;
    [SerializeField] GameObject[] carPrefabs;
    bool isGameActive;
    int maxObstacle = 10; // максимальное количество рандомных префабов
    float minimumSpawnTime = 10f; // минимальное время для спавна объектов, +5 = максимальное
    List<GameObject> spawnedObstacles = new List<GameObject>();
    void Start()
    {
        isGameActive = true;
        GlobalEventManager.OnGameOver.AddListener(GameOver);
        FirstSpawn();
        StartCoroutine(Spawning());
    }

    void FirstSpawn()
    {
        for (int i = 0; i < maxObstacle; i++)
        {
            var obs = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], transform.position, Quaternion.identity, transform);
            spawnedObstacles.Add(obs);
            obs.SetActive(false);
        }
    }

    IEnumerator Spawning()
    {
        while (isGameActive)
        {
            Vector3 spawnPos;
            Quaternion rotPos;
            if (Random.Range(0, 2) == 0)
            {
                spawnPos = new Vector3(2.5f, -0.45f, 90);
                rotPos = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                spawnPos = new Vector3(-3, -0.45f, 90);
                rotPos = Quaternion.Euler(0, 90, 0);

            }
            foreach (var obstacle in spawnedObstacles)
            {
                if (!obstacle.activeInHierarchy)
                {
                    obstacle.transform.position = spawnPos;
                    obstacle.transform.rotation = rotPos;
                    obstacle.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(Random.Range(minimumSpawnTime, minimumSpawnTime + 5));
        }
    }
    void GameOver()
    {
        isGameActive = false;
    }
}
