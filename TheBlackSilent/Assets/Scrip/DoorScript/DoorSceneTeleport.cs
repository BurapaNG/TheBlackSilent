using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneTeleport : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad; // The target scene name
    public string destinationDoorName; // The name of the door to spawn at in next scene

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.E;

    private bool playerIsOverlapping;

    void Update()
    {
        if (playerIsOverlapping && Input.GetKeyDown(interactKey))
        {
            // Save destination info
            PlayerPrefs.SetString("SpawnDoor", destinationDoorName);
            SceneManager.LoadScene(sceneToLoad);
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
