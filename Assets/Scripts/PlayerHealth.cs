using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private HealthBarUI healthBarUI;
    [SerializeField]
    private GameManager gameManager;

    private int currentHealth;

    [SerializeField]
    private HealthTextUI healthTextUI;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (healthBarUI != null)
        {
            healthBarUI.SetMaxHealth(maxHealth);
        }

        if (healthTextUI != null)
        {
            healthTextUI.SetHealthText(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth == 0)
        {
            return;
        }

        currentHealth =
        Mathf.Max(currentHealth - damage, 0);

        // Debug.Log("Player Health: " + currentHealth);
        if (healthBarUI != null)
        {
            healthBarUI.SetHealth(currentHealth);
        }

        if (healthTextUI != null)
        {
            healthTextUI.SetHealthText(currentHealth, maxHealth);
        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        if (gameManager != null)
        {
            gameManager.GameOver();
        }
        gameObject.SetActive(false);
    }
}