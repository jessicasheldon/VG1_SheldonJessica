using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;

        // Outlets
        public Transform[] spawnPoints;
        public GameObject[] asteroidPrefabs;
        public GameObject explosionPrefab;

        // Configuration
        public float maxAsteroidDelay = 2f;
        public float minAsteroidDelay = 0.2f;

        // State tracking
        public float timeElapsed;
        public float asteroidDelay;

        void Start()
        {
            StartCoroutine("AsteroidSpawnTimer");
        }

        IEnumerator AsteroidSpawnTimer()
        {
            // Wait
            yield return new WaitForSeconds(asteroidDelay);

            // Spawn
            SpawnAsteroid();

            // Repeat
            StartCoroutine("AsteroidSpawnTimer");
        }

        // Methods
        void Awake()
        {
            instance = this;
        }

        void Update()
        {
            // Increment passage of time for each frame of the game
            timeElapsed += Time.deltaTime;

            // Computer asteroid delay
            float decreaseDelayOverTime = maxAsteroidDelay - ((maxAsteroidDelay - minAsteroidDelay) / 30f * timeElapsed);
            asteroidDelay = Mathf.Clamp(decreaseDelayOverTime, minAsteroidDelay, maxAsteroidDelay);
        }

        void SpawnAsteroid()
        {
            // Pick random spawn point and random asteroid prefabs
            int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
            Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
            int randomAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
            GameObject randomAsteroidPrefab = asteroidPrefabs[randomAsteroidIndex];

            // Spawn 
            Instantiate(randomAsteroidPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }
}

