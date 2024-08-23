using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : ItemWorld
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private ClipTransition rotationAnim;
    [SerializeField] private ClipTransition openAnim;

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

    public void OpenChess()
    {
        animancer.Play(rotationAnim).Events.OnEnd = () =>
        {
            animancer.Play(openAnim).Events.OnEnd = () =>
            {
                itemData.Despawn(this.gameObject);
                uiSelectButton.CloseUI();
            };
        };
    }
}
