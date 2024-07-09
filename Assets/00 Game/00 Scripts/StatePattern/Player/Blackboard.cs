using System.Collections;
using System.Collections.Generic;
using Animancer;
using JetBrains.Annotations;
using UnityEngine;

public class Blackboard : MonoBehaviour
{

    [Header("----ReadOnly----")]
    public int currentHP;
    public int currentMP;

    public Vector3 velocity;
    public Vector3 movement;
    public Vector3 slopeVelocity;
    public float groundedCheckDis;
    public bool onGround;
    public bool onSlope;
    public bool onHit;
    public int hitDamage;

    [Header("----Component----")]
    public Player playerData;
    public PlayerController playerController;
    public AnimancerComponent animancer;
    public CharacterController charController;
    public Transform cam;

    [Header("----SO----")]
    public InputSO inputSO;

    public void SetBlackboard(int currentHP, int currentMP, Player playerData, PlayerController playerController, AnimancerComponent animancer, CharacterController charController, Transform cam)
    {
        this.currentHP = currentHP;
        this.currentMP = currentMP;
        this.playerData = playerData;
        this.playerController = playerController;
        this.animancer = animancer;
        this.charController = charController;
        this.cam = cam;
    }

    public void SetPlayerHP()
    {
        playerController.currentHP = currentHP;
    }
}
