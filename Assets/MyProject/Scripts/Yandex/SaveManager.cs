using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public int coin;

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

    // ===== Handle Level =====
    public void SaveGame()
    {
        YG2.saves.lastLevel = SceneManager.GetActiveScene().name;
        YG2.SaveProgress();
    }

    public string GetLastLevel()
    {
        return YG2.saves.lastLevel;
    }

    // ===== Handle Coin =====
    public void AddCoin(int amount)
    {
        YG2.saves.coins += amount;
        YG2.SaveProgress();
    }

    public void Load()
    {
        coin = YG2.saves.coins;
    }

    //public int GetCoins()
    //{
    //    return YG2.saves.coins;
    //}

    //public void SetCoins(int value)
    //{
    //    YG2.saves.coins = value;
    //    YG2.SaveProgress();
    //}



}
