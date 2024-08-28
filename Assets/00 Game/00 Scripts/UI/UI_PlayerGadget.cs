using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerGadget : UIBase
{
    [SerializeField] private RawImage gadgetModelImage;

    public override void OpenUI()
    {
        gadgetModelImage.gameObject.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public override void CloseUI()
    {
        gadgetModelImage.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
