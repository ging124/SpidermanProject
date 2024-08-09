using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private InventoryItem _itemData;
    [SerializeField] private Image _image;


    private void Start()
    {
        _image.sprite = _itemData.image;
    }

    public void SetData(Item item)
    {
        _itemData = (InventoryItem)item; 
    }

    public void ChangeItem()
    {
        _itemData.changeItem?.Raise(_itemData);
    }
}
