using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageInput : InputParent
{
    //ContextMenuで使用する関数のstring名
    const string methodClothMoneyName = "ClothChangeButtonsGetter";

    [Header("ImageActionクラスの参照")]
    [SerializeField] ImageAction imageAction;

    [Header("着せ替え購入にて使用するボタン")]
    [SerializeField] Button[] clothChangeButtons;

    private void Start()
    {
        //着せ替え購入ボタンのそれぞれにImageActionクラス内の関数を呼び出すように割り当てる
        for(int i = 0; i < clothChangeButtons.Length; i++)
        {
            int indexNumber = i;
            clothChangeButtons[i].onClick.AddListener(() => imageAction.clothButtonImageChanger(indexNumber));
        }
    }
#if UNITY_EDITOR
    //着せ替え購入の際に使用するボタンのコンポーネントを取得する関数
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
#endif
}
