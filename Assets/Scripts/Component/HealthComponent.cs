using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Subtract(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {health}");
        
        if (health <= 0)
        {
            Destroy();
            HandleDestruction();
        }
    }

    private void Destroy()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject); // Remove the entity from the game
    }

        private void HandleDestruction()
    {
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver); // Mengaktifkan OnDeath di Enemy
    }
}
