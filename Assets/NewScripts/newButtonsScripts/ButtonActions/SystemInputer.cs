using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SystemInputer : InputParent
{
    const string sceneButtonGetName = "SceneButtonGetter";
    //複数のオブジェクトと階層で被っているためシリアライズに入れ込む

    [SerializeField] SystemAction systemAction;

    //移動ボタンの取得
    [Header("着せ替えパネルからメインパネルへ戻る")]
    [SerializeField] Button clothBackButton;

    [Header("着せ替えパネルへの移動")]
    [SerializeField] Button ToClothButton;

    [Header("スコアパネルへの移動")]
    [SerializeField] Button ToScoreButton;

    [Header("スコアパネルからメインパネルへ戻る")]
    [SerializeField] Button scoreBackButton;

    //ポップボタンの取得
    [Header("サウンドポップ(0=up,1=down)")]
    [SerializeField] Button[] soundPopButtons;

    [Header("着せ替えポップアップ")]
    [SerializeField] Button[] clothPopUpButtons;

    [Header("着せ替えポップダウン")]
    [SerializeField] Button clothPopDownButton;

    //シーン遷移ボタンの取得
    [Header("シーン遷移ボタン")]
    [SerializeField] Button[] sceneButtons;

    private void Start()
    {
        //パネル遷移に関する部分
        clothBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 0));
        ToClothButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 0));
        ToScoreButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 1));
        scoreBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 1));

        //ポップに関する部分
        soundPopButtons[0].onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Sound));
        soundPopButtons[1].onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Sound));

        foreach(Button button in clothPopUpButtons)
        {
            button.onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Cloth));
        }
        clothPopDownButton.onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Cloth));

        //シーン遷移系に関する部分
        for(int i = 0; i < sceneButtons.Length; i++)
        {
            int inputNumber = i;
            sceneButtons[i].onClick.AddListener(() => systemAction.SceneMoveStarter(inputNumber));
        }
    }

    /// <summary>
    /// エディタ上実行関数
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
