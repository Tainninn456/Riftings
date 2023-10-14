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

    static Vector2 boalRigStock;

    public enum panelType
    {
        menuPanel,
        resultPanel
    }

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
    [Header("ボールのrigidbody")]
    [SerializeField] Rigidbody2D boalRig;

    [Header("Soundの種類を指定")]
    [SerializeField] AudioManager.SountType type;
    [Header("Soundの指示を指定")]
    [SerializeField] AudioManager.VolumeInstruction instruction;

    [Header("表示系をまとめる")]
    [SerializeField] Display display;
    [Header("プレイに関するオブジェクトをまとめる")]
    [SerializeField] GameObject objParent;

    [Header("dotweenのアニメーションの種類を指定")]
    [SerializeField] panelType panelSelection;

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
        if (panelSelection == panelType.menuPanel)
        {
            myTrans.DOMove(panelMoveDirection.position, animSpeed);
        }
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
        display.pops[Index].GetComponent<RectTransform>().DOScale(new Vector3(MaxScale, MaxScale, MaxScale), animSpeed).SetUpdate(true);
    }

    public void PopDown(int Index)
    {
        display.pops[Index].GetComponent<RectTransform>().DOScale(new Vector3(MinScale, MinScale,MinScale), animSpeed).OnComplete(() => display.pops[Index].SetActive(false)).OnComplete(() => display.popParent.SetActive(false));
        if(GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.stop) == (int)GameManager.State.stop)
        {
            objParent.SetActive(true);
            Time.timeScale = 1;
            boalRig.velocity = boalRigStock;
            GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, GameManager.ModeName.soccer, GameManager.State.game);
        }
    }

    public void SelectChange(int Index)
    {
        GameManager.ModeName name = GameManager.ModeName.soccer;
        switch (Index)
        {
            case 0:
                name = GameManager.ModeName.soccer;
                break;
            case 1:
                name = GameManager.ModeName.tennis;
                break;
            case 2:
                name = GameManager.ModeName.baseball;
                break;
            case 3:
                name = GameManager.ModeName.boring;
                break;
            case 4:
                name = GameManager.ModeName.panchi;
                break;
            case 5:
                name = GameManager.ModeName.tabletennis;
                break;
            case 6:
                name = GameManager.ModeName.ragby;
                break;
            case 7:
                name = GameManager.ModeName.biriyard;
                break;
            case 8:
                name = GameManager.ModeName.volley;
                break;
        }
        Sprite[] playerImages = Resources.LoadAll<Sprite>(resourceSelect[Index]);
        for(int i = 0; i < playerImages.Length; i++)
        {
            display.clothes[i].sprite = playerImages[i];
        }
        for (int i = 0; i < display.clothes.Length; i++)
        {
            display.modePrefer[i].targetModeName = name;
        }
    }

    public void ClothDesicion(int Index)
    {
        GameManager.Instance.ClothAccess(GameManager.Information.clothbool, GameManager.Instruction.insert, true);
        GameManager.Instance.InformationAccess(GameManager.Information.cloth, GameManager.Instruction.insert, targetModeName, Index);
    }

    public void Poze()
    {
        boalRigStock = boalRig.velocity;
        Time.timeScale = 0;
        objParent.SetActive(false);
        GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, GameManager.ModeName.soccer, GameManager.State.stop);
    }
    [ContextMenu(displayGetMethodName)]
    private void EditorMethodDisplayAssign()
    {
        display = GameObject.Find(displaysName).GetComponent<Display>();
    }
}
