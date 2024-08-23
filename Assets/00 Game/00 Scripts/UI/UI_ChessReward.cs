using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ChessReward : UIBase
{
    [SerializeField] private Chest chest;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text expText;

    private void OnEnable()
    {
        chest.GetReward();
        moneyText.text = chest.moneyCollectible.ToString();
        expText.text = chest.xpCollectible.ToString();
    }

    public override void OpenUI()
    {
        base.OpenUI();
        Invoke(nameof(CloseUI), 2);
    }

}
