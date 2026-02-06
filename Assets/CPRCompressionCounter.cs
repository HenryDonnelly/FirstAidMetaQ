using UnityEngine;
using TMPro;

public class CPRCompressionCounter : MonoBehaviour
{
    [Header("Compression")]
    public int compressionCount = 0;
    public float cooldownSeconds = 0.5f;

    [Header("UI")]
    public TextMeshProUGUI compressionText;

    private float lastCompressionTime = -999f;

    void Start()
    {
        UpdateText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastCompressionTime < cooldownSeconds)
            return;

        if (!other.name.ToLower().Contains("hand"))
            return;

        compressionCount++;
        lastCompressionTime = Time.time;

        UpdateText();

        Debug.Log("Compression #" + compressionCount);
    }

    void UpdateText()
    {
        if (compressionText != null)
            compressionText.text = $"Compressions: {compressionCount}";
    }
}
