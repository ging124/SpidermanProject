using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerGadget : MonoBehaviour
{
    public Gadget currentGadget;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameEvent<Item> _innitGadget;

    [SerializeField] private GameEventListener<Item> _changeGadget;

    public void OnEnable()
    {
        _changeGadget.Register();
    }

    public void OnDisable()
    {
        _changeGadget.Unregister();
    }


    private void Start()
    {
        InnitGadget();
    }

    private void InnitGadget()
    {
        _innitGadget.Raise(currentGadget);
    }

    public void ChangeGadget(Item item)
    {
        currentGadget = (Gadget)item;
    }
}
