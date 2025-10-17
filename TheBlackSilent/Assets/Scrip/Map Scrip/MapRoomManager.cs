using UnityEngine;
using UnityEngine.SceneManagement;

public class MapRoomManager: MonoBehaviour
{
    public static MapRoomManager instance;

    private MapContainerData[] instances;
    private MapContainerData[] _rooms;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _rooms = GetComponentsInChildren<MapContainerData>(true);
    }
    
    public void RevealRoom()
    {
        string newLoadedScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].RoomScene == newLoadedScene && !_rooms[i].HasBeenRvealed)
            {
                _rooms[i].gameObject.SetActive(true);
                _rooms[i].HasBeenRvealed = true;

                return;
            }
        }
    }
}
