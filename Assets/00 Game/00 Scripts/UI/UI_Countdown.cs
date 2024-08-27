using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Countdown : MonoBehaviour
{
    public ICountdownable countdownable;
    public TMP_Text timerText;
    public Image cdBackground;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        countdownable = null;
    }

    private void Update()
    {
        if (countdownable != null)
        {
            float timmer = countdownable.TimmerCountdown();
            timerText.text = timmer.ToString();
            cdBackground.fillAmount = timmer / countdownable.TimmerCountdown();

            timmer -= Time.deltaTime;
        }
    }

    public void SetTimmer(ICountdownable countdownable)
    {
        this.countdownable = countdownable;
    }

}
