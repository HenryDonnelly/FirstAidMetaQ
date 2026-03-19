using UnityEngine;
using UnityEngine.UI;
using Oculus.Interaction;

public class ThemeToggleController : MonoBehaviour
{
    public UIThemeManager themeManager;
    public Toggle toggle;

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            themeManager.ApplyTheme(1);
        }
        else
        {
            themeManager.ApplyTheme(0);
        }
    }
}