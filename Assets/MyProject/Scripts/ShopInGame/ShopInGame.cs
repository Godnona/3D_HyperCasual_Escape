using UnityEngine;

public class ShopInGame : MonoBehaviour
{
    public int itemPrice = 10;
    public int addLifeAmount = 1;

    public void BuyItem()
    {
        if (SaveManager.Instance == null || GameManager.Instance == null)
        {
            Debug.LogError("Manager not found!");
            return;
        }

        bool success = SaveManager.Instance.SpendCoin(itemPrice);

        if (!success)
        {
            Debug.Log("NOT ENOUGH COIN");
            return;
        }
        Debug.Log("BUY ITEM SUCCESS");

        // ⭐ add life (không cần player tồn tại)
        SaveManager.Instance.AddLife(addLifeAmount);

        GameObject player = GameManager.Instance.GetPlayer();
        if (player != null)
        {
            // ví dụ: play effect, sound
            Debug.Log("Player found");
            
        }
    }

    void GiveItemToPlayer()
    {
        // TODO:
        // - add skin
        // - add weapon
        // - unlock item
    }

}
