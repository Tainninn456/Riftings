using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// �{�^�����͂ł̊֐����s��script
/// </summary>
public class InputScripts : MonoBehaviour
{
    //dotween�Ɋւ���萔
    readonly int MaxScale = 1;
    readonly float MinScale = 0.05f;
    readonly float animSpeed = 0.5f;

    //GameObject�̖��O�Ɋւ���萔
    const string displaysName = "Displays";

    //�G�f�B�^����s�֐��̖��O
    const string displayGetMethodName = "EditorMethodDisplayAssign";

    //script���ł̒萔
    readonly string[] resourceSelect = new string[9] {"soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    static Vector2 boalRigStock;

    public enum panelType
    {
        menuPanel,
        resultPanel
    }

    [Header("�Ώۂ̃Q�[�����[�h���w��F�p�����[�^")]
    [SerializeField] GameManager.ModeName targetModeName;
    [Header("�Ώۂ̃V�[�����w��F�p�����[�^")]
    [SerializeField] ChangeScene.SceneName targetSceneName;
    [Header("�p�l���J�ڕ����F�p�����[�^")]
    [SerializeField] RectTransform panelMoveDirection;
    [Header("�p�l���g�����X�t�H�[���F�R���|�[�l���g")]
    [SerializeField] RectTransform myTrans;
    [Header("�|�b�v�A�j���[�V�����̑Ώ�")]
    [SerializeField] RectTransform pop;
    [Header("�{�[����rigidbody")]
    [SerializeField] Rigidbody2D boalRig;

    [Header("Sound�̎�ނ��w��")]
    [SerializeField] AudioManager.SountType type;
    [Header("Sound�̎w�����w��")]
    [SerializeField] AudioManager.VolumeInstruction instruction;

    [Header("�\���n���܂Ƃ߂�")]
    [SerializeField] Display display;
    [Header("�v���C�Ɋւ���I�u�W�F�N�g���܂Ƃ߂�")]
    [SerializeField] GameObject objParent;

    [Header("dotween�̃A�j���[�V�����̎�ނ��w��")]
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
