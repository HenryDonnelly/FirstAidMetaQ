using UnityEngine;

public class ChestCompressionSensor : MonoBehaviour
{
    [Header("Compression Settings")]
    public float maxCompressionDepth = 0.06f; // 6 cm (real CPR)
    public float deadZone = 0.005f;            // Ignore tiny jitter

    private float restY;
    private float currentDepth;

    void Start()
    {
        // Record rest position at start
        restY = transform.position.y;
    }

    void Update()
    {
        float delta = restY - transform.position.y;

        if (delta < deadZone)
            currentDepth = 0f;
        else
            currentDepth = Mathf.Clamp(delta, 0f, maxCompressionDepth);

        Debug.Log($"Compression depth: {currentDepth * 100f:F1} cm");
    }

    public float GetCompressionDepth()
    {
        return currentDepth;
    }

    public float GetCompressionPercent()
    {
        return currentDepth / maxCompressionDepth;
    }
}
