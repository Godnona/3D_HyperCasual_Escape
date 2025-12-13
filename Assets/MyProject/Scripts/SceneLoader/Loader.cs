using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private class LoadingMonobehaviour : MonoBehaviour { }
    private static AsyncOperation loadingAsyncOperation;

    public enum Scene
    {
        LoadingScene
    }

    private static Action m_onLoaderCallback;

    public static void Load(string nameScene)
    {
        // set the loader callback action to load the targer scene
        m_onLoaderCallback = () =>
        {
            GameObject _loadingGameobject = new GameObject("loading Game object");
            _loadingGameobject.AddComponent<LoadingMonobehaviour>().StartCoroutine(LoadSceneAsync(nameScene)); 
        };
    
        // load the LoadingScen
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    private static IEnumerator LoadSceneAsync(string nameScene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(nameScene);

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProcess()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else 
            return 1.0f; 

    }

    public static void LoaderCallback()
    {
        if(m_onLoaderCallback != null)
        {
            m_onLoaderCallback();
            m_onLoaderCallback = null;
        }
    }

}
