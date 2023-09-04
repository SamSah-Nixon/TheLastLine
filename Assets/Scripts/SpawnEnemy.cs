using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject enemySpawnPrefab;
    public GameObject enemyPrefab;
    public Transform playerLocation;
    public float spawnRate;
    public float moveSpeed = 1f;
    private float timer;
    private Vector3 spawnPos;

    void Start()
    {
        timer = Time.time + spawnRate;
    }
    void Update()
    {
        if (Time.time >= timer)
        {
            spawnPos = RandomXSpawn(50, transform.position);
            Instantiate(enemySpawnPrefab, spawnPos, Quaternion.identity);
            var enemy = Instantiate(enemyPrefab,spawnPos, Quaternion.identity);
            enemy.GetComponent<EnemyAI>().playerLocation = playerLocation;
            spawnRate /= 1.01f;
            enemy.GetComponent<EnemyAI>().moveSpeed = moveSpeed;
            timer = Time.time + spawnRate;
        }
    }

    Vector3 RandomXSpawn(float spread, Vector3 position)
    {
        return new Vector3(position.x + Random.Range(-spread, spread), position.y, position.z);
    }

}
