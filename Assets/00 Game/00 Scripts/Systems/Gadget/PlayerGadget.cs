using System.Collections;
using UnityEngine;

public class PlayerGadget : MonoBehaviour
{
    public Gadget currentGadget;
    [field:SerializeField] public bool onUseGadget { get; private set; }

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameEvent<Item> _innitGadget;

    [SerializeField] private GameEventListener<Item> _changeGadget;
    [SerializeField] private GameEventListener _gadgetFinished;


    public void OnEnable()
    {
        _changeGadget.Register();
        _gadgetFinished.Register();
    }

    public void OnDisable()
    {
        _changeGadget.Unregister();
        _gadgetFinished.Unregister();
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
        onUseGadget = false;
    }

    public void UseGadget()
    {
        currentGadget.StartGadget(_playerController);
        onUseGadget = true;
    }

    public void GadgetFinished()
    {
        onUseGadget = false;
    }

}
