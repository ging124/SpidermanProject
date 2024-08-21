using Animancer;
using EasyCharacterMovement;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlackboard : Blackboard
{
    [Header("----Component----")]
    public PlayerController playerController;
    public PlayerModel playerModel;
    public PlayerGadget playerGadget;
    public Character character;

    [Header("----SO----")]
    public InputSO inputSO;

    public void OnEnable()
    {
        inputSO.disableInput = false;
    }
}
