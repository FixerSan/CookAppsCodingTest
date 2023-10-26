using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;
using static UnityEngine.Application;

public class SceneManager 
{
    private string currentScene = string.Empty;
    private bool isLoading = false;
    private Action loadCallback;

    public void LoadScene(Define.Scene _scene, Action _loadCallback = null)
    {
        if (isLoading) return;
        isLoading = true;
        loadCallback = _loadCallback;
        string sceneName = _scene.ToString();
        //Managers.
        //Managers.Screen.FadeIn(1, () => { UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); });
    }

    private void LoadedScene(UnityEngine.SceneManagement.Scene _scene, LoadSceneMode _loadSceneMode)
    {
        //Managers.Pool.Clear();
        //RemoveScene(currentScene, () =>
        //{
        //    currentScene = _scene.name;
        //    AddScene(_scene.name);
        //});
    }

    public SceneManager()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadedScene;
    }
}
