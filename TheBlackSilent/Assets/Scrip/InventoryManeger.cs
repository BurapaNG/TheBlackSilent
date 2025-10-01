using UnityEngine;

public class InventoryManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryMenu;
    private bool isMenuOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {

        isMenuOpen = !isMenuOpen;


        if (isMenuOpen)
        {

            Time.timeScale = 0f;
            if (inventoryMenu != null) inventoryMenu.SetActive(true);
        }
        else
        {

            Time.timeScale = 1f;
            if (inventoryMenu != null) inventoryMenu.SetActive(false);
        }
    }


    public void AddItem(string itemName, int amount, Sprite itemSprite)
    {
        Debug.Log("Item Name: " + itemName + ", Quantity = " + amount + ", Item Sprite = " + itemSprite);
    }
}
