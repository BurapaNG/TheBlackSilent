using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // พูดถึงตัวของ player
    public float walkSpeed = 1.5f; // <--- ความเร็วในการเดิน (ตอนไม่เห็น/เดินผ่าน)
    public float runSpeed = 5f; // <--- ความเร็วในการวิ่ง (ตอนตามผู้เล่น)
    public float stopDistance = 1f; // ระยะในการหยุด
    public bool flipSpirit = true; // การหันหน้าตาม player
    public LayerMask obstacleMask; // กำหนด Layer ของสิ่งกีดขวาง
    public float visionRange = 10f; // ระยะการมองเห็น
    public float despawnTime = 5f; // เวลาในการหายตัวหลังจากไม่เห็น player


    public bool isAttacking = false;
    public float currentDespawnTime;
    private bool playerVisible = true; // ถ้า false จะไม่ตาม/โจมตี player
    private bool despawnTimmerActive;
    private Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (rb == null)
            Debug.LogError(" Missing Rigidbody 2D on Enemy");
    }

    void Update()
    {
        if (player == null) return;

        // ถ้า player ถูกตั้งเป็นมองไม่เห็น ศัตรูจะไม่ทำอะไร
        if (!playerVisible)
        {
            isAttacking = false;

            Collider2D enemyColliders = GetComponent<Collider2D>();
            if (enemyColliders != null)
            {
                

                // ศัตรูเดินผ่านไปแบบไม่สนใจตัวของผู้เล่น
                Vector2 moveDir = transform.localScale.x > 0 ? Vector2.right  : Vector2.left;
                rb.MovePosition(rb.position + moveDir * walkSpeed * 0.5f * Time.fixedDeltaTime);

                if (animator != null)
                {
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false); 
                }

                Debug.Log("Enemy Can't see player. Walking past...");
            }
            return;
        }

        // ตรวจจับสิ่งกีดขวางระหว่าง Enemy กับ Player
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask);
        bool hasObstacle = hit.collider != null;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance && !hasObstacle)
        {
            isAttacking = false;
            //ส่วนควบคุมแอนิเมชันการเดิน/วิ่ง
            if (animator != null)
            {
                animator.SetBool("isWalking", false); // <--- ปิดเดิน
                animator.SetBool("isRunning", true); // <--- เปิดวิ่ง
            }

            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * runSpeed * Time.deltaTime;
        }
        else if (!hasObstacle)
        {
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false); 
            }

            if (!isAttacking)
            {
                isAttacking = true;
                Attack();
            }
            else // hasObstacle เป็นจริง หรือ playerVisible เป็นเท็จ
            {
                
                // ********** ส่วนควบคุมแอนิเมชัน Idle **********
                if (animator != null)
                {
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", false);

                }
                // ****************************************************
            }
        }
        // ถ้ามีสิ่งกีดขวาง ศัตรูจะไม่เดินและไม่โจมตี

        if (flipSpirit)
        {
            Vector3 scale = transform.localScale;
            scale.x = player.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void CheckPlayVisible()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask);
        bool hasObstacle = hit.collider != null;
        if (distanceToPlayer <= visionRange && !hasObstacle)
        {
            SetPlayerVisible(true);
        }
        else
        {
            SetPlayerVisible(false);
        }
    }

    void StartDespawnTimer() 
    {
        despawnTimmerActive = true;
        currentDespawnTime = despawnTime;
    }

    void ResetDespawnTimer()
    {
        despawnTimmerActive = false;
        currentDespawnTime = despawnTime;
    }

    void DespawnEnemy()
    {
        Debug.Log("Enemy despawned");
        Destroy(gameObject);
    }

    void Attack()
    {
        Debug.Log("Enemy attacks");
    }

    // API เพื่อบอกให้ศัตรูรู้ว่าเห็น player หรือไม่
    public void SetPlayerVisible(bool visible)
    {
        playerVisible = visible;
        if (!playerVisible)
        {
            isAttacking = false;
        }
    }
}
