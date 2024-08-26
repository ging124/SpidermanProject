using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private InventoryItem _itemData;
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;



    private void Start()
    {
        _image.sprite = _itemData.image;
        _background.sprite = _itemData.background;
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
