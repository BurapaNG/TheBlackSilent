using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    private Transform currentHidePoint = null;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private SpriteRenderer rend;
    private bool canHide = false;
    private bool hiding = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (canHide && Input.GetKeyDown(KeyCode.F))
        {
            hiding = !hiding;
            if (hiding)
            {
                if (currentHidePoint != null)
                {
                    transform.position = currentHidePoint.position;
                }

                Physics2D.IgnoreLayerCollision(8, 9, true);
                rend.sortingOrder = 0;
            }


            else
            {
                Physics2D.IgnoreLayerCollision(8, 9, false);
                rend.sortingOrder = 2;

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
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
        else
        {
            body.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {
            canHide = true;
<<<<<<< Updated upstream
=======
            Transform hidePoint = other.transform.Find("HidePoint");
            if (hidePoint != null) { SetHidePoint(hidePoint); }


            if (hidePromptUI != null) hidePromptUI.SetActive(true);
>>>>>>> Stashed changes
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("locked"))
        {
<<<<<<< Updated upstream
=======

            ClearHidePoint();

            if (hidePromptUI != null) hidePromptUI.SetActive(false);

>>>>>>> Stashed changes
            canHide = false;
            rend.sortingOrder = 2;
        }
    }
}
