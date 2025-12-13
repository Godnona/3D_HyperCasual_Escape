using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.SceneManagement;
using System.Collections;
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
    } 
    
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
        StartCoroutine(DelayAssign());
    }

    private IEnumerator DelayAssign()
    {
        yield return null;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        // Nếu player cũ còn sót trong DontDestroyOnLoad → destroy luôn
        if (playerInstance != null)
            Destroy(playerInstance);

        // Spawn player mới 100%
        playerInstance = Instantiate(playerPrefab);

        ReAssignJoystick();
        ResetPlayer();
        
    }

    public void ResetPlayer() 
    { 
        if (playerInstance != null) 
            playerInstance.transform.position = new Vector3(0, 0, -4);
    } 
    
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
    private void ReAssignJoystick() 
    {
        if(playerJoystick == null)
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