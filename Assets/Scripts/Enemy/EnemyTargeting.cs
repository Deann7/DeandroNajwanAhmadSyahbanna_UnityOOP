using UnityEngine;

public class EnemyTargeting : Enemy
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float minSpawnDistance = -3f;
    [SerializeField] private float maxSpawnDistance = 3f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemy();
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void SpawnEnemy()
    {
        if (player == null) return;

        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector3 spawnDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.right;
        Vector3 spawnPosition = player.transform.position + (spawnDirection * randomDistance);

        transform.position = spawnPosition;
    }
}
