using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RewardSelected")]
public class RewardSelectedData : ScriptableObject
{
    public Color colorTextSelected;
    public Color colorTextNormal;
    public Sprite normalFrame;
    public Sprite focusFrame;
    public Sprite completeFrame;
}
