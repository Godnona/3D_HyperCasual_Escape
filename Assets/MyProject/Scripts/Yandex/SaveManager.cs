using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ===== SAVE =====
    public void SaveGame()
    {
        YG2.saves.lastLevel = SceneManager.GetActiveScene().name;
        YG2.SaveProgress();
    }

    public void AddCoin(int amount)
    {
        YG2.saves.coins += amount;
        YG2.SaveProgress();
    }

    public int GetCoins()
    {
        return YG2.saves.coins;
    }

    public string GetLastLevel()
    {
        return YG2.saves.lastLevel;
    }
}
