using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIItemPlayer : MonoBehaviour
{
    [SerializeField] private List<Item> item = new List<Item>();
    [SerializeField] private GameObject uiItemPrefab;

    private void Awake()
    {
        foreach (Item skin in item)
        {
            var uiItem = Instantiate(uiItemPrefab, this.transform);
            uiItem.GetComponent<UIItemSlot>().SetData(skin);
        }
    }

}
