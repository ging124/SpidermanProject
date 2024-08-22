using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MissionShipping : UIBase
{
    public ShippingMission shippingMission;

    private float time;
    private float minute;
    private float second;
    public Color colorhotDot;
    public TMP_Text timeText;
    public Image hotDotProgress1;
    public Image hotDotProgress2;
    public Image hotDotProgress3;

    public GameEvent missionFailse;
    public GameEventListener<ShippingMission> shippingMissionUI;

    private void OnEnable()
    {
        time = shippingMission.timeRequired;
        hotDotProgress1.color = colorhotDot;
        hotDotProgress2.color = colorhotDot;
        hotDotProgress3.color = colorhotDot;
    }

    protected override void Awake()
    {
        base.CloseUI();
        shippingMissionUI.Register();
    }

    public void OnDestroy()
    {
        shippingMissionUI.Unregister();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        DisplayMission();

        if (time <= 0)
        {
            missionFailse.Raise();
        }
    }

    private void DisplayMission()
    {
        DisplayTime();

        switch (shippingMission.point)
        {
            case 2:
                hotDotProgress1.color = Color.white;
                break;
            case 3:
                hotDotProgress2.color = Color.white;
                break;
            case 4:
                hotDotProgress3.color = Color.white;
                break;
        }

    }

    void DisplayTime()
    {
        minute = Mathf.FloorToInt(time / 60);
        second = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("Time: {0:00}:{1:00}", minute, second);
    }

    public void ShippingMissionUI(ShippingMission shippingMission)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            this.shippingMission = shippingMission;
            base.OpenUI();
        }
        else
        {
            base.CloseUI();
        }
    }

}
