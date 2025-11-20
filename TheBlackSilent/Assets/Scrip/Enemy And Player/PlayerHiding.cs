using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHidden = false;
    private bool canHide = false;
    private Transform hideSpot;
    private SpriteRenderer spriteRenderer;

    // ตัวแปรสำหรับ AudioSource ที่รับมาจากตู้
    private AudioSource currentCabinetAudioSource;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // กด F เพื่อซ่อน / ออกจากที่ซ่อน
        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            if (!isHidden)
            {
                Debug.Log("Hiding");
                Hide();
            } else
                Unhide();
        }
    }

    void Hide()
    {
        if (isHidden) return;

        // 📢 1. เล่นเสียงเปิดตู้
        if (currentCabinetAudioSource != null)
        {
            // สั่งเล่นเสียงที่ถูกกำหนดไว้ใน AudioSource Component ของตู้
            currentCabinetAudioSource.Play();
        }

        isHidden = true;
        spriteRenderer.enabled = false; // ซ่อน sprite
        if (hideSpot != null)
            transform.position = hideSpot.position; // ย้ายเข้าไปในตู้

        // บอกศัตรูทั้งหมดว่าไม่เห็น player
        var enemies = FindObjectsByType<Enemy>(sortMode: FindObjectsSortMode.None);
        foreach (var e in enemies)
            e.SetPlayerVisible(false);

        Debug.Log("Player is hiding!");
    }

    void Unhide()
    {
        if (!isHidden) return;

        // 📢 2. เล่นเสียงปิดตู้/ออกจากตู้
        if (currentCabinetAudioSource != null)
        {
            // ใช้เสียงเดิมก็ได้ หรือถ้าต้องการเสียงปิดตู้ต่างหาก
            // สามารถเปลี่ยน clip ก่อนเรียก Play() ได้ (แต่ต้องมี clip แยกในตู้)
            currentCabinetAudioSource.Play();
        }

        isHidden = false;
        spriteRenderer.enabled = true; // แสดง sprite อีกครั้ง

        // บอกศัตรูทั้งหมดว่าเห็น player อีกครั้ง
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var e in enemies)
            e.SetPlayerVisible(true);

        Debug.Log("Player left the hiding spot!");
    }

    // ... ส่วนที่เหลือของโค้ด (SetHidePoint, ClearHidePoint, OnTrigger...)

    // Methods เพื่อให้สอดคล้องกับ HideInCabinet
    // หมายเหตุ: เนื่องจากสคริปต์ HideInCabinet เดิมเรียก SetHidePoint 
    // และคุณได้เพิ่ม SetCabinetInteraction เข้ามา
    // คุณอาจต้องรวม logic ให้ SetHidePoint เรียก SetCabinetInteraction ด้วย 
    // หรือให้ HideInCabinet เรียก SetCabinetInteraction อย่างเดียว (ตามที่ผมแนะนำก่อนหน้านี้)

    // ... 

    // ฟังก์ชันใหม่ที่ใช้รับค่า AudioSource
    public void SetCabinetInteraction(Transform hidePoint, AudioSource cabinetAudioSource)
    {
        hideSpot = hidePoint; // ใช้ hideSpot เดิม
        canHide = true;
        currentCabinetAudioSource = cabinetAudioSource; // <--- เก็บค่า AudioSource
        Debug.Log("Press F to hide");

        // ถ้าคุณมีการใช้ SetHidePoint() จากที่อื่น คุณอาจจะต้องการให้โค้ดส่วนนี้มาแทนที่
        // SetHidePoint() เดิม
    }

    public void ClearCabinetInteraction()
    {
        canHide = false;
        if (isHidden) Unhide(); // ออกจากที่ซ่อนอัตโนมัติเมื่อเดินออก
        hideSpot = null;
        currentCabinetAudioSource = null; // <--- ล้างค่า AudioSource
    }}