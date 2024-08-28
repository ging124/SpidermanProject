using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Money : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;
    [SerializeField] Player player;

    // Update is called once per frame
    private void OnEnable()
    {
        moneyText.text = player.money.ToString();
    }
}
