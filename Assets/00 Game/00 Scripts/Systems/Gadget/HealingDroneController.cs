using UnityEngine;
using UnityEngine.Events;

public class HealingDroneController : GadgetController
{
    [field:SerializeField] public float time { get; private set; }

    void Start()
    {
        playerController = ((Gadget)itemData).playerController;
    }

    private void OnEnable()
    {
        time = 0;
    }

    protected override void Update()
    {
        base.Update();

        time += Time.deltaTime;

        if(playerController != null)
        {
            if (time <= ((Gadget)itemData).durationGadget)
            {
                Healing();
            }
            else
            {
                ((Gadget)itemData).gadgetFinished.Raise();
                itemData.Despawn(this.gameObject);
            }
        }
    }

    void Healing()
    {

        float hpRegen = ((HealingDrone)itemData).hpRegen;

        if (playerController.currentHP + hpRegen >= playerController.playerData.maxHP)
        {
            playerController.currentHP = playerController.playerData.maxHP;
        }
        else
        {
            playerController.currentHP += hpRegen;
        }

        playerController.playerChangeHP.Invoke(playerController.currentHP, playerController.playerData.maxHP);
    }
}
