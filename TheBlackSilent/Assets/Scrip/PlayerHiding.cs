using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public bool isHidden = false;
    private bool canHide = false;
    private Transform hideSpot;
    private SpriteRenderer spriteRenderer;

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
            }

            else
                Unhide();
        }
    }

    void Hide()
    {
        if (isHidden) return;

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

        isHidden = false;
        spriteRenderer.enabled = true; // แสดง sprite อีกครั้ง

        // บอกศัตรูทั้งหมดว่าเห็น player อีกครั้ง
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var e in enemies)
            e.SetPlayerVisible(true);

        Debug.Log("Player left the hiding spot!");
    }

    // Methods เพื่อให้สอดคล้องกับ HideInCabinet (SetHidePoint / ClearHidePoint)
    public void SetHidePoint(Transform point)
    {
        hideSpot = point;
        canHide = true;
        Debug.Log("Press F to hide");
    }

    public void ClearHidePoint()
    {
        canHide = false;
        if (isHidden) Unhide(); // ออกจากที่ซ่อนอัตโนมัติเมื่อเดินออก
        hideSpot = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // เก็บพฤติกรรมเดิมไว้ (ถ้ามี HideSpot แบบง่าย)
        if (other.CompareTag("locked"))
        {
            canHide = true;
            hideSpot = other.transform;
            Debug.Log("Press F to hide");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("locked"))
        {
            canHide = false;
            if (isHidden) Unhide(); // ออกจากที่ซ่อนอัตโนมัติเมื่อเดินออก
        }
    }
}
