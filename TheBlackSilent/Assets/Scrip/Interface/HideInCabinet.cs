using UnityEngine;

public class HideInCabinet : MonoBehaviour
{
    private Transform hidePoint;
    private BoxCollider2D cabinetCollider;

    // 📢 ตัวแปรใหม่สำหรับ AudioSource ของตู้
    private AudioSource cabinetAudioSource;

    private void Start()
    {
        cabinetCollider = GetComponent<BoxCollider2D>();

        // 📢 ดึง AudioSource Component
        cabinetAudioSource = GetComponent<AudioSource>();
        if (cabinetAudioSource == null)
        {
            Debug.LogWarning("Cabinet is missing an AudioSource component! Sound won't play.");
        }

        hidePoint = transform.Find("HidePoint");
        if (hidePoint == null)
        {
            Debug.LogError("HideInCabinet requires a child GameObject named 'HidePoint'!");
        } }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHide playerHide = other.GetComponent<PlayerHide>();
            if (playerHide != null)
            {
                // 📢 ส่งทั้ง HidePoint และ AudioSource ไปให้ PlayerHide
                playerHide.SetCabinetInteraction(hidePoint, cabinetAudioSource);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHide playerHide = other.GetComponent<PlayerHide>();
            if (playerHide != null)
            {
                // 📢 ล้างค่าการโต้ตอบเมื่อออกจากตู้
                playerHide.ClearCabinetInteraction();
            }
        } }}