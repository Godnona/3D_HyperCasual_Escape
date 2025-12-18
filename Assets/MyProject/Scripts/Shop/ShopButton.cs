using TMPro;
using UnityEngine;
using YG;

public class ShopButton : MonoBehaviour
{
    public string productID;
    public TMP_Text priceText;

    void Start()
    {
        YandexPriceReceiver.Instance.Register(productID, priceText);
    }

    public void Buy()
    {
        Debug.Log("CLICK BUY: " + productID);
        YG2.BuyPayments(productID);
        //YG2.PurchaseByID(productID);
        
    }
}
