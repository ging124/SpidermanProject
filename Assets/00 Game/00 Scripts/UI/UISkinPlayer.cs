using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UISkinPlayer : MonoBehaviour
{
    [SerializeField] private List<Skin> skins = new List<Skin>();
    [SerializeField] private GameObject uiSkinPrefab;

    private void Awake()
    {
        foreach (Skin skin in skins)
        {
            var uiItem = Instantiate(uiSkinPrefab, this.transform);
            uiItem.GetComponent<UIItemSlot>().SetData(skin);
        }
    }

}
