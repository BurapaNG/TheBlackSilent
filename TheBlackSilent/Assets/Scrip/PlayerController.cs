using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject hidePromptUI;
    private Rigidbody2D body;
    private SpriteRenderer rend;
    private bool canHide = false;
    private bool hiding = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        if (hidePromptUI != null)
        {
            hidePromptUI.SetActive(false);
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

                Physics2D.IgnoreLayerCollision(8, 9, true);
                rend.sortingOrder = 0;

                if (hidePromptUI != null) hidePromptUI.SetActive(false);
            }


            else
            {
                Physics2D.IgnoreLayerCollision(8, 9, false);
                rend.sortingOrder = 2;

                if (hidePromptUI != null) hidePromptUI.SetActive(true);

            }
        }

    }
    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (!hiding)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
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
            rend.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            rend.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {
            canHide = true;

            if (hidePromptUI != null) hidePromptUI.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {

            if (hidePromptUI != null) hidePromptUI.SetActive(false);

            canHide = false;
            rend.sortingOrder = 2;
        }
    }
}
