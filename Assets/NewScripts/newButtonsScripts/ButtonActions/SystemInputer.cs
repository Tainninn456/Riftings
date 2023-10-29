using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemInputer : InputParent
{
    const string sceneButtonGetName = "SceneButtonGetter";
    const string mainSceneName = "menuScene";
    const string playSceneName = "playScene";
    //�����̃I�u�W�F�N�g�ƊK�w�Ŕ���Ă��邽�߃V���A���C�Y�ɓ��ꍞ��

    [SerializeField] SystemAction systemAction;

    [Header("���C�����j���[")]
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
    [Header("�T�E���h�|�b�v(0=up,1=down)&(�C���Q�[���ł��g�p)")]
    [SerializeField] Button[] soundPopButtons;

    [Header("�����ւ��|�b�v�A�b�v")]
    [SerializeField] Button[] clothPopUpButtons;

    [Header("�����ւ��|�b�v�_�E��")]
    [SerializeField] Button clothPopDownButton;

    //�V�[���J�ڃ{�^���̎擾
    [Header("�V�[���J�ڃ{�^��(�C���Q�[���ł��g�p,0=�����[�h,1=���j���[�ɑJ�ځA�|�[�Y�ƃ��U���g�̏��Ԃŕێ�)")]
    [SerializeField] Button[] sceneButtons;

    [Header("�C���Q�[��")]
    [Header("�|�[�Y�|�b�v(0=up,1=down)")]
    [SerializeField] Button[] porzPopButtons;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            //�p�l���J�ڂɊւ��镔��
            clothBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 0));
            ToClothButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 0));
            ToScoreButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 1));
            scoreBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 1));

            foreach (Button button in clothPopUpButtons)
            {
                button.onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Cloth));
            }
            clothPopDownButton.onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Cloth));

            //�V�[���J�ڌn�Ɋւ��镔��
            for (int i = 0; i < sceneButtons.Length; i++)
            {
                int inputNumber = i;
                sceneButtons[i].onClick.AddListener(() => systemAction.SceneMoveStarter(inputNumber));
            }
            soundPopButtons[0].onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Sound));
            soundPopButtons[1].onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Sound));
        }
        else if (SceneManager.GetActiveScene().name == playSceneName)
        {
            porzPopButtons[0].onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Porz));
            porzPopButtons[1].onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Porz));

            soundPopButtons[0].onClick.AddListener(() => systemAction.PopChain(SystemAction.PopOperaition.up, SystemAction.PopName.Sound));
            soundPopButtons[1].onClick.AddListener(() => systemAction.PopChain(SystemAction.PopOperaition.down, SystemAction.PopName.Sound));

            sceneButtons[0].onClick.AddListener(() => systemAction.SimpleSceneMover(0));
            sceneButtons[1].onClick.AddListener(() => systemAction.SimpleSceneMover(1));
            sceneButtons[2].onClick.AddListener(() => systemAction.SimpleSceneMover(0));
            sceneButtons[3].onClick.AddListener(() => systemAction.SimpleSceneMover(1));
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
