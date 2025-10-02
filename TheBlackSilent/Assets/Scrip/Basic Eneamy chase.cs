using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;      // Reference to the player
    public float speed = 3f;      // How fast the enemy moves
    public float chaseRange = 10f; // Distance before chasing
    public float stopDistance = 1.5f; // Minimum distance from player

    private void Update()
    {
        if (player == null) return;

        // Get distance between enemy and player
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < chaseRange && distance > stopDistance)
        {
            // Move toward the player
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }
}
