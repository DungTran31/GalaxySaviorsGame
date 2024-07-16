using System.Collections;
using UnityEngine;
using DungTran31.Utilities;
using DungTran31.Utilities.Extensions;
using DungTran31.UI;
using DungTran31.GamePlay.Player;

namespace DungTran31.GamePlay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float initialSpawnRate = 10f;
        // Add a factor to control how much the spawn rate decreases
        [SerializeField] private float spawnRateDecreaseFactor = 0.9f;
        // Minimum spawn rate to ensure it doesn't get too fast
        [SerializeField] private float minSpawnRate = 1f;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private bool canSpawn = true;
        [SerializeField] private int maxEnemies = 15;
        private int currentEnemyCount = 0;

        private void Start()
        {
            StartCoroutine(Spawner());
        }


        private IEnumerator Spawner()
        {
            float currentSpawnRate = initialSpawnRate;

            while (canSpawn && currentEnemyCount <= maxEnemies)
            {
                WaitForSeconds wait = new(currentSpawnRate);
                yield return wait;

                int index = Random.Range(0, _enemyPrefabs.Length);
                string enemyTag = "";
                switch (index)
                {
                    case 0: enemyTag = "enemy"; break;
                    case 1: enemyTag = "enemy2"; break;
                    case 2: enemyTag = "rangedEnemy"; break;
                }

                float randomX = Random.Range(-10f, 10f);
                float randomY = Random.Range(-10f, 10f);
                ObjectPooler.Instance.SpawnFromPool(enemyTag, transform.position.Add(randomX, randomY, 0), Quaternion.identity);
                currentEnemyCount++;

                // Decrease the spawn rate for the next spawn, but ensure it doesn't go below minSpawnRate
                currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate * spawnRateDecreaseFactor);
            }
        }
    }
}
