using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Heart Images (size = maxLife)")]
    public Image[] hearts;   // 3 image tim

    private void OnEnable()
    {
        SaveManager.OnLifeChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        SaveManager.OnLifeChanged -= UpdateHealth;
    }

    private void Awake()
    {
        foreach (var heart in hearts)
            heart.enabled = false;
    }

    private void Start()
    {
        // cập nhật ngay khi vào game
        if (SaveManager.Instance != null)
            UpdateHealth(SaveManager.Instance.life);
        hearts[0].enabled = true;
    }

    private void UpdateHealth(int currentLife)
    {
        
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLife;
        }
    }
}
