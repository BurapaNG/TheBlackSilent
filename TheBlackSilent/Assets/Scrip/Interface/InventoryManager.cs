using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ต้องเพิ่มเข้ามาสำหรับจัดการ Scene Event

public class InventoryManager : MonoBehaviour
{
    // ตัวแปรสำหรับ Singleton Pattern (เพื่อควบคุมให้มีแค่ตัวเดียว)
    public static InventoryManager Instance;

    // รายการของไอเทมที่ผู้เล่นเก็บได้ (เก็บ ItemPickup component ไว้)
    private List<ItemPickup> collectedItems = new List<ItemPickup>();

    // รายการของ Image Component ที่ถูกค้นหาแบบ Dynamic
    private List<Image> uiItemSlots = new List<Image>();

    void Awake()
    {
        // 1. จัดการ Singleton และ DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 2. ลงทะเบียน Event เมื่อโหลดฉากใหม่เสร็จสมบูรณ์
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // ทำลายตัวซ้ำซ้อนทิ้ง
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันที่ถูกเรียกเมื่อ GameObject นี้ถูกทำลาย (เพื่อยกเลิกการลงทะเบียน Event)
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ฟังก์ชัน Event ที่ถูกเรียกเมื่อโหลดฉากเสร็จ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. ค้นหา UI Slot ใหม่ในฉากปัจจุบัน
        FindAndSetupUI();

        // 2. อัปเดต UI ด้วยไอเทมที่เก็บไว้แล้ว (ใช้สำหรับแสดงไอคอนทันทีหลังเปลี่ยนฉาก)
        UpdateInventoryUI();
    }

    /// <summary>
    /// ค้นหา UI Slot Image ใหม่ในฉากปัจจุบัน
    /// </summary>
    private void FindAndSetupUI()
    {
        uiItemSlots.Clear(); // ล้าง List เก่าออกก่อน

        // **ค้นหา Image Component ที่มีชื่อตามรูปแบบ Slot (ต้องตรงกับชื่อใน Hierarchy)**
        // ตัวอย่างเช่น: Slot1, Slot2, Slot3, ...

        // เราจะค้นหา Slot ทั้งหมด 6 ช่อง ตามที่คุณได้สร้างไว้ในรูป
        for (int i = 1; i <= 6; i++)
        {
            GameObject slotObject = GameObject.Find("Slot" + i);
            if (slotObject != null)
            {
                Image slotImage = slotObject.GetComponent<Image>();
                if (slotImage != null)
                {
                    uiItemSlots.Add(slotImage);
                }
                else
                {
                    Debug.LogWarning("Slot" + i + " found but no Image component attached.");
                }
            }
        }

        Debug.Log("Inventory UI Slots set up. Total found: " + uiItemSlots.Count);
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