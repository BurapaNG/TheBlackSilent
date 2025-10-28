using UnityEngine;

public class DoorSpawnPoint : MonoBehaviour
{
    void Start()
    {
        string savedDoor = PlayerPrefs.GetString("SpawnDoor", "");

        // Only move player if this door was the saved destination
        if (savedDoor == name)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // --- Option 3: Use child SpawnPoint ---
                Transform spawnPoint = transform.Find("SpawnPoint");

                if (spawnPoint != null)
                    player.transform.position = spawnPoint.position;
                else
                    player.transform.position = transform.position;
            }
        }
    }
}
