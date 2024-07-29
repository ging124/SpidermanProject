using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfigLine : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_InputField inputField;

    public FloatVariables data;

    private void Awake()
    {
        SetConfigLine(data.name, data.Value);
    }

    public void SetConfigLine(string attribiute, float value)
    {
        nameText.text = attribiute;
        inputField.text = value.ToString();
    }

    public void SaveData()
    {
        float.TryParse(inputField.text, out data.Value);
    }
}
