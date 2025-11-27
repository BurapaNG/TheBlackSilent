using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorSceneTeleport : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad; // The target scene name
    public string destinationDoorName; // The name of the door to spawn at in next scene

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.F;

    [Header("Audio Settings")]
    public AudioSource doorAudioSource;
    public float audioDuration = 0.5f;

    private bool playerIsOverlapping;

    void Update()
    {
        if (playerIsOverlapping && Input.GetKeyDown(interactKey))
        {
            // แทนที่จะเปลี่ยนฉากทันที เราจะเรียก Coroutine เพื่อเล่นเสียงก่อน
            StartCoroutine(LoadSceneAfterAudio());
        }
    }

    // Coroutine ที่ใช้ในการหน่วงเวลา
    IEnumerator LoadSceneAfterAudio()
    {
        // 1. เล่นเสียง
        if (doorAudioSource != null)
        {
            doorAudioSource.Play();
        }

        // 2. รอตามระยะเวลาของเสียงที่กำหนด
        // ใช้เวลาหน่วงเพื่อให้เสียงเปิดประตูเล่นได้ยินก่อนเปลี่ยนฉาก
        yield return new WaitForSeconds(audioDuration);

        // 3. บันทึกข้อมูลและเปลี่ยนฉาก
        PlayerPrefs.SetString("SpawnDoor", destinationDoorName);
        SceneManager.LoadScene(sceneToLoad);
    }void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsOverlapping = false;
    }
}
