using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerSkin : UIBase
{
    [SerializeField] private RawImage playerModelImage;

    protected override void Awake()
    {
        OpenUI();
    }

    public override void OpenUI()
    {
        playerModelImage.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public override void CloseUI()
    {
        playerModelImage.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
