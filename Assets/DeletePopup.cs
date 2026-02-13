using UnityEngine;

public class DeletePopup : MonoBehaviour
{
    public GameObject dialog;

    public void Close()
    {
        if (dialog != null)
            Destroy(dialog);
    }
}
