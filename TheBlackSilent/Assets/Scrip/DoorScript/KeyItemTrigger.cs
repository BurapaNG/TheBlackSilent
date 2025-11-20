using UnityEngine;

public class KeyItemTrigger : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.F;

    [Header("Objects To Spawn & Destroy")]
    public GameObject newDoor;        // the door that will spawn
    public GameObject oldDoor;        // the door that will be destroyed
    public GameObject keyItemToDestroy; // the key item object (usually THIS object)

    private bool playerIsOverlapping;

    void Update()
    {
        if (playerIsOverlapping && Input.GetKeyDown(interactKey))
        {
            if (newDoor != null)
                Instantiate(newDoor, newDoor.transform.position, newDoor.transform.rotation);

            if (oldDoor != null)
                Destroy(oldDoor);

            if (keyItemToDestroy != null)
                Destroy(keyItemToDestroy);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = false;
    }
}
