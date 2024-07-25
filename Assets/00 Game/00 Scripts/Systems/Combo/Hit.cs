using Animancer;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Attack/Hit")]
public class Hit : ScriptableObject
{
    public float timeNextAttack;
    public float timeEndAttack;
    public HitType hitType;
    public ClipTransition hitAnim;
}

public enum HitType
{
    LeftHand,
    RightHand,
    LeftLeg,
    RightLeg,
}

