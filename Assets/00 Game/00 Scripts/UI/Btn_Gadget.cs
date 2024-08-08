using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Gadget : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private GameEventListener<Item> _changeGadget;
    [SerializeField] private GameEventListener<Item> _innitGadget;

    private void OnEnable()
    {
        _changeGadget.Register();
        _innitGadget.Register();
    }

    private void OnDisable()
    {
        _changeGadget.Unregister();
        _innitGadget.Unregister();
    }

    public void ChangeButtonImage(Item item)
    {
        _image.sprite = ((Gadget)item).skillIcon;
    }

}
