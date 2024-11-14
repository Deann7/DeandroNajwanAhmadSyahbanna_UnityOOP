using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) // Prevent friendly fire
        {
            return;
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        Invincibility invincibility = other.GetComponent<Invincibility>();
        if (hitbox != null && invincibility != null && !invincibility.isInvincible)
        {
            Debug.Log($"{gameObject.name} collided with {other.gameObject.name} and is dealing {damage} damage.");
            hitbox.Damage(damage);
            invincibility.StartInvincibility();

        }
        if (bullet != null ){
            hitbox.Damage(bullet);
        }

    }
}
