using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : ScriptableObject
{
    public int amount;
    public Sprite image;
    public RewardStatus status;
}

public enum RewardStatus
{
    None,
    Complete,
    Forcus
}

