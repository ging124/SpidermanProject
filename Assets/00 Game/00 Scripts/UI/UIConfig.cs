using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : UIBase
{
    public Transform contener;

    public List<ConfigLine> configLines = new List<ConfigLine>();
    
    protected override void Awake()
    {
        base.Awake();

        foreach(Transform t in contener)
        {
            Debug.Log(t.name);
            if (t.GetComponent<ConfigLine>() == null) continue;

            configLines.Add(t.GetComponent<ConfigLine>());
        }
    }

    public void SaveConfig()
    {
        foreach(ConfigLine line in configLines)
        {
            line.SaveData();
        }
    }
}
