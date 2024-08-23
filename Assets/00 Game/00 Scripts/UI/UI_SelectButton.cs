using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectButton : UIBase
{
    [SerializeField] private ChestController chestController;
    public UI_ChestReward uiChestReward;

    public virtual void OpenUI(ChestController chestController)
    {
        this.chestController = chestController;
        base.OpenUI();
    }

    public void OpenChess()
    {
        chestController.OpenChess();
    }

}
