using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// シーン遷移に関するクラス、シングルトンでどのクラスからも呼び出すことができる
/// </summary>
public class ChangeScene : MonoBehaviour
{
    private static ChangeScene instance;
    public static ChangeScene Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ChangeScene>();
            }
            return instance;
        }
    }

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
