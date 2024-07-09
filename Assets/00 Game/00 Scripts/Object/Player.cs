using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RPGObject/Player")]
public class Player : RPGObject
{
    public float jumpHeight = 30;
    public float rotateSpeed = 20;
    public float gravityValue = 6;
    public float rollVelocity = 10;
    public float bufferCheckDis = 0.25f;
    
}
