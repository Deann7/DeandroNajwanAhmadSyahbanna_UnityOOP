using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 40;
    public int damage = 10;
    private Rigidbody2D rb;
    private IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool
    {
        set => objectPool = value;
    }

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) 
        {
            return;
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            Debug.Log($"{gameObject.name} collided with {other.gameObject.name} and is dealing {damage} damage.");
            hitbox.Damage(damage);
        }

        objectPool.Release(this);
    }

    public void ResetBullet()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        transform.rotation = Quaternion.identity;
    }
}
