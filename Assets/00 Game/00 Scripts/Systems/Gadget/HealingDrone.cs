using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gadget/HealingDrone")]
public class HealingDrone : Gadget
{
    public float hpRegen = 20;

    public override void StartGadget(PlayerController playerController)
    {
        base.StartGadget(playerController);
        this.Spawn(playerController.transform.position, Quaternion.identity);
    }

}
