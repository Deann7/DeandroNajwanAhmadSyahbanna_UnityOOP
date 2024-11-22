using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityAction<Enemy> OnEnemyDefeated;
    [SerializeField] public int level; // Level musuh

    public void OnDeath()
    {
        OnEnemyDefeated?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (gameObject.activeInHierarchy) // Pastikan OnDeath tidak dipanggil dua kali
            OnDeath();
    }
}
