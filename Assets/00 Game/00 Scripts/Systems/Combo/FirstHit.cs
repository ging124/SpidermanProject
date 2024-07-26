using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Attack/FirstHit")]
public class FirstHit : ScriptableObject
{
    public Hit nearAttack;
    public Hit mediumAttack;
    public Hit farAttack;
}
