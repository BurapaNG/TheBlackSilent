using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActived)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActived = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActived)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActived = true;
        }
    }
}
