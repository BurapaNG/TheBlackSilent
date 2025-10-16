using UnityEngine;

public class Eattack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 20;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;

    private float lastAttackTime = 0f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        // หา player โดย tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        StartAttack();
    }

    public void StartAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // เล่นแอนิเมชันโจมตี
            if (animator != null)
            {
                animator.SetTrigger("Attack");
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
