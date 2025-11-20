using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{   private Vector3 positionBeforeHiding;
    private Transform currentHidePoint = null;

    private bool isRunning = false;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip runSound;

    // ย้ายตัวแปร speed มาไว้ข้างล่าง Audio Settings เพื่อความเป็นระเบียบ
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private GameObject hidePromptUI;
    [SerializeField] private GameObject exitHidePromptUI;
    private Rigidbody2D body;
    private SpriteRenderer rend;
    private SpriteRenderer[] allBodyRenderers;
    private Dictionary<SpriteRenderer, int> originalSortingOrders;
    private bool canHide = false;
    private bool hiding = false;
    public Animator animator;

    // ฟังก์ชันนี้ถูกเรียกโดย Animation Event
    public void PlayFootstepSound()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // 📢 1. ตรวจสอบว่ากำลังซ่อนตัว หรือผู้เล่นหยุดกดปุ่มแล้วหรือไม่
        // ถ้าหยุดกดปุ่มหรือกำลังซ่อนตัว ให้หยุดเสียงทันทีและออกจากฟังก์ชัน
        if (hiding || Mathf.Abs(horizontalInput) < 0.01f)
        {
            if (footstepAudioSource != null && footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop();
            }
            return;
        }

        if (footstepAudioSource == null || (walkSound == null && runSound == null))
        {
            return;
        }

        // กำหนด clip ที่จะเล่นตามสถานะ isRunning
        AudioClip clipToPlay = isRunning ? runSound : walkSound;

        if (clipToPlay != null)
        {
            // เปลี่ยน clip ของ AudioSource แล้วเล่น
            footstepAudioSource.clip = clipToPlay;
            footstepAudioSource.pitch = Random.Range(0.9f, 1.1f); // เพิ่มความหลากหลายของเสียงเล็กน้อย
            footstepAudioSource.Play();
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        allBodyRenderers = GetComponentsInChildren<SpriteRenderer>();

        originalSortingOrders = new Dictionary<SpriteRenderer, int>();
        foreach (SpriteRenderer r in allBodyRenderers)
        {
            originalSortingOrders.Add(r, r.sortingOrder);
        }

        if (hidePromptUI != null)
        {
            hidePromptUI.SetActive(false);
        }
        if (exitHidePromptUI != null)
        {
            exitHidePromptUI.SetActive(false);
        }
    }

    private void SetAllSortingOrder(int offset)
    {
        foreach (SpriteRenderer r in allBodyRenderers)
        {

            r.sortingOrder = originalSortingOrders[r] + offset;
        }
    }

    private void ResetAllSortingOrder()
    {
        foreach (SpriteRenderer r in allBodyRenderers)
        {

            r.sortingOrder = originalSortingOrders[r];
        }
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        animator.SetBool("IsRunning", isRunning);

        // 📢 2. การตรวจสอบทันที: ถ้าหยุดเคลื่อนที่ ให้สั่งหยุดเสียงทันที
        if (Mathf.Abs(horizontalInput) < 0.01f && footstepAudioSource != null && footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Stop();
        }

        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            hiding = !hiding;

            animator.SetBool("IsHiding", hiding);

            if (hiding)
            {
                SetAllSortingOrder(-100);

                positionBeforeHiding = transform.position;
                if (currentHidePoint != null)
                {
                    transform.position = currentHidePoint.position;

                    body.isKinematic = true;
                }

                Physics2D.IgnoreLayerCollision(8, 9, true);


                if (hidePromptUI != null) hidePromptUI.SetActive(false);
                if (exitHidePromptUI != null) exitHidePromptUI.SetActive(true);
            }


            else
            {

                ResetAllSortingOrder();

                transform.position = positionBeforeHiding;

                body.isKinematic = false;

                Physics2D.IgnoreLayerCollision(8, 9, false);


                if (hidePromptUI != null) hidePromptUI.SetActive(true);
                if (exitHidePromptUI != null) exitHidePromptUI.SetActive(false);

            }
        }

    }

    public void SetHidePoint(Transform point)
    {
        currentHidePoint = point;
    }

    public void ClearHidePoint()
    {
        currentHidePoint = null;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (!hiding)
        {

            float currentSpeed = walkSpeed;
            isRunning = false;

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed = runSpeed;
                isRunning = true;
            }


            body.velocity = new Vector2(horizontalInput * currentSpeed, body.velocity.y);
            FlipSprite(horizontalInput);
        }
        else
        {
            body.velocity = Vector2.zero;
            isRunning = false;
            animator.SetBool("IsRunning", isRunning);
        }
    }

    private void FlipSprite(float horizontalInput)
    {

        if (horizontalInput > 0.01f)
        {

            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        else if (horizontalInput < -0.01f)
        {

            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {
            canHide = true;
            Transform hidePoint = other.transform.Find("HidePoint");
            if (hidePoint != null) { SetHidePoint(hidePoint); }


            if (hidePromptUI != null) hidePromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {

            ClearHidePoint();
            canHide = false;
            if (hidePromptUI != null) hidePromptUI.SetActive(false);
            if (exitHidePromptUI != null) exitHidePromptUI.SetActive(false);

            if (hiding)
            {
                hiding = false;


                transform.position = positionBeforeHiding;
                body.isKinematic = false;
                Physics2D.IgnoreLayerCollision(8, 9, false);
                rend.sortingOrder = 2;
            }

        }
    }}