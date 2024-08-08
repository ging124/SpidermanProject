using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gadget : Item
{
    public float durationGadget;
    public Sprite skillIcon;
    public PlayerController playerController;

    public virtual void StartGadget(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}
