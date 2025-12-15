using UnityEngine;
using YG;

public class PurchaseListener : MonoBehaviour
{
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
        int coin = productID switch
        {
            "coin_100" => 100,
            "coin_200" => 200,
            "coin_500" => 500,
            "coin_800" => 800,
            "coin_1000" => 1000,
            _ => 0
        };

        if (coin > 0)
        {
            SaveManager.Instance.AddCoin(coin);
            YG2.SaveProgress();
        }
    }
}
