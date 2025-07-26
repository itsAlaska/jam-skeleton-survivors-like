using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Events")]
    public UnityEvent onDeath;
    public UnityEvent onDamageTaken;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        onDamageTaken?.Invoke();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameOverManager>()?.TriggerGameOver();
        gameObject.SetActive(false); // Optional: make player disappear
    }


    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}