using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerDead : UIBase
{
    public Player player;
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
        player.Spawn(Vector3.zero, Quaternion.identity);
    }
}
