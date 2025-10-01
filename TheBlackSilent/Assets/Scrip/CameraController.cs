using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform player;

    private void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y + yOffset,-10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
    }
}
