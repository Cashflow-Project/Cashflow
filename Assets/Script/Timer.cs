using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private bool Countdown = true;

    private float timerDuration = 3f * 60f;
    private float timer;

    [SerializeField]
    private TMP_Text firstMinutes;
    [SerializeField]
    private TMP_Text secondMinutes;
    [SerializeField]
    private TMP_Text Separator;
    [SerializeField]
    private TMP_Text firstSeconds;
    [SerializeField]
    private TMP_Text secondSeconds;

    private float flashTimer;
    private float flashDuration = 1f;
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if(Countdown && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else if (!Countdown && timer < timerDuration)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else
        {
            Flash();
        }
        
    }

    private void ResetTimer()
    {
        if (Countdown)
        {
            timer = timerDuration;
        }
        else
        {
            timer = 0;
        }
        SetTextDisplay(true);
    }

    private void UpdateTimerDisplay(float time)
    {
        float minites = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}",minites,seconds);
        firstMinutes.text = currentTime[0].ToString();
        secondMinutes.text = currentTime[1].ToString();
        firstSeconds.text = currentTime[2].ToString();
        secondSeconds.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if(Countdown && timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (!Countdown && timer != timerDuration)
        {
            timer = timerDuration;
            UpdateTimerDisplay(timer);
        }


        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }else if(flashTimer >= flashDuration/2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void SetTextDisplay(bool on)
    {
        firstMinutes.enabled = on;
        secondMinutes.enabled = on;
        Separator.enabled = on;
        firstSeconds.enabled = on;
        secondSeconds.enabled = on;
    }
}
