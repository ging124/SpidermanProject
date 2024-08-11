using UnityEngine;
using UnityEngine.Events;

public class HealingDroneController : GadgetController
{

    private float _time;

    void Start()
    {
        playerController = ((Gadget)itemData).playerController;
    }

    private void OnEnable()
    {
        _time = 0;
    }

    protected override void Update()
    {
        base.Update();

        _time += Time.deltaTime;

        if (_time <= ((Gadget)itemData).durationGadget)
        {
            Healing();
        }
        else
        {
            itemData.Despawn(this.gameObject);
        }
    }


    void Healing()
    {
        float hpRegen = ((HealingDrone)itemData).hpRegen;

        if (playerController.playerData.currentHP + hpRegen >= playerController.playerData.maxHP.Value)
        {
            playerController.playerData.currentHP = playerController.playerData.maxHP.Value;
        }
        else
        {
            playerController.playerData.currentHP += hpRegen;
        }

        playerController.playerChangeHP.Invoke(playerController.playerData.currentHP, playerController.playerData.maxHP.Value);
    }
}
