using UnityEngine;
using YG;

public class ShopButton : MonoBehaviour
{
    public string productID;

    public void Buy()
    {
        YG2.PurchaseByID(productID);
    }
}
