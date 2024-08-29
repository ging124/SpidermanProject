using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeComponent : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachine;
    private float shakerTimer;
    private float shakerTimerTotal;
    private float startingIntensity;

    private void Awake()
    {
        cinemachine = this.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasic = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasic.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - shakerTimer / shakerTimerTotal);
        }
    }


    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMulti = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMulti.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakerTimer = time;
        shakerTimer = time;
    }

}

