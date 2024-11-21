using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private EnemySpawner spawner;
    public int level = 1;

    public void SetSpawner(EnemySpawner enemySpawner)
    {
        spawner = enemySpawner;
    }

    public void OnDeath()
    {
        spawner?.NotifyEnemyDestroyed(this); // Melaporkan diri ke spawner
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDeath();
    }
}

