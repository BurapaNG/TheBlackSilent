using UnityEngine;

public class Eattack : Enemy
{
    [Header("Attack Settings")]
    public int damage = 20;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;
    

    private float lastAttackTime = 0f;
    private Transform player;
    public Animator animator;

    void Start()
    {
        // หา player โดย tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;
        StartAttack();
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
            

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log(" Enemy attacked Player for " + damage);
            }

            lastAttackTime = Time.time;
        }
    }
}
