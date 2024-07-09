using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Manager/Input")]
public class InputSO : ScriptableObject
{
    public Vector2 move;
    public Vector2 look;

    public bool buttonJump;
    public bool buttonRoll;
    public bool buttonRun;
    public bool buttonAttack;
    public bool buttonInventory;
    public bool buttonHolding;

    public bool disableInput;

    public void DisableInput(bool disable)
    {
        disableInput = disable;
    }
}
