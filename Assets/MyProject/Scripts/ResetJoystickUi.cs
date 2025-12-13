using UnityEngine;

public class ResetJoystickUi : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponentInChildren<RectTransform>();
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = Vector2.zero;

    }
}
