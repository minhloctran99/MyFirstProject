using System;
using UnityEngine;

public enum PatrolAxis { Horizontal, Vertical } // Thêm tùy chọn trục tuần tra (Ngang hoặc Dọc)

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange = 6f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Behavior Settings")]
    [SerializeField] private bool canPatrol = true; 

    [Header("Patrol Settings")]
    [SerializeField] private Transform pointA; 
    [SerializeField] private Transform pointB;
    [SerializeField] private PatrolAxis patrolAxis = PatrolAxis.Horizontal; // Chọn Ngang (Horizontal) hoặc Dọc (Vertical)
    [SerializeField] private float patrolDistance = 3f; // Khoảng cách di chuyển qua lại
    
    private Vector2 startPosition;
    private bool movingToB = true;

    private EnemyState currentState = EnemyState.Patrol;
    private Transform currentPatrolPoint;
    private EnemyCombat enemyCombat;
    public bool IsChasing => currentState == EnemyState.Chase;
    public bool IsPatrolling => currentState == EnemyState.Patrol;
    public Transform Player => player;
    public Transform CurrentPatrolPoint => currentPatrolPoint;
    public float ReachDistance => ReachDistance;

    // Cung cấp vị trí mục tiêu tuần tra cho EnemyMovement
    public Vector2 CurrentPatrolTarget
    {
        get
        {
            // Nếu có gán Point A và Point B (đặt ngoài Scene), ưu tiên dùng điểm đó
            if (pointA != null && pointB != null)
            {
                return movingToB ? (Vector2)pointB.position : (Vector2)pointA.position;
            }
            else
            {
                // Ngược lại, tính toán tự động dựa trên trục đã chọn (Ngang hoặc Dọc)
                if (patrolAxis == PatrolAxis.Horizontal)
                {
                    float targetX = movingToB ? startPosition.x + patrolDistance : startPosition.x - patrolDistance;
                    return new Vector2(targetX, startPosition.y);
                }
                else // Chiều dọc (Lên / Xuống)
                {
                    float targetY = movingToB ? startPosition.y + patrolDistance : startPosition.y - patrolDistance;
                    return new Vector2(startPosition.x, targetY);
                }
            }
        }
    }

    private void Awake()
    {
        enemyCombat = GetComponent<EnemyCombat>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    private void Start()
    {
        // Ghi lại vị trí xuất phát chính xác ngay khi quái vừa được sinh ra
        startPosition = transform.position;
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        Debug.Log(gameObject.name + " State: " + currentState);
    }

    private void UpdateState()
    {
        if (player == null)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            ChangeState(EnemyState.Attack);
        }
        else if (distance <= chaseRange)
        {
            ChangeState(EnemyState.Chase);
        }
        else
        {
            if (canPatrol)
            {
                ChangeState(EnemyState.Patrol);
            }
            else
            {
                ChangeState(EnemyState.Idle);
            }
        }
    }

    private void Update()
    {
        if (player == null)
        {
            ChangeState(EnemyState.Idle);
            return;
        }

        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdle();
                break;
            case EnemyState.Patrol:
                HandlePatrol();
                break;
            case EnemyState.Chase:
                HandleChase();
                break;
            case EnemyState.Attack:
                HandleAttack();
                break;
        }
    }

    private void HandleIdle() { }
    private void HandleChase() { }

    private void HandleAttack()
    {
        if (enemyCombat != null)
        {
            enemyCombat.TryAttack(player);
        }
    }

    private void HandlePatrol()
    {
        Vector2 targetPos = CurrentPatrolTarget;
        
        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
        {
            movingToB = !movingToB;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, chaseRange);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, attackRange);

        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireSphere(pointB.position, 0.3f);
        }
    }
#endif
}
