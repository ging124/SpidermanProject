using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hp : MonoBehaviour
{
    [SerializeField] private Image hpImage;

    public void PlayerHpChange(float currentHp , float maxHp)
    {
        hpImage.fillAmount = currentHp / maxHp;
    }
}
