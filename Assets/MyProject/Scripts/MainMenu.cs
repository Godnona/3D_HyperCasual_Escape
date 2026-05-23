using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance?.PlayButtonClick();
        SaveManager.Instance.ForceUpdateUI();
        SceneManager.LoadScene(SaveManager.Instance.GetLastLevel());
    }
    public void NewGame()
    {
        AudioManager.Instance?.PlayButtonClick();
        SceneManager.LoadScene("Map1");
        SaveManager.Instance.ResetLife();
    }
    public void QuitGame()
    {
        AudioManager.Instance?.PlayButtonClick();
        Application.Quit();
    }
}
