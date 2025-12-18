using System.Collections.Generic;
using UnityEngine;
using YG;
using YG.Utils.Pay;

public class PurchaseListener : MonoBehaviour
{
    //private bool purchaseHandled = false;
    private void Awake()
    {
    }

    void OnEnable()
    {
        YG2.onPurchaseSuccess += OnPurchaseSuccess;
        //purchaseHandled = false;
    }

    void OnDisable()
    {
        YG2.onPurchaseSuccess -= OnPurchaseSuccess;

    }

    void OnPurchaseSuccess(string productID)
    {
        //if (purchaseHandled)
        //{
        //    Debug.LogWarning("Purchase already handled, ignore!");
        //    return;
        //}
        //purchaseHandled = true;

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
        YG2.ConsumePurchases();
        //YG2.ConsumePurchaseByID(productID, purchaseHandled);
    }

}
