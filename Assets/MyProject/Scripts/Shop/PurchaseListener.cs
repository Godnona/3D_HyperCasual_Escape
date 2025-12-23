using System.Collections.Generic;
using UnityEngine;
using YG;
using YG.Utils.Pay;

public class PurchaseListener : MonoBehaviour
{
    public static PurchaseListener Instance { get; private set; }

    private void Start()
    {
        //YG2.ConsumePurchases();

        // Create just one game manager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        YG2.onPurchaseSuccess += OnPurchaseSuccess;
    }

    void OnDisable()
    {
        YG2.onPurchaseSuccess -= OnPurchaseSuccess;

    }

    void OnPurchaseSuccess(string productID)
    {
        Debug.Log("BUY SUCCESS: " + productID);
        int coin = productID switch
        {
            "coin_100" => 100,
            "coin_200" => 200,
            "coin_500" => 500,
            "coin_800" => 800,
            "coin_1000" => 1000,
            _ => 0
        };

        if (coin <= 0) return;

        SaveManager.Instance.AddCoin(coin);
        YG2.SaveProgress();
    }

}
