using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonScan : MonoBehaviour
{
    [SerializeField] GameObject hightlightEffect;

    [SerializeField] GameEventListener buttonScanOn;
    [SerializeField] GameEventListener buttonScanOff;

    private void Awake()
    {
        buttonScanOn.Register();
        buttonScanOff.Register();
    }

    private void OnDestroy()
    {
        buttonScanOn.Unregister();
        buttonScanOff.Unregister();
    }

    public void ButtonScanOn()
    {
        hightlightEffect.SetActive(true);
    }

    public void ButtonScanOff()
    {
        hightlightEffect.SetActive(false);
    }
}
