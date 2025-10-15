using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour 
{
    private Vector3 positionBeforeHiding;
    private Transform currentHidePoint = null;

    [SerializeField] private float walkSpeed = 5f;  
    [SerializeField] private float runSpeed = 10f;
    
    [SerializeField] private GameObject hidePromptUI;
    private Rigidbody2D body;
    private SpriteRenderer rend;
    private SpriteRenderer[] allBodyRenderers;
    private Dictionary<SpriteRenderer, int> originalSortingOrders;
    private bool canHide = false;
    private bool hiding = false;

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
        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            hiding = !hiding;
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
            }


            else
            {

                ResetAllSortingOrder();

                transform.position = positionBeforeHiding;

                body.isKinematic = false;

                Physics2D.IgnoreLayerCollision(8, 9, false);
                

                if (hidePromptUI != null) hidePromptUI.SetActive(true);

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
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed = runSpeed;
            }


            body.velocity = new Vector2(horizontalInput * currentSpeed, body.velocity.y);
            FlipSprite(horizontalInput);
        }
        else
        {
            body.velocity = Vector2.zero;
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
            
                if (hiding)
                {
                    hiding = false;


                    transform.position = positionBeforeHiding;
                    body.isKinematic = false;
                    Physics2D.IgnoreLayerCollision(8, 9, false);
                    rend.sortingOrder = 2;
                }
            
        }
    }
}
