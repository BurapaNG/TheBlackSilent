using UnityEngine;

public class SpecificAudioController : MonoBehaviour
{
    // ตัวแปรสำหรับ AudioSource ที่ต้องการควบคุม
    public AudioSource targetAudioSource;

    // ปรับระดับเสียงของ AudioSource นี้
    public void SetSpecificVolume(float volume)
    {
        if (targetAudioSource != null)
        {
            // ค่า volume ที่กำหนดใน Inspector
            targetAudioSource.volume = volume;
        }
    }

    //ลดเสียงตู้ลง 
    public void LowerCabinetVolume()
    {
        SetSpecificVolume(0.15f);
    }
}