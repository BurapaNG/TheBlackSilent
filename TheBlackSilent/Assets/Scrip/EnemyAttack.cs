using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int damage = 20;
    public float attackCooldown = 2f;
    public float attackRange = 1.5f;

    private float lastAttackTime = 0f;
    private Transform player;

    void Start()
    {
        // หา player โดย tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    public void StartAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("⚔️ Enemy attacked Player for " + damage);
            }

            lastAttackTime = Time.time;
        }
    }
}
