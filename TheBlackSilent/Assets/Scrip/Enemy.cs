using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // พูดถึงตัวของ player
    public float speed = 3f; // ความเร็ว
    public float stopDistance = 1f; // ระยะในการหยุด
    public bool flipSpirit = true; // การหันหน้าตาม player
    public LayerMask obstacleMask; // กำหนด Layer ของสิ่งกีดขวาง

    public bool isAttacking = false;
    private bool playerVisible = true; // ถ้า false จะไม่ตาม/โจมตี player

    void Update()
    {
        if (player == null) return;

        // ถ้า player ถูกตั้งเป็นมองไม่เห็น ศัตรูจะไม่ทำอะไร
        if (!playerVisible)
        {
            isAttacking = false;
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
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
        else if (!hasObstacle)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Attack();
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
