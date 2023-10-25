using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TextInput : InputParent
{
    const string methodClothMoneyName = "ClothButtonGetter";
    const string methodItemMoneyName = "ItemButtonGetter";

    [SerializeField] TextAction textAction;

    [SerializeField] Button[] clothMoneyConsumeButtons = new Button[0];
    [Header("0=コイン、1=ハート")]
    [SerializeField] Button[] itemMoneyConsumeButtons = new Button[0];

    [ContextMenu(methodClothMoneyName)]
    private void ClothButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref clothMoneyConsumeButtons, buttonArray.Length);
        for(int i = 0; i < buttonArray.Length; i++)
        {
            clothMoneyConsumeButtons[i] = buttonArray[i];
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
}
