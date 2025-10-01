using UnityEngine;

public class HideInCabinet : MonoBehaviour 
{
    private Transform hidePoint;
    private BoxCollider2D cabinetCollider;

    private void Start()
    {
        cabinetCollider = GetComponent<BoxCollider2D>();

        hidePoint = transform.Find("HidePoint");
        if (hidePoint == null)
        {
            Debug.LogError("HideInCabinet requires a child GameObject named 'HidePoint'!");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetHidePoint(hidePoint);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ClearHidePoint();
            }
        }
    }   

}
