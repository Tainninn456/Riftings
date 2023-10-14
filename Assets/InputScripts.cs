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

    [Header("Sound�̎�ނ��w��")]
    [SerializeField] AudioManager.SountType type;
    [Header("Sound�̎w�����w��")]
    [SerializeField] AudioManager.VolumeInstruction instruction;

    [Header("�\���n���܂Ƃ߂�")]
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
