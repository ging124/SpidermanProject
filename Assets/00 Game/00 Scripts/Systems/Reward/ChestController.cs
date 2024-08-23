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

    public UI_SelectButton uiSelectButton;

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

    public void OpenChess()
    {
        animancer.Play(rotationAnim).Events.OnEnd = () =>
        {
            uiSelectButton.uiChestReward.OpenUI();
            uiSelectButton.CloseUI();
            GameObject gameObject = effect.Spawn(this.transform.position + Vector3.up * 0.5f, Quaternion.identity, this.transform);
            animancer.Play(openAnim).Events.OnEnd = () =>
            {
                itemData.Despawn(this.gameObject);
                effect.Despawn(gameObject);
            };
        };
    }
}
