using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashLight; // อ้างอืงกับ Light Component ของไฟฉาย
    public KeyCode toggleKey = KeyCode.F; //ปุ่มเปิดปิด

    private void Start()
    {
        if (flashLight == null) 
        {
            flashLight == GetComponentInChildren<Light>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (flashLight != null)
            {
                flashLight.enabled = !flashLight.enabled;
            }
        }
    }
}
