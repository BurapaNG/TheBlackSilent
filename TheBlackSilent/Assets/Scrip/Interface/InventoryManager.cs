using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    // ตัวแปรสำหรับ Singleton Pattern (เพื่อควบคุมให้มีแค่ตัวเดียว)
    public static InventoryManager Instance;

    // รายการของไอเทมที่ผู้เล่นเก็บได้ (คงอยู่ข้ามฉาก)
    private List<ItemPickup> collectedItems = new List<ItemPickup>();

    // รายการของ Image Component ที่ถูกค้นหาแบบ Dynamic (ถูกเติมใหม่ทุกฉาก)
    public List<Image> uiItemSlots = new List<Image>();

    void Awake()
    {
        // 1. จัดการ Singleton และ DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            // ทำให้ Player GameObject (ที่มีสคริปต์นี้) คงอยู่ข้ามฉาก
            DontDestroyOnLoad(gameObject);

            // 2. ลงทะเบียน Event เมื่อโหลดฉากใหม่เสร็จสมบูรณ์
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // ทำลาย Player ตัวใหม่ที่ถูกสร้างในฉากทิ้ง
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // ยกเลิกการลงทะเบียน Event เมื่อ GameObject นี้ถูกทำลาย
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ฟังก์ชัน Event ที่ถูกเรียกเมื่อโหลดฉากเสร็จ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // A. ค้นหา UI Slot ใหม่ในฉากปัจจุบัน
        FindAndSetupUI();

        // B. อัปเดต UI ด้วยไอเทมที่เก็บไว้แล้ว
        UpdateInventoryUI();

        // C. จัดการตำแหน่งเกิดของผู้เล่นให้ตรงกับ Spawn Point
        HandlePlayerSpawn();
    }

    /// <summary>
    /// ค้นหา UI Slot Image ใหม่ในฉากปัจจุบัน
    /// </summary>
    private void FindAndSetupUI()
    {
        uiItemSlots.Clear();

        // ค้นหา Slot ทั้งหมด (Slot1 ถึง Slot6) - ต้องแน่ใจว่าชื่อตรงกันทุกฉาก
        for (int i = 1; i <= 8; i++)
        {
            GameObject slotObject = GameObject.Find("Slot" + i);
            if (slotObject != null)
            {
                Image slotImage = slotObject.GetComponent<Image>();
                if (slotImage != null)
                {
                    uiItemSlots.Add(slotImage);
                }
            }
        }

        Debug.Log("Inventory UI Slots set up. Total found: " + uiItemSlots.Count);
    }

    /// <summary>
    /// ย้ายผู้เล่นไปยังจุดเกิดที่กำหนดโดย DoorSceneTeleport.cs
    /// </summary>
    private void HandlePlayerSpawn()
    {
        string spawnPointName = PlayerPrefs.GetString("SpawnDoor", "");

        if (!string.IsNullOrEmpty(spawnPointName))
        {
            // หา Spawn Point ในฉากปัจจุบัน
            GameObject spawnPoint = GameObject.Find(spawnPointName);

            if (spawnPoint != null)
            {
                // ย้ายผู้เล่นไปที่ตำแหน่ง Spawn Point
                transform.position = spawnPoint.transform.position;
                Debug.Log("Player moved to spawn point: " + spawnPointName);

                // (ทางเลือก: ล้างค่า PlayerPrefs.DeleteKey("SpawnDoor");)
            }
            else
            {
                Debug.LogWarning("Spawn point \"" + spawnPointName + "\" not found in the current scene. Player will remain at the default location.");
            }
        }
    }

    // ฟังก์ชัน AddItem (เหมือนเดิม)
    public void AddItem(ItemPickup item)
    {
        if (collectedItems.Count < uiItemSlots.Count)
        {
            collectedItems.Add(item);
            UpdateInventoryUI();
            Debug.Log("Picked up item: " + item.itemName);
        }
        else
        {
            Debug.Log("Inventory is Full!");
        }
    }

    // ฟังก์ชัน UpdateInventoryUI (เหมือนเดิม)
    private void UpdateInventoryUI()
    {
        for (int i = 0; i < uiItemSlots.Count; i++)
        {
            if (i < collectedItems.Count)
            {
                uiItemSlots[i].sprite = collectedItems[i].itemIcon;
                uiItemSlots[i].enabled = true;
            }
            else
            {
                uiItemSlots[i].enabled = false;
            }
        }
    }

    // ฟังก์ชัน HasItem (เหมือนเดิม)
    public bool HasItem(string requiredItemName)
    {
        foreach (var item in collectedItems)
        {
            if (item.itemName == requiredItemName)
            {
                return true;
            }
        }
        return false;
    }
}