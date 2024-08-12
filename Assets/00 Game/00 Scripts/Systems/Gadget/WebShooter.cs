using Animancer;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Gadget/WebShooter")]
public class WebShooter : Gadget
{
    public ClipTransition webShooterAnim;
    public WebBullet webBullet;
    public float stunLockDuration;

    private PlayerModel playerModel;

    public override void StartGadget(PlayerController playerController)
    {
        base.StartGadget(playerController);
        playerController.animancer.Play(webShooterAnim, 0.25f, FadeMode.FromStart);
        playerModel = playerController.GetComponent<PlayerBlackboard>().playerModel;

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
        if (playerController.enemyTarget != null)
        {
            playerController.transform.DOLookAt(playerController.enemyTarget.transform.position, 0.2f, AxisConstraint.Y);
            var bulletController = bullet.GetComponent<WebBulletController>();
            bulletController.target = playerController.enemyTarget.GetComponent<EnemyController>();
            bulletController.stunLockDuration = stunLockDuration;
        }
    }
}
