using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<HealthComponent>();
        }
    }

    public void Damage(int amount)
    {
        Debug.Log($"{gameObject.name} is being damaged by {amount} points.");
        health?.Subtract(amount);
    }

    public void Damage(Bullet bullet)
    {
        Debug.Log($"{gameObject.name} is being damaged by bullet with {bullet.damage} points.");
        Damage(bullet.damage);
    }
}
