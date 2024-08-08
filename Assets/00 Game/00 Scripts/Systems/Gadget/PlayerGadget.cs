using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGadget : MonoBehaviour
{
    public Gadget currentGadget;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameEvent<Item> _innitGadget;

    private void Start()
    {
        InnitGadget();
    }

    private void InnitGadget()
    {
        _innitGadget.Raise(currentGadget);
        /*var gadget = currentGadget.Spawn(_playerController.transform.position + new Vector3(1, 2, 0), this.transform.rotation, null);
        gadget.GetComponent<GadgetController>()._playerController = _playerController;*/
    }
}
