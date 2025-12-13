using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.SceneManagement;
using System.Collections;

using YG;

public class GameManager : MonoBehaviour 
{ 
    public static GameManager Instance; 
    [Header("Player")] 
    public GameObject playerPrefab;
    public GameObject playerJoystick;

    private GameObject playerInstance;
    public GameObject GetPlayer()
    {
        return playerInstance;
    }
    

    [Header("Game State")] 
    public bool isGamePaused = false; 
    
    //=====================================================================
    void Awake() 
    { 
        // Create just one game manager
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        } 
        Instance = this; 
        DontDestroyOnLoad(gameObject); 
    } 
    
    private void Start() 
    {
        // SpawnPlayer(); 
        // Auto Save
        //if (SceneManager.GetActiveScene().name.StartsWith("Map"))
        //{
        //    SaveManager.Instance.SaveGame();
        //}
    }


    //======================= Load player when next scene ============================
    private void OnEnable() 
    { 
        SceneManager.sceneLoaded += OnSceneLoaded; 
    } 
    private void OnDisable() 
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    } 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name.StartsWith("Map"))
            StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return null;
        if (SceneManager.GetActiveScene().name.StartsWith("Map"))
        {
            SpawnPlayer();
            SaveManager.Instance.SaveGame(); // just save scene map
        }
    }

    //=====================================================================
    //======================= Save game Yandex ============================
    //=====================================================================
    private void OnApplicationPause(bool pause)
    {
        if (pause && SceneManager.GetActiveScene().name.StartsWith("Map"))
            SaveManager.Instance.SaveGame();
    }

    private void OnApplicationQuit()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Map"))
            SaveManager.Instance.SaveGame();
    }

    //========================================================================
    //======================== Handle Player =================================
    //========================================================================
    private void SpawnPlayer()
    {
        if (!SceneManager.GetActiveScene().name.StartsWith("Map"))
            return;

        // Nếu player cũ còn sót trong DontDestroyOnLoad → destroy luôn
        if (playerInstance != null)
            Destroy(playerInstance);

        // Spawn player mới 100%
        playerInstance = Instantiate(playerPrefab);

        ReAssignJoystick();
        ResetPlayer();
    }

    private void ReAssignJoystick()
    {
        if (playerJoystick == null)
        {
            Debug.Log("finding...");
            playerJoystick = GameObject.Find("Floating Joystick");
        }

        if (playerInstance == null)
            return;

        if (playerJoystick == null)
            Debug.Log("Please assign joystick into game manager");
        if (playerJoystick != null)
        {
            Debug.Log("found");
            playerInstance.GetComponent<PlayerController>().joystick = playerJoystick.GetComponent<Joystick>();
        }
    }

    public void ResetPlayer() 
    { 
        if (playerInstance != null) 
            playerInstance.transform.position = new Vector3(0, 0, -4);
    }

    //=====================================================================
    //======================= Call from other class =======================
    //=====================================================================
    public void LoadNextLevel() 
    { 
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        int nextIndex = currentIndex + 1;
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("No more maps!");
            return;
        }
        string nextSceneName = "Map" + (nextIndex);
        Loader.Load(nextSceneName);
    }

    //=====================================================================
    //============================ Function Button ============================
    //=====================================================================
    public void PauseGame() 
    { 
        isGamePaused = true; 
        Time.timeScale = 0f; 
    } 
    public void ResumeGame() 
    { 
        isGamePaused = false; 
        Time.timeScale = 1f; 
    } 
    public void ResetGame() 
    { 
        if (playerInstance != null) 
        { 
            Destroy(playerInstance); 
            playerInstance = null; 
        }

        int _currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentIndex);
        
        //ResetPlayer();
        //ReAssignJoystick();
    } 
}