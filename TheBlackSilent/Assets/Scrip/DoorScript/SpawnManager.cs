using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Start()
    {
        // 1. ดึงชื่อจุดเกิดที่ถูกบันทึกไว้ตอนออกจากฉากที่แล้ว
        string savedSpawnPointName = PlayerPrefs.GetString("SpawnDoor", "");

        // ถ้าไม่มีการบันทึกชื่อจุดเกิด (อาจเป็นครั้งแรกที่เข้าฉาก หรือเข้าฉากปกติ) ก็ไม่ต้องทำอะไร
        if (string.IsNullOrEmpty(savedSpawnPointName))
        {
            // ลบค่าที่บันทึกไว้เพื่อป้องกันการสปอนผิดที่ในการโหลดฉากครั้งถัดไป
            PlayerPrefs.DeleteKey("SpawnDoor");
            return;
        }

        // 2. ค้นหา GameObject ที่มี Tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Error: Cannot find GameObject with tag 'Player'. Make sure your player has the correct tag.");
            return;
        }

        // 3. ค้นหาจุดเกิด (GameObject) ในฉากปัจจุบันที่มีชื่อตรงกับที่บันทึกไว้
        GameObject spawnPoint = GameObject.Find(savedSpawnPointName);

        if (spawnPoint != null)
        {
            // 4. ย้ายผู้เล่นไปยังตำแหน่งของจุดเกิด
            // เพื่อให้ผู้เล่นไม่ติดอยู่ในประตูตอนเกิด, เราจะใช้ตำแหน่งของ Transform
            player.transform.position = spawnPoint.transform.position;

            // ลบค่าที่บันทึกไว้หลังจากใช้งานเสร็จ
            PlayerPrefs.DeleteKey("SpawnDoor");
            Debug.Log($"Player spawned at: {savedSpawnPointName}");
        }
        else
        {
            Debug.LogWarning($"Spawn point '{savedSpawnPointName}' not found in the current scene. Player will remain at the default spawn location.");
        }
    }
}