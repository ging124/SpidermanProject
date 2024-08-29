using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Manager/Input")]
public class InputSO : ScriptableObject
{
    public Vector2 move;
    public Vector2 look;
    public bool isLooking;

    public bool buttonJump;
    public bool buttonRoll;
    public bool buttonAttack;
    public bool buttonZip;
    public bool buttonDodge;
    public bool buttonGadget;
    public bool buttonUltimate;

    public bool buttonScan;

    public bool disableInput;

}
