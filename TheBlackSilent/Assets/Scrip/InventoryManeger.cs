using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;



    private InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        
=======
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManeger>();
>>>>>>> Stashed changes
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);
            if (leftOverItems == 0)
                Destroy(gameObject);
            else
                quantity = leftOverItems;
        }
<<<<<<< Updated upstream
        else if (Input.GetButtonDown("Inventory") && !menuActived)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActived = true;
        }
=======
>>>>>>> Stashed changes
    }
}
