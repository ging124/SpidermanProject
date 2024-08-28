using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetModelUI : MonoBehaviour
{
    [SerializeField] private Gadget _currentGadget;
    [SerializeField] private GameObject _currentGadgetGameObject;

    [Header("----GameEvent----")]
    [SerializeField] private GameEventListener<Item> _changeGadget;

    private void Start()
    {
        InnitSkin();
    }

    private void OnEnable()
    {
        _changeGadget.Register();
    }

    private void OnDisable()
    {
        _changeGadget.Unregister();
    }

    void InnitSkin()
    {
        if (_currentGadgetGameObject.GetComponent<GadgetController>().itemData != _currentGadget)
        {
            _currentGadgetGameObject = _currentGadget.Spawn(this.transform.position, this.transform.rotation, this.transform);
        }

    }

    public void ChangeSkin(Item item)
    {
        if (item == _currentGadget) return;

        var skin = this.GetComponentInChildren<GadgetController>();
        _currentGadget.Despawn(skin.gameObject);
        _currentGadget = (Gadget)item;

        InnitSkin();
    }
}
