using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Loader.Load("Map1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
