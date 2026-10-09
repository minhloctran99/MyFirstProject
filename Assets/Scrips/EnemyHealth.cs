using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;
    private int currentHealth;

    private bool isDead = false;
    private EnemySpawner enemySpawner;

    [System.Obsolete]
    private void Awake()
    {
        currentHealth = maxHealth;
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    public void TakeDamage(int damge)
    {
        if (isDead || damge <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth -damge, 0);

        Debug.Log("Enemy Health: " + currentHealth);

        if(currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        if (enemySpawner != null)
        {
            enemySpawner.SpawnEnemy();
        }
        Destroy(gameObject);
    }
}
