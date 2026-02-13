using UnityEngine;

public class ManikinAligner : MonoBehaviour
{
    [Header("Alignment")]
    public Transform head;
    public Vector3 headOffset = new Vector3(0f, -0.6f, -0.6f);

    private bool isLocked = false;

    void Start()
    {
        if (head == null)
        {
            Camera cam = Camera.main;
            if (cam != null)
                head = cam.transform;
        }

        if (head != null)
        {
            transform.SetParent(head);
            transform.localPosition = headOffset;
            transform.localRotation = Quaternion.identity;
        }
    }

    void LateUpdate()
    {
        if (isLocked || head == null) return;

        // Keep upright, follow head yaw only
        Vector3 flatForward = Vector3.ProjectOnPlane(head.forward, Vector3.up);
        if (flatForward.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(flatForward, Vector3.up);
        }
    }

    public void LockAlignment()
    {
        if (isLocked) return;

        isLocked = true;

        // Preserve world transform
        Vector3 worldPos = transform.position;
        Quaternion worldRot = transform.rotation;

        transform.SetParent(null);
        transform.position = worldPos;
        transform.rotation = worldRot;

        Debug.Log("Manikin alignment LOCKED");
    }

    public void LockAlignment(bool isOn)
    {
        if (!isOn) return;
        LockAlignment();
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void UnlockAlignment()
{
    if (!isLocked) return;

    isLocked = false;

    if (head != null)
    {
        transform.SetParent(head);
        transform.localPosition = headOffset;
        transform.localRotation = Quaternion.identity;
    }

    Debug.Log("Manikin alignment UNLOCKED");
}
}
