using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] meleeSpawnPoints;
    [SerializeField] private Transform[] rangedSpawnPoints;

    [SerializeField] private GameObject meleeEnemyPrefab;
    [SerializeField] private GameObject rangedEnemyPrefab;

    [SerializeField] private float wavePauseTime = 5f;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        CheckVictory();
    }

    private void UpdateActiveEnemiesList(GameObject[] allEnemies)
    {
        activeEnemies.Clear();

        foreach (GameObject enemy in allEnemies)
        {
            if (enemy.activeSelf)
            {
                activeEnemies.Add(enemy);
            }
        }
    }

    private void CheckVictory()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        UpdateActiveEnemiesList(allEnemies);

        if (activeEnemies.Count == 0 && currentWave >= 5)
        {
            Debug.Log("Victory!");
        }
    }

    private IEnumerator SpawnWaves()
    {
        for (int wave = 1; wave <= 4; wave++)
        {
            Debug.Log($"Wave {wave} started!");
            currentWave = wave;

            SpawnMeleeEnemies(meleeSpawnPoints);

            yield return new WaitForSeconds(wavePauseTime);

            if (wave == 2)
            {
                SpawnRangedEnemies(rangedSpawnPoints);
            }

            yield return new WaitForSeconds(wavePauseTime);

            if (wave == 3)
            {
                SpawnRangedEnemies(rangedSpawnPoints);
                SpawnMeleeEnemies(meleeSpawnPoints);
            }

            yield return new WaitForSeconds(wavePauseTime);

            if (wave == 4)
            {
                SpawnMeleeEnemies(meleeSpawnPoints);
                SpawnRangedEnemies(rangedSpawnPoints);
            }
        }

        Debug.Log("All waves completed!");
    }

    private void SpawnMeleeEnemies(Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Debug.Log("Spawned melee enemy");
            Instantiate(meleeEnemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void SpawnRangedEnemies(Transform[] spawnPoints)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            Debug.Log("Spawned Range enemy");
            Instantiate(rangedEnemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
