using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public KeyCode interactionKey = KeyCode.F;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true);
                interactionText.text = "Press " + interactionKey.ToString() + " to open";
            }
        }
    }

    // ส่วนนี้จะถูกเรียกเมื่อวัตถุอื่นออกจากระยะ
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ปิดใช้งาน Text UI และซ่อนข้อความ
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}