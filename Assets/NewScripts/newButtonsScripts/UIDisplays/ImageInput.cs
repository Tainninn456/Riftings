using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageInput : InputParent
{
    const string methodClothMoneyName = "ClothChangeButtonsGetter";

    [SerializeField] ImageAction imageAction;

    [SerializeField] Button[] clothChangeButtons = new Button[0];
    [ContextMenu(methodClothMoneyName)]
    private void ClothChangeButtonsGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref clothChangeButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            clothChangeButtons[i] = buttonArray[i];
        }
    }
}
