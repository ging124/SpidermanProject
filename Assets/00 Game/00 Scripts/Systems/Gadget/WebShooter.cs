using Animancer;
using DG.Tweening;
using System.Resources;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gadget/WebShooter")]
public class WebShooter : Gadget
{
    public ClipTransition webShooterAnim;
    public WebBullet webBullet;
    public float stunLockDuration;

    private PlayerModel playerModel;

    public override GameObject StartGadget(PlayerController playerController)
    {
        base.StartGadget(playerController);
        playerController.animancer.Play(webShooterAnim, 0.25f, FadeMode.FromStart).Events.OnEnd = () =>
        {
            gadgetFinished.Raise();
        };
        playerModel = playerController.GetComponent<PlayerBlackboard>().playerModel;
        return null;
    }

    public void ShootingLeftHand()
    {
        Shoot(playerModel.leftHand.position);
    }

    public void ShootingRightHand()
    {
        Shoot(playerModel.rightHand.position);
    }

    private void Shoot(Vector3 pos)
    {
        var bullet = webBullet.Spawn(pos, playerController.transform.rotation, null);
        if (playerController.target != null)
        {
            playerController.transform.DOLookAt(playerController.target.transform.position, 0.2f, AxisConstraint.Y);
            var bulletController = bullet.GetComponent<WebBulletController>();
            bulletController.target = playerController.target.GetComponent<EnemyController>();
            bulletController.stunLockDuration = stunLockDuration;
        }
    }

}
