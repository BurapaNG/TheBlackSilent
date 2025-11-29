using UnityEngine;
using UnityEngine.SceneManagement; // ต้องเพิ่มเข้ามาสำหรับจัดการ Scene Event

public class CameraController : MonoBehaviour
{

    public float yOffset = 1f;
    // เปลี่ยนเป็น private เพื่อให้โค้ดจัดการการค้นหาเอง
    private Transform player;

    void Awake()
    {
        // 1. ลงทะเบียน Event เมื่อโหลดฉากใหม่เสร็จสมบูรณ์
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 2. ค้นหาผู้เล่นทันทีเมื่อกล้องถูกสร้างในฉากแรก
        FindPlayerTarget();
    }

    void OnDestroy()
    {
        // 3. ยกเลิกการลงทะเบียน Event เมื่อกล้องถูกทำลาย
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 4. ฟังก์ชัน Event ที่ถูกเรียกเมื่อโหลดฉากเสร็จ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayerTarget(); // สั่งให้ค้นหาผู้เล่นใหม่ทุกครั้งที่เปลี่ยนฉาก
    }

    /// <summary>
    /// ค้นหา GameObject ของผู้เล่นที่ข้ามฉากมา
    /// </summary>
    private void FindPlayerTarget()
    {
        // ค้นหา GameObject ที่มี Tag เป็น "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            // ถ้าเจอผู้เล่น ให้กำหนด player เป็น Transform ของผู้เล่น
            player = playerObject.transform;
            Debug.Log("Camera successfully found new Player target: " + playerObject.name);
        }
        else
        {
            // ถ้ายังไม่เจอ อาจเป็นเพราะผู้เล่นยังไม่ถูก Spawn หรือ Tag ผิด
            Debug.LogWarning("CameraController: Cannot find GameObject with tag 'Player'.");
        }
    }

    private void LateUpdate()
    {
        // ต้องตรวจสอบว่าเจอผู้เล่นแล้วจึงจะตาม
        if (player == null)
        {
            // ลองค้นหาอีกครั้งเผื่อว่าผู้เล่นถูกสร้างช้า
            FindPlayerTarget();
            if (player == null) return; // ถ้ายังไม่เจอก็หยุดการตาม
        }

        // Logic การตามกล้องเดิมของคุณ
        Vector3 newPos = new Vector3(player.position.x, player.position.y + yOffset, -10f);

        // กำหนดตำแหน่งใหม่
        transform.position = newPos;
    }}