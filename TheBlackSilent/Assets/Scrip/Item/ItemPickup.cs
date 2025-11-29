using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    // กำหนดชื่อไอเทมที่จะใช้แสดงผลบน UI
    public string itemName = "Default Item";

    // อ้างอิงถึง Sprite ของไอเทมสำหรับแสดงผลบน UI
    public Sprite itemIcon;
    public TextMeshProUGUI pickupTextPrompt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pickupTextPrompt != null)
        {
            // แสดงข้อความ "กด E" เมื่อผู้เล่นเข้ามาในรัศมี
            pickupTextPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // ตรวจสอบว่าวัตถุที่ชนคือผู้เล่น และผู้เล่นกดปุ่ม 'E'
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // พยายามเพิ่มไอเทมเข้า Inventory ของผู้เล่น
            InventoryManager inventory = other.GetComponent<InventoryManager>();
            if (inventory != null)
            {
                inventory.AddItem(this);
                if (pickupTextPrompt != null)
                {
                    pickupTextPrompt.gameObject.SetActive(false);
                }

                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pickupTextPrompt != null)
        {
            // ซ่อนข้อความ "กด E" เมื่อผู้เล่นเดินออก
            pickupTextPrompt.gameObject.SetActive(false);
        }
    }

    }