using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DataInputer : InputParent
{
    const string methodClothMoneyName = "ClothButtonGetter";
    const string methodItemMoneyName = "ItemButtonGetter";
    const string methodConsomeName = "ConsumeButtonGetter";

    const int sportTypeAmount = 9;

    [SerializeField] DataAction dataAction;

    [SerializeField] Button[] clothMoneyConsumeSelectButtons = new Button[0];
    [SerializeField] Button[] consumeDesicionButtons = new Button[0];
    [Header("0=コイン、1=ハート")]
    [SerializeField] Button[] itemMoneyConsumeButtons = new Button[0];

    private void Start()
    {
        for(int i = 0; i < sportTypeAmount; i++)
        {
            var input = i;
            clothMoneyConsumeSelectButtons[i].onClick.AddListener(() => dataAction.ClothDesicion(input));
            consumeDesicionButtons[i].onClick.AddListener(() => dataAction.ClothConsume(input));
        }
        itemMoneyConsumeButtons[0].onClick.AddListener(() => dataAction.ItemConsume(0));
        itemMoneyConsumeButtons[1].onClick.AddListener(() => dataAction.ItemConsume(1));
    }

    /// <summary>
    /// エディタ上実行関数
    /// </summary>
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

    [ContextMenu(methodConsomeName)]
    private void ConsumeButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref consumeDesicionButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            consumeDesicionButtons[i] = buttonArray[i];
        }
    }
}
