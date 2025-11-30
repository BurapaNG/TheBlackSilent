using UnityEngine;

public class Eattack : Enemy
{
    [Header("Attack Settings")]
    public int damage = 20;
    public float attackCooldown = 5f;
    public float attackRange = 1.5f;

    [Header("Sound Settings")]
    // ลากไฟล์เสียง (AudioClip) ของมอนสเตอร์มาใส่ใน Inspector
    public AudioClip sightingSound;
    private AudioSource audioSource;
    // ระยะเวลา Cooldown ของการเล่นเสียงร้อง (เช่น 5 วินาที)
    public float soundCooldown = 5f;
    private float lastSoundTime = -10f; // เริ่มต้นให้เล่นได้ทันที

    private float lastAttackTime = 1f;
    private Transform player;
    public Animator animator;

    void Start()
    {
        // หา player โดย tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        animator = this.GetComponent<Animator>();

        // รับหรือเพิ่ม AudioSource
        audioSource = this.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float sightingRange = attackRange * 1.5f; // กำหนดระยะการมองเห็น
        float currentDistance = Vector3.Distance(transform.position, player.position);

        // **ตรวจสอบเงื่อนไข: ผู้เล่นอยู่ในระยะการมองเห็น และเสียงร้องพร้อมใช้งาน**
        if (currentDistance <= sightingRange && Time.time >= lastSoundTime + soundCooldown)
        {
            PlaySightingSound();
        }

        StartAttack();
    }

    // ฟังก์ชันสำหรับเล่นเสียงเมื่อเห็นผู้เล่น
    public void PlaySightingSound()
    {
        if (audioSource != null && sightingSound != null)
        {
            // อัปเดตเวลาการเล่นเสียงครั้งล่าสุด
            lastSoundTime = Time.time;
            // เล่นเสียง
            audioSource.PlayOneShot(sightingSound);
            Debug.Log("Monster Sighting Sound Played!");
        }
    }

    public void StartAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        Enemy enemy = this.GetComponent<Enemy>();

        if (enemy.isAttacking == true && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // เล่นแอนิเมชันโจมตี
            if (animator != null)
            {
                animator.SetTrigger("isAttacking 0");
            }

            // Note: เสียงร้องจะถูกเรียกใน Update() เมื่อพบผู้เล่น ไม่ใช่ที่นี่

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log(" Enemy attacked Player for " + damage);
            }

            lastAttackTime = Time.time;
        }
    }}