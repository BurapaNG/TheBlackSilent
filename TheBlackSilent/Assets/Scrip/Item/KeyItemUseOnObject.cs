using UnityEngine;

public class KeyItemUseOnObject : MonoBehaviour
{
    public string requiredItemName;      // Example: "Axe" or "Ladder"
    public GameObject keyPrefab;         // The key object that will be spawned
    public Transform spawnPoint;         // Where the key appears (on the ground)
    public bool destroyAfterInteraction = true;

    private bool playerInRange = false;
    private InventoryManager inventory;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
            TryUseRequiredItem();
    }

    private void TryUseRequiredItem()
    {
        if (!inventory.HasItem(requiredItemName))
        {
            Debug.Log("You need: " + requiredItemName);
            return;
        }

        // Spawn the key on the ground
        if (keyPrefab != null)
            Instantiate(keyPrefab, spawnPoint.position, Quaternion.identity);

        // Destroy object (barrel / shelf)
        if (destroyAfterInteraction)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = true;
        inventory = other.GetComponent<InventoryManager>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
