using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrungNamespace
{

    public class CameraShake : MonoBehaviour
    {
        private CinemachineVirtualCamera cinemachine;
        private float shakeIntensity = 1f;
        private float shakeTime = 0.2f;

        private float timer;
        private CinemachineBasicMultiChannelPerlin cinemachineBasicMulti;
        private bool cameraShake;

        private void Awake()
        {
            cinemachine = this.GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            StopShake();
        }

        private void Update()
        {
            if(cameraShake)
            {
                cameraShake = false;
                ShakeCamera();
            }

            if(timer > 0)
            {
                timer -= Time.deltaTime;

                if(timer <= 0)
                {
                    StopShake();
                    cameraShake = true;
                }
            }
        }


        public void ShakeCamera()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMulti =  cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMulti.m_AmplitudeGain = shakeIntensity;

            timer = shakeTime;
        }

        void StopShake()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMulti = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMulti.m_AmplitudeGain = 0;
            timer = 0;
        }

        public void StartShakeCamrea(float a, float b)
        {
            cameraShake = true;
        }

    }
}


