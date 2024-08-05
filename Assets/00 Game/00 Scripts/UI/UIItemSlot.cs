using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private Item _itemData;
    [SerializeField] private Image _image;

    [SerializeField] private GameEvent<Item> changeSkin;


    private void Start()
    {
        _image.sprite = _itemData.image;
    }

    public void SetData(Item item)
    {
        _itemData = item; 
    }

    public void ChangeSkin()
    {
        Debug.Log("ChangeSkin");
        changeSkin?.Raise(_itemData);
    }
}
