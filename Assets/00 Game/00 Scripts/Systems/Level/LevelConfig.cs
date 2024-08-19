using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Level/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public List<int> levelConfig = new List<int>();
}
