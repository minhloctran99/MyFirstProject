using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private EnemyAI enemyAI;
    private float knockbackEndTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        if (Time.time < knockbackEndTime) return;

        // Chỉ di chuyển khi đang ở trạng thái Chase HOẶC Patrol
        if (!enemyAI.IsChasing && !enemyAI.IsPatrolling) return;

        Vector2 targetPosition = Vector2.zero;

        // Xác định mục tiêu tùy thuộc vào trạng thái hiện tại của AI
        if (enemyAI.IsChasing)
        {
            Transform player = enemyAI.Player;
            if (player == null) return;
            targetPosition = player.position;
        }
        else if (enemyAI.IsPatrolling)
        {
            targetPosition = enemyAI.CurrentPatrolTarget;
        }

        // Thực hiện di chuyển vật lý bằng Rigidbody2D
        Vector2 direction = (targetPosition - rb.position).normalized;
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;
        
        rb.MovePosition(newPosition);
    }

    public void ApplyKnockbackDelay(float duration)
    {
        knockbackEndTime = Time.time + duration;
    }
}