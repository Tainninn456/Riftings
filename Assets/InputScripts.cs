using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�^�����͂ł̊֐����s��script
/// </summary>
public class InputScripts : MonoBehaviour
{
    [Header("�Ώۂ̃Q�[�����[�h���w��")]
    [SerializeField] GameManager.ModeName targetModeName;
    [Header("�Ώۂ̃V�[�����w��")]
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
