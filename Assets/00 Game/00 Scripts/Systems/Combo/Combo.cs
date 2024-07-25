using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Attack/Combo"), Serializable]
public class Combo : ScriptableObject
{
    public List<Hit> hitList;
}
