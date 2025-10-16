using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public LayerMask obstacleMask;
    private EnemyAttack attackScript; // <- ลิงก์ไปหา EnemyAttack.cs

    [Header("Movement Settings")]
    public float speed = 3f;
    public float stopDistance = 1.2f; // ระยะเริ่มโจมตี
    public bool flipSprite = true;

    private Rigidbody2D rb;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackScript = GetComponent<EnemyAttack>(); // เชื่อมสคริปต์โจมตีเข้ามา
    }

    void Update()
    {
        if (player == null) return;

        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // ตรวจสิ่งกีดขวาง
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask);
        bool hasObstacle = hit.collider != null;

        // ถ้าไม่มีสิ่งกีดขวาง และยังอยู่ไกล → เดินเข้าหา
        if (distanceToPlayer > stopDistance && !hasObstacle)
        {
            isAttacking = false;
            Vector2 direction = directionToPlayer.normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else if (!hasObstacle && !isAttacking)
        {
            // เข้าโหมดโจมตี
            isAttacking = true;
            attackScript.StartAttack(); // ← เรียกฟังก์ชันจาก EnemyAttack.cs
        }

        // หันหน้าตาม player
        if (flipSprite)
        {
            Vector3 scale = transform.localScale;
            scale.x = player.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }
}