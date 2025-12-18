using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SaveManager.Instance.ForceUpdateUI();
        SceneManager.LoadScene(SaveManager.Instance.GetLastLevel());
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Map1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
