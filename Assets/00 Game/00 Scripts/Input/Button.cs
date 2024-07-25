using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool button;

    public void ButtonJumpDown()
    {
        button = true;
    }

    public void ButtonJumpUp()
    {
        button = false;
    }

    public void ButtonJump()
    {
        button = true;
        StartCoroutine(ButtonJumpFalse());
    }

    IEnumerator ButtonJumpFalse()
    {
        yield return 0;
        button = false;
    }
}
