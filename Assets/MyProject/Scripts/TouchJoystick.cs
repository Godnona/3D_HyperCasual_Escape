using UnityEngine;

public class TouchJoystick : MonoBehaviour
{
    public GameObject joystickPrefab;   // prefab joystick của bạn
    private GameObject currentJoystick; // joystick đang tồn tại

    void Update()
    {
        // Khi người chơi CHẠM XUỐNG
        if (Input.GetMouseButtonDown(0))
        {
            SpawnJoystick(Input.mousePosition);
        }

        // Khi người chơi NHẢ TAY
        if (Input.GetMouseButtonUp(0))
        {
            DestroyJoystick();
        }
    }

    private void SpawnJoystick(Vector2 screenPos)
    {
        if (currentJoystick != null)
            Destroy(currentJoystick);

        // Spawn joystick tại Canvas
        currentJoystick = Instantiate(joystickPrefab, transform);

        // Chuyển màn hình thành anchoredPosition UI
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform,
            screenPos,
            null,
            out Vector2 localPos
        );

        RectTransform joyRect = currentJoystick.GetComponent<RectTransform>();
        joyRect.anchoredPosition = localPos;

        // Gán joystick mới vào PlayerController
        PlayerController player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        player.joystick = currentJoystick.GetComponentInChildren<Joystick>();
    }

    private void DestroyJoystick()
    {
        if (currentJoystick != null)
        {
            Destroy(currentJoystick);
            currentJoystick = null;
        }

        // Reset input để player không tiếp tục chạy
        PlayerController player = GameManager.Instance.GetPlayer().GetComponent<PlayerController>();
        player.joystick = null;
    }
}
