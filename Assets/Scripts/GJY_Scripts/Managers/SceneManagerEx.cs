using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene => Object.FindObjectOfType<BaseScene>();

    public void LoadScene(Define.TestScenes scene)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(scene));
    }

    private string GetSceneName(Define.TestScenes scene)
    {
        string sceneName = System.Enum.GetName(typeof(Define.TestScenes), scene);
        return sceneName;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
