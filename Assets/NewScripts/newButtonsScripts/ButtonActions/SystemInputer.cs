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
    //複数のオブジェクトと階層で被っているためシリアライズに入れ込む

    [SerializeField] SystemAction systemAction;

    [Header("メインメニュー")]
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
    [Header("サウンドポップ(0=up,1=down)&(インゲームでも使用)")]
    [SerializeField] Button[] soundPopButtons;

    [Header("着せ替えポップアップ")]
    [SerializeField] Button[] clothPopUpButtons;

    [Header("着せ替えポップダウン")]
    [SerializeField] Button clothPopDownButton;

    //シーン遷移ボタンの取得
    [Header("シーン遷移ボタン(インゲームでも使用,0=リロード,1=メニューに遷移、ポーズとリザルトの順番で保持)")]
    [SerializeField] Button[] sceneButtons;

    [Header("インゲーム")]
    [Header("ポーズポップ(0=up,1=down)")]
    [SerializeField] Button[] porzPopButtons;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            //パネル遷移に関する部分
            clothBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 0));
            ToClothButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 0));
            ToScoreButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 1));
            scoreBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 1));

            foreach (Button button in clothPopUpButtons)
            {
                button.onClick.AddListener(() => systemAction.PopUp(SystemAction.PopName.Cloth));
            }
            clothPopDownButton.onClick.AddListener(() => systemAction.PopDown(SystemAction.PopName.Cloth));

            //シーン遷移系に関する部分
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
