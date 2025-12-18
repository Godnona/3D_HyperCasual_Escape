using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public int coin;

    public static event Action<int> OnCoinChanged;

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

    private void OnEnable()
    {
        YG2.onGetSDKData += LoadFromCloud;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= LoadFromCloud;
    }

    private void LoadFromCloud()
    {
        coin = YG2.saves.coins;
        OnCoinChanged?.Invoke(coin); // update UI ngay
        Debug.Log("SAVE LOADED, COIN = " + coin);
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
        coin = YG2.saves.coins;
        YG2.SaveProgress();
        OnCoinChanged?.Invoke(coin);
    }

    public void Load()
    {
        coin = YG2.saves.coins;
    }

    public void ForceUpdateUI()
    {
        coin = YG2.saves.coins;
        OnCoinChanged?.Invoke(coin);
    }
}
