using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : ItemWorld
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private ClipTransition rotationAnim;
    [SerializeField] private ClipTransition openAnim;
    [SerializeField] GameEffect effect;

    private GameObject effectGameObject;

    public UI_SelectButton uiSelectButton;
    public UI_ChestReward uiChestReward;

    private void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            uiSelectButton.OpenUI(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uiSelectButton.CloseUI();
        }
    }

    public void StartOpenChest(UI_ChestReward ui)
    {
        this.uiChestReward = ui;
        animancer.Play(rotationAnim).Events.OnEnd = OpenChest;
    }

    public void OpenChest()
    {
        uiChestReward.OpenUI();
        uiSelectButton.CloseUI();
        effectGameObject = effect.Spawn(this.transform.position + Vector3.up * 0.5f, Quaternion.identity, this.transform);
        animancer.Play(openAnim).Events.OnEnd = CloseChest;
    }

    public void CloseChest()
    {
        itemData.Despawn(this.gameObject);
        ((Chest)itemData).despawnEffect.Spawn(this.transform.position, Quaternion.identity);
        effect.Despawn(effectGameObject);
    }
}
