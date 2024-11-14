using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.5f;

    [Header("Bullets")]
    public Bullet bulletPrefab;  // Renamed for clarity
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    public IObjectPool<Bullet> objectPool;

    private bool collectionCheck = false;
    private int defaultCapacity = 30;
    private int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        // Initialize the object pool with correct delegate setup
        objectPool = new ObjectPool<Bullet>(
            BulletFactory,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    private Bullet BulletFactory()
    {
        Bullet newBullet = Instantiate(bulletPrefab, parentTransform);
        newBullet.ObjectPool = objectPool;  // Assign the pool to the bullet
        return newBullet;
    }

    private void OnTakeFromPool(Bullet bullet)
    {
        // Set the bullet's position and rotation when taken from the pool
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(Bullet bullet)
    {
        // Deactivate the bullet when returned to the pool
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    private void Shoot()
    {
        Bullet bullet = objectPool.Get();  // Get a bullet from the pool
    }
}
