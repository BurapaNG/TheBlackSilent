using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    // Item Slot //
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selecteShader;
    public bool thisItemSelected;

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        isFull = true;

        quantityText.text = quantity.ToString();
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

    }

    private void OnLeftClick()
    {
        selecteShader.SetActive(true);
        thisItemSelected = true;
    }

    
    public void OnRightClick()
    {
        
    }
}
