using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


/// <summary>
/// パネル、ポップアップ、シーン遷移等に関するボタンを保持し、関数を割り当てているクラス
/// </summary>
public class SystemInputer : InputParent
{
    //ContextMenuにて使用している関数のstring名
    const string sceneButtonGetName = "SceneButtonGetter";
    const string clothButtonGetName = "ClothButtonGetter";

    const string mainSceneName = "menuScene";
    const string playSceneName = "playScene";

    [Header("SystemActionクラスの参照")]
    [SerializeField] SystemAction systemAction;

    [Header("メインメニュー")]
    //パネル遷移のボタン
    [Header("着せ替えパネル → メインパネルを実行するボタン")]
    [SerializeField] Button clothBackButton;

    [Header("メインパネル → 着せ替えパネルを実行するボタン")]
    [SerializeField] Button ToClothButton;

    [Header("メインパネル → スコアパネルを実行するボタン")]
    [SerializeField] Button ToScoreButton;

    [Header("スコアパネル → メインパネルを実行するボタン")]
    [SerializeField] Button scoreBackButton;

    //ポップアップボタンの取得
    [Header("サウンドポップアップのボタン(0=display,1=hidden)&(インゲームでも使用)")]
    [SerializeField] Button[] soundPopupButtons;

    [Header("着せ替えポップアップ表示ボタン")]
    [SerializeField] Button[] clothPopupDisplayButtons;

    [Header("着せ替えポップアップ非表示ボタン")]
    [SerializeField] Button clothPopupHiddenButton;

    //シーン遷移ボタンの取得
    [Header("シーン遷移ボタン")]
    [SerializeField] Button[] sceneButtons;

    [Header("インゲーム")]
    [Header("ポーズポップアップ(0=display,1=hidden)")]
    [SerializeField] Button[] porzPopupButtons;

    private void Start()
    {
        //メインメニューのシーン内でボタンに対して、SystemActionクラス内の関数を割り当てている
        if (SceneManager.GetActiveScene().name == mainSceneName)
        {
            //パネル遷移に関する部分
            clothBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 0));
            ToClothButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 0));
            ToScoreButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.right, 1));
            scoreBackButton.onClick.AddListener(() => systemAction.PanelMove(SystemAction.MoveDirection.left, 1));

            foreach (Button button in clothPopupDisplayButtons)
            {
                button.onClick.AddListener(() => systemAction.PopupDisplay(SystemAction.PopupName.Cloth));
            }
            clothPopupHiddenButton.onClick.AddListener(() => systemAction.PopupHidden(SystemAction.PopupName.Cloth));

            //シーン遷移系に関する部分
            for (int i = 0; i < sceneButtons.Length; i++)
            {
                int inputNumber = i;
                sceneButtons[i].onClick.AddListener(() => systemAction.SceneMoveStarter(inputNumber));
            }
            soundPopupButtons[0].onClick.AddListener(() => systemAction.PopupDisplay(SystemAction.PopupName.Sound));
            soundPopupButtons[1].onClick.AddListener(() => systemAction.PopupHidden(SystemAction.PopupName.Sound));
        }
        //ゲームプレイのシーン内でボタンに対して、SystemActionクラス内の関数を割り当てている
        else if (SceneManager.GetActiveScene().name == playSceneName)
        {
            porzPopupButtons[0].onClick.AddListener(() => systemAction.PopupDisplay(SystemAction.PopupName.Porz));
            porzPopupButtons[1].onClick.AddListener(() => systemAction.PopupHidden(SystemAction.PopupName.Porz));

            soundPopupButtons[0].onClick.AddListener(() => systemAction.PopupChainDisplay(SystemAction.PopupOperaion.display, SystemAction.PopupName.Sound));
            soundPopupButtons[1].onClick.AddListener(() => systemAction.PopupChainDisplay(SystemAction.PopupOperaion.hidden, SystemAction.PopupName.Sound));

            sceneButtons[0].onClick.AddListener(() => systemAction.InGameSceneMover(0));
            sceneButtons[1].onClick.AddListener(() => systemAction.InGameSceneMover(1));
            sceneButtons[2].onClick.AddListener(() => systemAction.InGameSceneMover(0));
            sceneButtons[3].onClick.AddListener(() => systemAction.InGameSceneMover(1));
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// エディタ上実行関数
    /// </summary>
    /// 

    //シーン遷移を実行するボタンを取得する関数
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

    //着せ替え実行ボタンを取得する関数
    [ContextMenu(clothButtonGetName)]
    private void ClothButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref clothPopupDisplayButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            clothPopupDisplayButtons[i] = buttonArray[i];
        }
    }
#endif
}
