using UnityEngine;

public class ManikinAligner : MonoBehaviour
{
    [Header("References")]
    public Transform head;                 // CenterEyeAnchor
    public GameObject virtualManikinMesh;  // Ghost mesh (can be semi-transparent)
    public GameObject lockUI;              // Canvas (UI button root)

    [Header("Alignment Settings")]
    public Vector3 headOffset = new Vector3(0f, -0.6f, 0.4f);

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
        if (isLocked) return;

        // Keep mannequin upright (no head tilt)
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

        // Detach from head so it stops moving
        transform.SetParent(null);

        // Hide ghost mesh
        if (virtualManikinMesh != null)
            virtualManikinMesh.SetActive(false);

        // Hide UI
        if (lockUI != null)
            lockUI.SetActive(false);

        Debug.Log("Manikin alignment locked");
    }

    public bool IsLocked()
    {
        return isLocked;
    }
}
