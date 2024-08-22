using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MissionFailed : UIBase
{
    public Player player;
    public PlayerController playerController;
    public GameEventListener playerDead;

    protected override void Awake()
    {
        base.CloseUI();
        playerDead.Register();
    }

    public void OnDestroy()
    {
        playerDead.Unregister();
    }

    public override void CloseUI()
    {
        base.CloseUI();
        SpawnPlayer();

    }

    public void SpawnPlayer()
    {
        if (!playerController.gameObject.activeInHierarchy)
        {
            player.Spawn(Vector3.zero, Quaternion.identity);
        }
    }
}
