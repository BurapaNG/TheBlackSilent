using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwapManager : MonoBehaviour
{
    private void Onable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        MapRoomManager.instance.RevealRoom();
    }
    
}
