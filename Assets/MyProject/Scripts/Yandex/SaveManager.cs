using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public int coin;
    public static event Action<int> OnCoinChanged;

    public int life;
    public static event Action<int> OnLifeChanged;
    public int maxLife = 3;

    private bool pendingPurchasesChecked;

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
        YG2.onPurchaseSuccess += HandlePurchaseSuccess;
    }

    private void OnDisable()
    {
        YG2.onGetSDKData -= LoadFromCloud;
        YG2.onPurchaseSuccess -= HandlePurchaseSuccess;
    }

    private void LoadFromCloud()
    {
        // Load Coin
        coin = YG2.saves.coins;
        OnCoinChanged?.Invoke(coin); // update UI ngay
        //Debug.Log("SAVE LOADED, COIN = " + coin);

        // Load health
        life = YG2.saves.life;
        OnLifeChanged?.Invoke(life);
        Debug.Log($"SAVE LOADED | COIN={coin} | LIFE={life}");

        if (!pendingPurchasesChecked)
        {
            pendingPurchasesChecked = true;
            YG2.ConsumePurchases();
        }
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

    public bool SpendCoin(int amount)
    {
        if (YG2.saves.coins < amount)
        {
            Debug.Log("NOT ENOUGH COIN");
            return false;
        }

        YG2.saves.coins -= amount;
        coin = YG2.saves.coins;
        YG2.SaveProgress();
        OnCoinChanged?.Invoke(coin);

        return true;
    }

    public void Load()
    {
        coin = YG2.saves.coins;
        life = YG2.saves.life;

    }

    public void ForceUpdateUI()
    {
        coin = YG2.saves.coins;
        OnCoinChanged?.Invoke(coin);

        life = YG2.saves.life;
        OnLifeChanged?.Invoke(life);
    }

    // ============== Handle Health ==============
    public bool LoseLife()
    {
        if (YG2.saves.life <= 0)
            return false;

        YG2.saves.life--;
        life = YG2.saves.life;

        YG2.SaveProgress();
        OnLifeChanged?.Invoke(life);

        return life > 0;
    }

    public void AddLife(int amount = 1)
    {
        YG2.saves.life = Mathf.Min(YG2.saves.life + amount, maxLife); ;
        life = YG2.saves.life;

        YG2.SaveProgress();
        OnLifeChanged?.Invoke(life);

        Debug.Log("ADD LIFE: " + amount + " | TOTAL LIFE = " + life);
    }

    public void ResetLife(int value = 1)
    {
        YG2.saves.life = value;
        life = value;

        YG2.SaveProgress();
        OnLifeChanged?.Invoke(life);
    }

    private void HandlePurchaseSuccess(string productId)
    {
        int purchasedCoins = GetPurchasedCoinAmount(productId);
        if (purchasedCoins <= 0)
        {
            Debug.LogWarning($"PURCHASE SUCCESS but no coin reward mapped for product '{productId}'");
            return;
        }

        AddCoin(purchasedCoins);
        Debug.Log($"PURCHASE REWARDED | PRODUCT={productId} | COINS+={purchasedCoins} | TOTAL={coin}");
    }

    private static int GetPurchasedCoinAmount(string productId)
    {
        const string coinPrefix = "coin_";

        if (string.IsNullOrEmpty(productId) ||
            !productId.StartsWith(coinPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return 0;
        }

        string amountText = productId.Substring(coinPrefix.Length);
        return int.TryParse(amountText, out int amount) ? amount : 0;
    }
}
