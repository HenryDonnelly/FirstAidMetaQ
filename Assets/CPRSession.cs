using UnityEngine;
using TMPro;
using System.Collections;

public class CPRSession : MonoBehaviour
{
    [Header("Session Settings")]
    public float delayBeforeStart = 3f;
    public float sessionLength = 60f;

    [Header("References")]
    public CompressionCounter compressionCounter;
    public TextMeshProUGUI sessionStatusText;

    private bool sessionActive = false;
    private bool sessionRunning = false;

    public void StartSession()
    {
        if (sessionRunning) return;

        compressionCounter.ResetSessionData();

        StartCoroutine(SessionRoutine());
    }

    IEnumerator SessionRoutine()
    {
        sessionRunning = true;
        sessionActive = false;

        // 3 second delay countdown
        float countdown = delayBeforeStart;

        while (countdown > 0)
        {
            if (sessionStatusText != null)
                sessionStatusText.text = "Starting in: " + Mathf.Ceil(countdown);

            countdown -= Time.deltaTime;
            yield return null;
        }

        // Start session
        sessionActive = true;

        if (sessionStatusText != null)
            sessionStatusText.text = "CPR IN PROGRESS";

        float timer = sessionLength;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if (sessionStatusText != null)
                sessionStatusText.text = "Time Left: " + Mathf.Ceil(timer);

            yield return null;
        }

        // End session
        sessionActive = false;
        sessionRunning = false;

        if (sessionStatusText != null)
            sessionStatusText.text = "SESSION COMPLETE";
    }

    public bool IsSessionActive()
    {
        return sessionActive;
    }
}
