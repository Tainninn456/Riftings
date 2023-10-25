using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SystemInputer : InputParent
{
    const string sceneButtonGetName = "SceneButtonGetter";
    //�����̃I�u�W�F�N�g�ƊK�w�Ŕ���Ă��邽�߃V���A���C�Y�ɓ��ꍞ��

    [SerializeField] SystemAction systemAction;

    //�ړ��{�^���̎擾
    [Header("�����ւ��p�l�����烁�C���p�l���֖߂�")]
    [SerializeField] Button clothBackButton;

    [Header("�����ւ��p�l���ւ̈ړ�")]
    [SerializeField] Button ToClothButton;

    [Header("�X�R�A�p�l���ւ̈ړ�")]
    [SerializeField] Button ToScoreButton;

    [Header("�X�R�A�p�l�����烁�C���p�l���֖߂�")]
    [SerializeField] Button scoreBackButton;

    //�|�b�v�{�^���̎擾
    [Header("�T�E���h�|�b�v(0=up,1=down)")]
    [SerializeField] Button[] soundPopButtons;

    [Header("�����ւ��|�b�v�A�b�v")]
    [SerializeField] Button[] clothPopUpButtons;

    [Header("�����ւ��|�b�v�_�E��")]
    [SerializeField] Button clothPopDownButton;

    //�V�[���J�ڃ{�^���̎擾
    [Header("�V�[���J�ڃ{�^��")]
    [SerializeField] Button[] sceneButtons;

    private void Start()
    {
        //�p�l���J�ڂɊւ��镔��
        clothBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 0));
        ToClothButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 0));
        ToScoreButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 1));
        scoreBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 1));

        //�|�b�v�Ɋւ��镔��
        soundPopButtons[0].onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Sound));
        soundPopButtons[1].onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Sound));

        foreach(Button button in clothPopUpButtons)
        {
            button.onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Cloth));
        }
        clothPopDownButton.onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Cloth));

        //�V�[���J�ڌn�Ɋւ��镔��
        for(int i = 0; i < sceneButtons.Length; i++)
        {
            int inputNumber = i;
            sceneButtons[i].onClick.AddListener(() => systemAction.SceneMoveStarter(inputNumber));
        }
    }

    /// <summary>
    /// �G�f�B�^����s�֐�
    /// </summary>
    [ContextMenu(sceneButtonGetName)]
    private void SceneButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref sceneButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            sceneButtons[i] = buttonArray[i];
        }
    }
}
