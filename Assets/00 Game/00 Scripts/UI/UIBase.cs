using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        CloseUI();
    }

    public virtual void OpenUI()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
}
