using UnityEngine;

public class EnemyForward : Enemy
{
    [SerializeField] private float topBound = 5f;
    [SerializeField] private float bottomBound = -5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minSpawnX = -8f;
    [SerializeField] private float maxSpawnX = 8f;

    private bool movingUp;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        MoveEnemy();
        CheckScreenBounds();
    }

    private void SpawnEnemy()
    {
        float spawnX = Random.Range(minSpawnX, maxSpawnX);
        float spawnY = topBound;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        transform.position = spawnPosition;
        movingUp = false; // Ensure enemy starts moving downward
    }

    private void MoveEnemy()
    {
        Vector3 direction = movingUp ? Vector3.up : Vector3.down;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckScreenBounds()
    {
        if (transform.position.y <= bottomBound)
        {
            movingUp = true;
        }
        else if (transform.position.y >= topBound)
        {
            movingUp = false;
        }
    }
}
