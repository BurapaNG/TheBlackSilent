using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // พูดถึงตัวของ player
    public float walkSpeed = 1.5f; // <--- ความเร็วในการเดิน (ตอนไม่เห็น/เดินผ่าน)
    public float runSpeed = 5f; // <--- ความเร็วในการวิ่ง (ตอนตามผู้เล่น)
    public float stopDistance = 1f; // ระยะในการหยุด
    public bool flipSpirit = true; // การหันหน้าตาม player
    public LayerMask obstacleMask; // กำหนด Layer ของสิ่งกีดขวาง

    [Header("Movement Sound Settings")]
    // ลากไฟล์เสียงก้าวเดิน/วิ่ง มาใส่ใน Inspector
    public AudioClip walkSound;
    public AudioClip runSound;
    

    public bool isAttacking = false;
    private bool playerVisible = true; // ถ้า false จะไม่ตาม/โจมตี player
    private Rigidbody2D rb;
    public Animator animator;
    private AudioSource audioSource; // **เพิ่ม AudioSource**

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // **รับหรือเพิ่ม AudioSource**
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // stepTimer = timeBetweenSteps; // ไม่ใช้แล้วในวิธีนี้

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
            if (enemyColliders != null){
                // ศัตรูเดินผ่านไปแบบไม่สนใจตัวของผู้เล่น
                Vector2 moveDir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
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

        // ... (ส่วนการตามหา/โจมตี) ...
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleMask);
        bool hasObstacle = hit.collider != null;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance && !hasObstacle)
        {isAttacking = false;
            if (animator != null)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
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
            // ...
        }
        else // ถ้ามีสิ่งกีดขวางหรือเงื่อนไขอื่น ๆ ที่ทำให้หยุดนิ่ง
        { if (animator != null){
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }}
        // ... (ส่วนการกลับทิศทาง) ...

        if (flipSpirit)
        {
            Vector3 scale = transform.localScale;
            scale.x = player.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    // ฟังก์ชันช่วยสำหรับเล่นเสียงก้าวเท้า
    // สามารถวางไว้ตรงไหนก็ได้ในคลาส แต่ตามธรรมเนียมมักจะวางไว้ท้ายสุดก่อน Attack()
    private void PlayFootstep(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            // ใช้ PlayOneShot เพื่อให้เสียงไม่ถูกหยุดโดยเสียงร้องหรือเสียงอื่น ๆ
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    }

    // ฟังก์ชันสาธารณะที่จะถูกเรียกจาก Animation Event ของแอนิเมชันเดิน
    public void PlayFootstepSound_Walk()
    {
        PlayFootstep(walkSound);
    }

    // ฟังก์ชันสาธารณะที่จะถูกเรียกจาก Animation Event ของแอนิเมชันวิ่ง
    public void PlayFootstepSound_Run()
    {
        PlayFootstep(runSound);
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
    }}