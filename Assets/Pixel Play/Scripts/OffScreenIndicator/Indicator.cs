﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Assign this script to the indicator prefabs.
/// </summary>
public class Indicator : MonoBehaviour
{
    [SerializeField] private IndicatorType indicatorType;
    [SerializeField] private Image indicatorImage;
    [SerializeField] private Text distanceText;

    public Image OffScreenImage;
    /// <summary>
    /// Gets if the game object is active in hierarchy.
    /// </summary>
    public bool Active
    {
        get
        {
            return transform.gameObject.activeInHierarchy;
        }
    }

    /// <summary>
    /// Gets the indicator type
    /// </summary>
    public IndicatorType Type
    {
        get
        {
            return indicatorType;
        }
    }

    void Awake()
    {
        indicatorImage = this.GetComponent<Image>();
        distanceText = this.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Sets the image color for the indicator.
    /// </summary>
    /// <param name="color"></param>
    public void SetImageColor(Color color)
    {
        indicatorImage.color = color;
    }
    public void SetImageSprite(Sprite sprite)
    {
        indicatorImage.sprite = sprite;
    }
    public void SetImageOffScreen(Sprite sprite, Color color)
    {
        OffScreenImage.sprite = sprite;
        OffScreenImage.color = color;
    }
    /// <summary>
    /// Sets the distance text for the indicator.
    /// </summary>
    /// <param name="value"></param>
    public void SetDistanceText(float value)
    {
        distanceText.text = value >= 0 ? Mathf.Floor(value) + " m" : "";
    }

    /// <summary>
    /// Sets the distance text rotation of the indicator.
    /// </summary>
    /// <param name="rotation"></param>
    public void SetTextRotation(Quaternion rotation)
    {
        distanceText.rectTransform.rotation = rotation;
    }
    public void SetImageRotation(Quaternion rotation)
    {
        OffScreenImage.rectTransform.rotation = rotation;
    }

    /// <summary>
    /// Sets the indicator as active or inactive.
    /// </summary>
    /// <param name="value"></param>
    public void Activate(bool value)
    {
        transform.gameObject.SetActive(value);
    }
}

public enum IndicatorType
{
    BOX,
    ARROW
}