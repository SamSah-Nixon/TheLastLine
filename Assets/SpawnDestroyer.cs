using UnityEngine;

public class SpawnDestroyer : MonoBehaviour
{

    public GameObject destroyerSpawnPrefab;
    public GameObject destroyerPrefab;
    public Transform baseLocation;
    public float spawnRate;
    public float moveSpeed = 1f;
    private float timer;
    private Vector3 spawnPos;

    void Update()
    {
        if (Time.time >= timer)
        {
            spawnPos = RandomXSpawn(50, transform.position);
            Instantiate(destroyerSpawnPrefab, spawnPos, Quaternion.identity);
            var enemy = Instantiate(destroyerPrefab, spawnPos, Quaternion.identity);
            enemy.GetComponent<DestroyerAI>().baseLocation = baseLocation;
            spawnRate /= 1.01f;
            enemy.GetComponent<DestroyerAI>().moveSpeed = moveSpeed;
            timer = Time.time + spawnRate;
        }
    }

    Vector3 RandomXSpawn(float spread, Vector3 position)
    {
        return new Vector3(position.x + Random.Range(-spread, spread), position.y, position.z);
    }

}