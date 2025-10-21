using UnityEngine;

public class PlayerHealth : PlayerController
{
    [Header("Player Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log("Player HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(" Player Dead!");

        if (animator != null)
        {
            
            animator.SetBool("IsDead", true);
        }
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.enabled = false;
        }
        enabled = false;
    }
}
