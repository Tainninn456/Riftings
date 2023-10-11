using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボタン入力での関数実行のscript
/// </summary>
public class InputScripts : MonoBehaviour
{
    [Header("対象のゲームモードを指定")]
    [SerializeField] GameManager.ModeName targetModeName;
    [Header("対象のシーンを指定")]
    [SerializeField] ChangeScene.SceneName targetSceneName;
    public void SceneChange()
    {
        ChangeScene.instance.SceneLoad(targetSceneName);
        GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, targetModeName, GameManager.State.game);
    }
    public void GameMode()
    {
        GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.insert, targetModeName, GameManager.State.game);
    }
}
