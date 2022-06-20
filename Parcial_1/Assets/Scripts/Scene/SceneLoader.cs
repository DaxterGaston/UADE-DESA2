using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoaderParams
{
    public static string sceneToLoad;
}

public class SceneLoader : MonoBehaviour
{
    private const string DEFAULT_SCENE = "main_menu";
    
    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        AsyncOperation level =
            SceneManager.LoadSceneAsync(SceneLoaderParams.sceneToLoad == "" ? DEFAULT_SCENE : SceneLoaderParams.sceneToLoad); 
        yield return new WaitForEndOfFrame();
    }
}