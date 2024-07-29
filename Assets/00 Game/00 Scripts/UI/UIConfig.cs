using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfig : MonoBehaviour
{
    public Player player;
    public Transform contener;

    public List<ConfigLine> configLines = new List<ConfigLine>();
    
    private void Awake()
    {
        foreach(Transform t in contener)
        {
            if (t.GetComponent<ConfigLine>() == null) return;


            configLines.Add(t.GetComponent<ConfigLine>());
        }

    }
}
