using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class UI_ComboText : MonoBehaviour
{
    [SerializeField] TMP_Text comboText;
    private float count = 0;
    private Tween tween;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        count = 0;
    }

    public void ShowCombo()
    {
        StopCoroutine(Deactive());
        this.gameObject.SetActive(true);
        tween.Kill();
        count++;
        this.transform.localScale = Vector3.zero;
        tween = this.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        comboText.text = $"Combo x{count}";
        StartCoroutine(Deactive());
    }

    private IEnumerator Deactive()
    {
        yield return new WaitForSeconds(10);
        this.gameObject.SetActive(false);
    }
}
