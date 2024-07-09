using UnityEngine;

namespace DungTran31.GamePlay.Collectible
{
    public class CollectibleSpawner : MonoBehaviour
    {
        public GameObject[] collectiblePrefabs; // Now an array of GameObjects
        public float spawnRate = 5f; // Time in seconds between each spawn
        public Vector2 spawnAreaMin; // Minimum spawn coordinates
        public Vector2 spawnAreaMax; // Maximum spawn coordinates

        private float nextSpawnTime;

        void Start()
        {
            nextSpawnTime = Time.time + spawnRate;
        }

        void Update()
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnCollectible();
                nextSpawnTime = Time.time + spawnRate;
            }
        }

        void SpawnCollectible()
        {
            // Generate a random position within the defined boundaries
            float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            // Choose a random collectible prefab from the array
            if (collectiblePrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, collectiblePrefabs.Length);
                GameObject collectiblePrefab = collectiblePrefabs[randomIndex];

                // Instantiate the chosen collectible at the random position
                Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
