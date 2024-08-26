    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainScannerDEMO
{
    public class SensorObject : MonoBehaviour
    {
        [SerializeField] private SensorDetector _detector;

        private AudioSource _audioTrigger;
        [SerializeField] AudioClip _sensorExit;

        [SerializeField] protected Target target;

        private bool _detected;

        private float _timeToReset;
        private float _timerToReset;

        private void Start()
        {
            _audioTrigger = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            target.enabled = false;
        }

        private void Update()
        {
            if (_detected)
            {
                if (_timerToReset > _timeToReset)
                {
                    if (_detector.SensorOn) { return; }
                    _detected = false;
                    _timerToReset = 0f;
                    target.enabled = false;
                    _audioTrigger.PlayOneShot(_sensorExit);
                }

                _timerToReset += Time.deltaTime;
            }

            if (_detector.SensorOn)
            {
                if (Vector3.Distance(transform.position, _detector.Origin) < (_detector.Radius - 1f))
                {
                    if (_detected)
                    {
                        return;
                    }
                    Detected();
                }
            }
        }

        private void Detected()
        {
            Debug.Log("he");
            _detected = true;
            _timeToReset = 2 * _detector.Duration;
            _audioTrigger.Play();
            target.enabled = true;
        }
    }
}
