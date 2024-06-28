using System.Collections;
using UnityEngine;
using DungTran31.Utilities;
namespace DungTran31.GamePlay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnRate = 2f;
        [SerializeField] private GameObject[] _enemyPrefabs;
        [SerializeField] private bool canSpawn = true;
        [SerializeField] private int maxEnemies = 10;
        private int currentEnemyCount = 0;

        private void Start()
        {
            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            WaitForSeconds wait = new WaitForSeconds(spawnRate);

            while (canSpawn && currentEnemyCount <= maxEnemies)
            {
                yield return wait;
                int index = Random.Range(0, _enemyPrefabs.Length);
                GameObject enemyToSpawn = _enemyPrefabs[index];
                string enemyTag = "";
                if (index == 0)
                {
                    enemyTag = "enemy";
                } 
                else if (index == 1)
                {
                    enemyTag = "enemy2";
                } 
                else if (index == 2)
                {
                    enemyTag = "rangedEnemy";
                }
                float randomX = Random.Range(-10f, 10f);
                float randomY = Random.Range(-10f, 10f);
                ObjectPooler.Instance.SpawnFromPool(enemyTag, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);
                currentEnemyCount++;
            }
        }
    }
}
