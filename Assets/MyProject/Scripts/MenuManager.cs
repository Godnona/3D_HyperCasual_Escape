using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject controlPanel;
    public GameObject shopPanel;
    public GameObject menuPanel;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void OpenShop()
    {
        GameManager.Instance.PauseGame();
        shopPanel.SetActive(true);
    }
    public void CloseShop()
    {
        GameManager.Instance.ResumeGame();
        shopPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        GameManager.Instance.PauseGame();
        menuPanel.SetActive(true);
    }
    public void CloseMenu()
    {
        GameManager.Instance.ResumeGame();
        menuPanel.SetActive(false);
    }

    public void ResetButton()
    {
        GameManager.Instance.ResetGame();
        GameManager.Instance.ResumeGame();
        menuPanel.SetActive(false);
    }   
     public void BackToMainmenu()
    {

    }    
}
