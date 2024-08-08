using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gadget/WebShooter")]
public class WebShooter : Gadget
{
    public ClipTransition webShooterAnim;
    public GameObject webBulletPrefab;

    public override void StartGadget(PlayerController playerController)
    {
        playerController.animancer.Play(webShooterAnim);
    }
}
