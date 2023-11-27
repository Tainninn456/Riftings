using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// データ変更を伴うボタンを保持し、関数を割り当てているクラス
/// </summary>
public class DataInputer : InputParent
{
    //ContextMenuでそれぞれ使用する関数のstring名
    const string methodClothMoneyName = "ClothButtonGetter";
    const string methodItemMoneyName = "ItemButtonGetter";
    const string methodConsomeName = "ConsumeButtonGetter";

    //スポーツの種類数
    const int sportTypeAmount = 9;

    [Header("DataActionクラスの参照")]
    [SerializeField] DataAction dataAction;

    [Header("着せ替えを行うスポーツを選択するボタンを保持")]
    [SerializeField] Button[] clothMoneyConsumeSelectButtons;
    [Header("購入を確定するボタンを保持")]
    [SerializeField] Button[] consumeDecisionButtons;
    [Header("0=コイン、1=ハートとし、各レベルを上昇するボタンを保持")]
    [SerializeField] Button[] itemMoneyConsumeButtons;

    private void Start()
    {
        //ボタンに対して選択処理や消費処理を実行するDataActionクラス内の関数を割り当てる
        for(int i = 0; i < sportTypeAmount; i++)
        {
            var input = i;
            clothMoneyConsumeSelectButtons[i].onClick.AddListener(() => dataAction.ClothDesicion(input));
            consumeDecisionButtons[i].onClick.AddListener(() => dataAction.ClothConsume(input));
        }
        itemMoneyConsumeButtons[0].onClick.AddListener(() => dataAction.ItemConsume(0));
        itemMoneyConsumeButtons[1].onClick.AddListener(() => dataAction.ItemConsume(1));
    }
#if UNITY_EDITOR

    /// <summary>
    /// エディタ上実行関数
    /// </summary>
    /// 
    
    //各スポーツの着せ替え購入画面を表示するボタンを取得する関数
    [ContextMenu(methodClothMoneyName)]
    private void ClothButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref clothMoneyConsumeSelectButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            clothMoneyConsumeSelectButtons[i] = buttonArray[i];
        }
    }

    //コインレベル、ハートレベルのボタンを取得する関数
    [ContextMenu(methodItemMoneyName)]
    private void ItemButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref itemMoneyConsumeButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            itemMoneyConsumeButtons[i] = buttonArray[i];
        }
    }

    //着せ替え購入ボタンの取得をする関数
    [ContextMenu(methodConsomeName)]
    private void ConsumeButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref consumeDecisionButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            consumeDecisionButtons[i] = buttonArray[i];
        }
    }
#endif
}
