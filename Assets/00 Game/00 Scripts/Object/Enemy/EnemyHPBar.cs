using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Image hpEffectImage;

    public void EnemyHPChange(float currentHP, float maxHP)
    {
        float fillAmount = (float)currentHP / maxHP;
        hpImage.fillAmount = fillAmount;
        hpEffectImage.DOFillAmount(fillAmount, 0.5f);
    }
}
