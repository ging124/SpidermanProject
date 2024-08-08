using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGadget : MonoBehaviour
{
    public Gadget currentGadget;

    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        InnitGadget();
    }

    private void InnitGadget()
    {
        /*var gadget = currentGadget.Spawn(playerController.transform.position + new Vector3(1, 2, 0), this.transform.rotation, null);
        gadget.GetComponent<GadgetController>().playerController = playerController;*/
    }
}
