using Animancer;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.UI;

public class Blackboard : MonoBehaviour
{
    public LayerMask wallLayer;
    public Transform swingPointOnCam;
    public float detectionLength;
    public RaycastHit frontWallHit;

    public float zipDetectionRange;
    public float zipDetectionLength;
    public float zipLength => (zipPoint - playerController.transform.position).magnitude;
    public float maxZipLength = 50;
    public RaycastHit zipPointDetection;
    public Vector3 zipPoint;
    public Image zipIconImage;

    [Header("----ReadOnly----")]
    public int currentHP;
    public int currentMP;

    public Vector3 movement;

    public int hitDamage;
    public bool onHit;
    public bool onSwim;
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
        Gizmos.DrawSphere(zipPointDetection.point, zipDetectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(zipPoint, zipDetectionRange);
    }
}
