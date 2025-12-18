using System.Collections.Generic;
using TMPro;
using UnityEngine; 

public class YandexPriceReceiver : MonoBehaviour
{
    public static YandexPriceReceiver Instance;

    public Dictionary<string, TMP_Text> priceTexts =
        new Dictionary<string, TMP_Text>();

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register(string productID, TMP_Text text)
    {
        priceTexts[productID] = text;
    }

    public void OnReceivePrice(string json)
    {
        var data = JsonUtility.FromJson<PriceData>(json);

        if (priceTexts.TryGetValue(data.id, out var text))
        {
            text.text = $"{data.price} {data.currency}"; // 19 RUB
        }
    }
}

[System.Serializable]
public class PriceData
{
    public string id;
    public string price;
    public string currency;
}
