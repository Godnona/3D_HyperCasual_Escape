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

    private void OnEnable()
    {
        SaveManager.OnCoinChanged += UpdateUI;
    }

    private void OnDisable()
    {
        SaveManager.OnCoinChanged -= UpdateUI;
    }

    private void UpdateUI(int value)
    {
        coinText.text = value.ToString();
    }
}
