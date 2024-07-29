using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
}
