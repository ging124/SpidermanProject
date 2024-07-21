using Animancer;
using EasyCharacterMovement;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public LayerMask wallLayer;
    public float detectionLength;
    public float zipRange;
    public float zipLength;
    public Transform swingPointOnCam;
    public RaycastHit zipPointDetection;
    public Vector3 zipPoint;


    public RaycastHit frontWallHit;

   [Header("----ReadOnly----")]
    public int currentHP;
    public int currentMP;

    public Vector3 movement;

    public int hitDamage;
    public bool onHit;
    public bool wallFront;


    [Header("----Component----")]
    public Player playerData;
    public PlayerController playerController;
    public AnimancerComponent animancer;
    public Character character;
    public Transform cam;
    public Rigidbody rb;

    [Header("----SO----")]
    public InputSO inputSO;

    public void SetBlackboard(int currentHP, int currentMP, Player playerData, PlayerController playerController, AnimancerComponent animancer, Character character, Transform cam, Rigidbody rb)
    {
        this.currentHP = currentHP;
        this.currentMP = currentMP;
        this.playerData = playerData;
        this.playerController = playerController;
        this.animancer = animancer;
        this.character = character;
        this.cam = cam;
        this.rb = rb;
    }

    public void SetPlayerHP()
    {
        playerController.currentHP = currentHP;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(zipPointDetection.point, zipRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(zipPoint, zipRange);
    }
}
