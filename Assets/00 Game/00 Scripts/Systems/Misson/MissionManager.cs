using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Mission/MissionManager")]
public class MissionManager : ScriptableObject
{
    public List<BaseMission> missions = new List<BaseMission>();
}
