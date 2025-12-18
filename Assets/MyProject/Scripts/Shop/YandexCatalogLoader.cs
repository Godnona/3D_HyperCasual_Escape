using UnityEngine;
using System.Runtime.InteropServices;

public class YandexCatalogLoader : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void GetYandexCatalog();
#endif

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        GetYandexCatalog();
#endif
    }
}