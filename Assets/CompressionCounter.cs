using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CompressionCounter : MonoBehaviour
{

    [Header("Session")]
    public CPRSession sessionManager; 

    [Header("Compression Count")]
    public int compressionCount = 0;

    [Header("UI")]
    public TextMeshProUGUI compressionText;
    public Image speedBarFill;

    [Header("Rate UI")]
    public TextMeshProUGUI compressionRate;


    [Header("Speed Settings")]
    public float targetCPM = 100f;
    public int samplesForAverage = 6;
    public float smoothingSpeed = 5f;   // Higher = smoother

    private List<float> compressionIntervals = new List<float>();
    private float lastCompressionTime = -1f;

    private float currentDisplayedCPM = 0f;

    void Start()
    {
        UpdateText();
        UpdateSpeedBar(0f);
    }

    void Update()
    {
        UpdateLiveSpeed();
    }

    public void AddCompression()
{
    if (sessionManager != null && !sessionManager.IsSessionActive())
        return;

    compressionCount++;
    UpdateText();

    float now = Time.time;

    if (lastCompressionTime > 0f)
    {
        float interval = now - lastCompressionTime;

        compressionIntervals.Add(interval);

        if (compressionIntervals.Count > samplesForAverage)
            compressionIntervals.RemoveAt(0);
    }

    lastCompressionTime = now;
}

public void ResetSessionData()
{
    compressionCount = 0;
    compressionIntervals.Clear();
    lastCompressionTime = -1f;
    currentDisplayedCPM = 0f;

    UpdateText();
    UpdateSpeedBar(0f);

    if (compressionRate != null)
        compressionRate.text = "Compression Rate: 0 CPM";
}



void UpdateLiveSpeed()
{
    if (compressionIntervals.Count == 0)
    {
        SmoothSpeed(0f);

        if (compressionRate != null)
            compressionRate.text = "Compression Rate: 0 CPM";

        return;
    }

    // Calculate average interval
    float avgInterval = 0f;
    foreach (float t in compressionIntervals)
        avgInterval += t;

    avgInterval /= compressionIntervals.Count;

    // Convert to CPM
    float rawCPM = 60f / avgInterval;

    // Clamp to realistic CPR range
    rawCPM = Mathf.Clamp(rawCPM, 0f, 160f);

    // Update UI TEXT (live number)
    if (compressionRate != null)
        compressionRate.text = "Compression Rate: " + Mathf.RoundToInt(rawCPM) + " CPM";

    SmoothSpeed(rawCPM);
}


    void SmoothSpeed(float targetCPMValue)
    {
        // Smoothly interpolate speed
        currentDisplayedCPM = Mathf.Lerp(
            currentDisplayedCPM,
            targetCPMValue,
            Time.deltaTime * smoothingSpeed
        );

        float normalized = Mathf.Clamp01(currentDisplayedCPM / targetCPM);
        UpdateSpeedBar(normalized);
    }

    void UpdateText()
    {
        if (compressionText != null)
            compressionText.text = $"Compressions: {compressionCount}";
    }

    void UpdateSpeedBar(float value)
    {
        if (speedBarFill != null)
            speedBarFill.fillAmount = value;
    }
}
