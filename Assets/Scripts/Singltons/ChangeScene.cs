using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーン遷移に関するクラス、シングルトンでどのクラスからも呼び出すことができる
/// </summary>
public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;

    public enum SceneName
    {
        menuScene,
        playScene
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //シーンをロードする関数,enumでシーンの名前を取得している
    public void SceneLoad(SceneName targetName)
    {
        SceneManager.LoadScene(targetName.ToString());
    }
}
