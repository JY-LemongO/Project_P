using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    Main,
    Play,
}

public class SceneLoader : Singleton<SceneLoader>
{
    public SceneType CurrentScene { get; private set; }

    public void LoadScene(SceneType scene)
    {
        SceneManager.LoadScene(scene.ToString());

        CurrentScene = scene; // 이건 Scene 클래스에서 해줘야 할 것 같기도 하고~   
        // 뭔가 더 해야할듯
    }

    public void LoadSceneAysnc(SceneType scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString());
    }
}