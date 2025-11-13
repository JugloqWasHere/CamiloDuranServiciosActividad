using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject fallingPrefab;
    public float spawnInterval = 1.0f;
    public float spawnRangeX = 6f;
    public float spawnHeight = 10f;
    public bool spawning = false;

    private float timer = 0f;
    private Transform parent;

    void Start()
    {
        parent = new GameObject("FallingObjects").transform;
    }

    void Update()
    {
        if (!spawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnHeight, 0f) + transform.position;
        GameObject go = Instantiate(fallingPrefab, pos, Quaternion.identity, parent);
    
    }

    public void StartSpawning() => spawning = true;
    public void StopSpawning() => spawning = false;
}
