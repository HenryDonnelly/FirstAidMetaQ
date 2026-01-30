using UnityEngine;

public class ButtonTouch : MonoBehaviour
{
    [Header("Reference to Aligner")]
    public ManikinAligner ManikinAligner; // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (ManikinAligner == null) return;
        if (ManikinAligner.IsLocked()) return;

        // Optional: only trigger from fingertip colliders
        if (other.CompareTag("FingerTip") || other.gameObject.name.Contains("index"))
        {
            ManikinAligner.LockAlignment();
        }
    }
}
