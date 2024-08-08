using UnityEngine;
using UnityEngine.Events;

public class HealingDroneController : GadgetController
{
    [SerializeField] private UnityEvent<float, float> _playerHeal;

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

        if (playerController.currentHP + hpRegen >= playerController.playerData.maxHP.Value)
        {
            playerController.currentHP = playerController.playerData.maxHP.Value;
        }
        else
        {
            playerController.currentHP += hpRegen;
        }

        _playerHeal.Invoke(playerController.currentHP, playerController.playerData.maxHP.Value);
    }
}
