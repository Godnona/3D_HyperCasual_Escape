using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void Refresh()
    {
        coinText.text = SaveManager.Instance.coin.ToString();
    }

    void Start()
    {
        Refresh();
    }
}
