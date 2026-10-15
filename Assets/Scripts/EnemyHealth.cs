using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 50;

    [SerializeField]
    private float knockbackForce = 5f;

    private int currentHealth;
    private bool isDead = false;
    private EnemySpawner enemySpawner;

    private Rigidbody2D rb;

    [SerializeField]
    private float knockbackDuration = 0.2f;

    private EnemyMovement enemyMovement;

    [System.Obsolete]
    private void Awake()
    {
        currentHealth = maxHealth;
        enemySpawner =
            FindFirstObjectByType<EnemySpawner>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void ApplyKnockback(Vector2 attackerPosition)
    {
        if (rb == null)
        {
            return;
        }

        Vector2 direction =
            (rb.position - attackerPosition).normalized;

        if (enemyMovement != null)
        {
            enemyMovement.ApplyKnockbackDelay(knockbackDuration);
        }

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(
            direction * knockbackForce,
            ForceMode2D.Impulse
        );
    }

    public void TakeDamage(int damage, Vector2 attackerPosition)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);

        Debug.Log("Enemy Health: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
        ApplyKnockback(attackerPosition);
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
