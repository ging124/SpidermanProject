using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gadget : InventoryItem
{
    public float durationGadget;
    public Sprite skillIcon;
    public PlayerController playerController;
    public GameEvent gadgetFinished;


    public virtual GameObject StartGadget(PlayerController playerController)
    {
        this.playerController = playerController;
        return null;
    }
}
