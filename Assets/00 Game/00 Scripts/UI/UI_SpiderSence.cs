using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SpiderSence : MonoBehaviour
{
    private Transform _cam;
    [SerializeField] private GameObject spiderSence;

    private void Awake()
    {
        _cam = GameObject.FindWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(this.transform.position + _cam.forward);
    }
}
