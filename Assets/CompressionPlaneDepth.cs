using UnityEngine;
using TMPro;

public class ChestPlaneDepth : MonoBehaviour
{
    [Header("Settings")]
    public float maxDepth = 0.06f;
    public float returnSpeed = 6f;

    [Header("UI")]
    public TextMeshProUGUI depthText;

    private Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        Vector3 local = transform.localPosition;

        // Clamp downward motion only
        float depth = Mathf.Clamp(startLocalPos.y - local.y, 0f, maxDepth);
        local.y = startLocalPos.y - depth;

        // Spring back up
        if (depth < maxDepth)
        {
            local.y = Mathf.Lerp(local.y, startLocalPos.y, Time.fixedDeltaTime * returnSpeed);
        }

        transform.localPosition = local;

        if (depthText != null)
        {
            depthText.text = $"Depth: {(depth * 100f):F1} cm";
        }
    }

    public float GetDepth()
    {
        return Mathf.Clamp(startLocalPos.y - transform.localPosition.y, 0f, maxDepth);
    }
}
