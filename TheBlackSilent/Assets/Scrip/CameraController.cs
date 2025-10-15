using UnityEngine;

public class CameraController : MonoBehaviour 
{
   
    public float yOffset = 1f;
    public Transform player;

    private void LateUpdate()
    {
       
        Vector3 newPos = new Vector3(player.position.x, player.position.y + yOffset, -10f);

        
        transform.position = newPos;
    }
    
}
