using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ボタン入力での関数実行のscript
/// </summary>
public class InputScripts : MonoBehaviour
{
    //dotweenに関する定数
    readonly int MaxScale = 1;
    readonly float MinScale = 0.05f;
    readonly float animSpeed = 0.5f;

    //GameObjectの名前に関する定数
    const string displaysName = "Displays";

    //エディタ上実行関数の名前
    const string displayGetMethodName = "EditorMethodDisplayAssign";

    //script内での定数
    readonly string[] resourceSelect = new string[9] {"soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    [Header("対象のゲームモードを指定：パラメータ")]
    [SerializeField] GameManager.ModeName targetModeName;
    [Header("対象のシーンを指定：パラメータ")]
    [SerializeField] ChangeScene.SceneName targetSceneName;
    [Header("パネル遷移方向：パラメータ")]
    [SerializeField] RectTransform panelMoveDirection;
    [Header("パネルトランスフォーム：コンポーネント")]
    [SerializeField] RectTransform myTrans;
    [Header("ポップアニメーションの対象")]
    [SerializeField] RectTransform pop;

    [Header("Soundの種類を指定")]
    [SerializeField] AudioManager.SountType type;
    [Header("Soundの指示を指定")]
    [SerializeField] AudioManager.VolumeInstruction instruction;

    [Header("表示系をまとめる")]
    [SerializeField] Display display;

    public void SceneChange()
    {
        ChangeScene.instance.SceneLoad(targetSceneName);
        GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, targetModeName, GameManager.State.game);
    }
    public void GameMode()
    {
        GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.insert, targetModeName, GameManager.State.game);
    }
    public void PanelMove()
    {
        myTrans.DOMove(panelMoveDirection.position, animSpeed);
    }
    public void SoundVolumeAction()
    {
        if (type == AudioManager.SountType.BGM)
        {
            AudioManager.instance.BGMVolumeChange(instruction);
        }
        else if (type == AudioManager.SountType.SE)
        {
            AudioManager.instance.SEVolumeChange(instruction);
        }
    }

    public void PopUp(int Index)
    {
        display.popParent.SetActive(true);
        for(int i = 0; i < display.pops.Length; i++)
        {
            display.pops[i].SetActive(false);
        }
        display.pops[Index].SetActive(true);
        display.pops[Index].GetComponent<RectTransform>().DOScale(new Vector3(MaxScale, MaxScale, MaxScale), animSpeed);
    }

    public void PopDown(int Index)
    {
        display.pops[Index].GetComponent<RectTransform>().DOScale(new Vector3(MinScale, MinScale,MinScale), animSpeed).OnComplete(() => display.pops[Index].SetActive(false)).OnComplete(() => display.popParent.SetActive(false));
    }

    public void SelectChange(int Index)
    {
        Sprite[] playerImages = Resources.LoadAll<Sprite>(resourceSelect[Index]);
        for(int i = 0; i < playerImages.Length; i++)
        {
            Debug.Log(playerImages[i]);
            display.clothes[i].sprite = playerImages[i];
        }
    }
    [ContextMenu(displayGetMethodName)]
    private void EditorMethodDisplayAssign()
    {
        display = GameObject.Find(displaysName).GetComponent<Display>();
    }
}
