using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageFloat : MonoBehaviour
{
    public TMP_Text damageText;
    [SerializeField] float duration;
    [SerializeField] Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Start()
    {
        damageText.transform.DOMove(damageText.transform.position + new Vector3(0, 3, 0), duration);
        damageText.transform.DOScale(new Vector3(0, 0, 0), duration + 1);
        damageText.DOFade(0, duration + 1);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
