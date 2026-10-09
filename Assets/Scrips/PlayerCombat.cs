using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private Transform attackPoint;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private int attackDamage = 20;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float attackCooldown = 0.5f;

    private float nextAttackTime;

    [SerializeField]
    private LineRenderer attackCircle;

    [SerializeField]
    private int circleSegments = 60;

    private void Start()
    {
        DrawAttackCircle();
    }
    private void DrawAttackCircle()
    {
        if (attackCircle == null)
            return;

        attackCircle.useWorldSpace = false;
        attackCircle.loop = true;
        attackCircle.positionCount = circleSegments;
        attackCircle.startWidth = 0.03f;
        attackCircle.endWidth = 0.03f;

        for (int i = 0; i < circleSegments; i++)
        {
            float angle = i * Mathf.PI * 2f / circleSegments;

            float x = Mathf.Cos(angle) * attackRange;
            float y = Mathf.Sin(angle) * attackRange;

            attackCircle.SetPosition(
            i,
            new Vector3(x, y, 0)
            );
        }
    }

    private void Attack()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                enemyLayer
            );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemyHealth =
                hit.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
        Time.time >= nextAttackTime)

        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }
}
