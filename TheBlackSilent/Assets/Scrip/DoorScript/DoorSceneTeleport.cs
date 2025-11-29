using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorSceneTeleport : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad; // The target scene name
    public string destinationDoorName; // The name of the door to spawn at in next scene

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.F;

    [Header("Audio Settings")]
    public AudioSource doorAudioSource;
    public float audioDuration = 0.5f;

    [Header("Key Requirement")]
    public string requiredKeyName; // ใส่ชื่อไอเทมกุญแจที่ต้องใช้เปิด เช่น "Key_Red"

    // เปลี่ยนจาก bool เป็น Collider2D เพื่อเก็บ reference ของผู้เล่น
    private Collider2D playerCollider = null;

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ข้างในและกดปุ่ม
        if (playerCollider != null && Input.GetKeyDown(interactKey))
        {
            // 1. ดึง InventoryManager จาก Collider ของผู้เล่น
            InventoryManager inventory = playerCollider.GetComponent<InventoryManager>();

            // 2. ตรวจสอบเงื่อนไขกุญแจ
            // ถ้ามีการกำหนด requiredKeyName และผู้เล่นไม่มีกุญแจ
            if (!string.IsNullOrEmpty(requiredKeyName))
            {
                if (inventory == null || !inventory.HasItem(requiredKeyName))
                {
                    Debug.Log("Door is locked! Required Key: " + requiredKeyName);
                    // (สามารถเพิ่ม UI Feedback ตรงนี้เพื่อบอกผู้เล่นว่าประตูถูกล็อค)
                    return; // หยุดการทำงานถ้าไม่มีกุญแจ
                }
            }

            // ถ้าผ่านเงื่อนไข (มีกุญแจ หรือไม่ต้องใช้กุญแจ)
            StartCoroutine(LoadSceneAfterAudio());
        }
    }

    // Coroutine ที่ใช้ในการหน่วงเวลา (ไม่มีการเปลี่ยนแปลง)
    IEnumerator LoadSceneAfterAudio()
    {
        // 1. เล่นเสียง
        if (doorAudioSource != null)
        {
            doorAudioSource.Play();
        }

        // 2. รอตามระยะเวลาของเสียงที่กำหนด
        yield return new WaitForSeconds(audioDuration);

        // 3. บันทึกข้อมูลและเปลี่ยนฉาก
        PlayerPrefs.SetString("SpawnDoor", destinationDoorName);
        SceneManager.LoadScene(sceneToLoad);
    }

    // ใช้ OnTriggerEnter2D เพื่อเก็บ Collider Reference
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = other;
        }
    }

    // ใช้ OnTriggerExit2D เพื่อล้าง Collider Reference
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCollider = null;
        }
    }
}