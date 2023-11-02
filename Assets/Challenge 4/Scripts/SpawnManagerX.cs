using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15;
    private float spawnZMax = 25;

    public int enemyCount;
    public int waveCount = 1;


    public GameObject player;
    public WaveManager waveManager; // Assign the WaveManager in the Inspector

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            // Advance to the next wave and get the enemy speed for the current wave
            float currentEnemySpeed = waveManager.GetEnemySpeedForCurrentWave();

            // Call a method to spawn the enemy wave with the updated speed
            SpawnEnemyWave(waveCount, currentEnemySpeed);
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn, float enemySpeed)
    {
        // Spawn enemies with the provided speed
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemyPrefab.GetComponent<EnemyX>().speed = enemySpeed; // Set the enemy speed
        }

        waveCount++;
        ResetPlayerPosition();
        // Move player back to position in front of own goal
        void ResetPlayerPosition()
        {
            player.transform.position = new Vector3(0, 1, -7);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        }
    }
}

