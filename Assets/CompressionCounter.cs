using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CompressionCounter : MonoBehaviour
{
    [Header("Compression Count")]
    public int compressionCount = 0;

    [Header("UI")]
    public TextMeshProUGUI compressionText;
    public Image speedBarFill;

    [Header("Speed Settings")]
    public float targetCPM = 100f;
    public int samplesForAverage = 5;

    private List<float> compressionIntervals = new List<float>();
    private float lastCompressionTime = -1f;

    void Start()
    {
        UpdateText();
        UpdateSpeedBar(0f);
    }

    void Update()
    {
        UpdateLiveSpeed();
    }

    public void AddCompression(bool isOn)
    {
        if (!isOn) return;
        AddCompression();
    }

    public void AddCompression()
    {
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

    void UpdateLiveSpeed()
    {
        if (lastCompressionTime <= 0f || compressionIntervals.Count == 0)
        {
            UpdateSpeedBar(0f);
            return;
        }

        float timeSinceLast = Time.time - lastCompressionTime;

        // If user stopped pressing, gradually drop bar
        if (timeSinceLast > 2f)
        {
            UpdateSpeedBar(0f);
            return;
        }

        float avgInterval = 0f;
        foreach (float t in compressionIntervals)
            avgInterval += t;

        avgInterval /= compressionIntervals.Count;

        float currentCPM = 60f / avgInterval;

        float normalized = Mathf.Clamp01(currentCPM / targetCPM);

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
