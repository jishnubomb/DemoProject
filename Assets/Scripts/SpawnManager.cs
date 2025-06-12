using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;
    private float lastSpawnPlatformLength = 0f;
    private float lastSpawnPlatformX = -5f;
    private float lastSpawnPlatformMinY = -1.75f;
    private float lastSpawnPlatformMaxY = -1.75f;
    private float lastSpawnX = -5f;

    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    public float maxYChange = 3f;
    public float minYChange = -5f;
    public float maxYRange = 10f;
    public float minLength = 5f;
    public float maxLength = 10f;
    public float xRange = 6f;

    void Update()
    {
        // Check if player moved far enough to spawn new platform
        if (player.position.x + 5 > lastSpawnX + lastSpawnPlatformLength)
        {
            SpawnPlatform();
        }
    }

    private void OnEnable()
    {
        lastSpawnPlatformLength = 0f;
        lastSpawnPlatformX = -5f;
        lastSpawnPlatformMinY = -1.75f;
        lastSpawnPlatformMaxY = -1.75f;
        lastSpawnX = -5f;
    }
    void SpawnPlatform()
    {
        // Spawn ahead of player
        float spawnX = lastSpawnPlatformX + lastSpawnPlatformLength + Random.Range(3f, xRange);
        float minY = Random.Range(lastSpawnPlatformMinY + minYChange, lastSpawnPlatformMaxY + maxYChange);
        float maxY = minY + Random.Range(3f, maxYRange);
        float spawnY = Random.Range(minY, maxY);
        float length = Random.Range(minLength, maxLength);
        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);

        // Create platform
        Platform platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity).GetComponent<Platform>();
        platform.startY = minY;
        platform.endY = maxY;
        platform.transform.localScale = new Vector3(length, 1, 1);
        platform.speed = Random.Range(minSpeed, maxSpeed);

    // Update last spawn position
    lastSpawnPlatformX = spawnX;
        lastSpawnPlatformMinY = minY;
        lastSpawnPlatformMaxY = maxY;
        lastSpawnPlatformLength = length;
        lastSpawnX = player.position.x;
    }
}