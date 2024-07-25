using Animancer;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBlackboard : Blackboard
{
    [Header("----Component----")]
    public PlayerController playerController;
    public Character character;

    [Header("----SO----")]
    public InputSO inputSO;
}
