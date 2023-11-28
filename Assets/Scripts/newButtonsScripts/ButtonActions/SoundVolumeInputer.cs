using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// サウンド系のボタンを保持し、関数を割り当てているクラス
/// </summary>
public class SoundVolumeInputer : InputParent
{
    //ContextMenuにて使用する関数のstring名
    const string methodBGMName = "BGMButtonGetter";
    const string methodSEName = "SEButtonGetter";

    [Header("SoundActionクラスの参照")]
    [SerializeField] SoundVolumeAction soundAction;

    [Header("0=up、1=downとし、それぞれBGMとSEの調整を行うボタンを保持")]
    [SerializeField] Button[] BGMButtons = new Button[0];
    [SerializeField] Button[] SEButtons = new Button[0];

    private void Start()
    {
        //各ボタンに対して、ボリューム変更に関するSoundActionクラス内の関数を割り当てる
        BGMButtons[0].onClick.AddListener(() => soundAction.VolumeUp(SoundVolumeAction.SoundType.BGM));
        BGMButtons[1].onClick.AddListener(() => soundAction.VolumeDown(SoundVolumeAction.SoundType.BGM));
        SEButtons[0].onClick.AddListener(() => soundAction.VolumeUp(SoundVolumeAction.SoundType.SE));
        SEButtons[1].onClick.AddListener(() => soundAction.VolumeDown(SoundVolumeAction.SoundType.SE));
    }
#if UNITY_EDITOR

    /// <summary>
    /// エディタ上実行関数
    /// </summary>
    /// 

    //BGMのボリュームを変更するボタンを取得する関数
    [ContextMenu(methodBGMName)]
    private void BGMButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref BGMButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            BGMButtons[i] = buttonArray[i];
        }
    }

    //SEのボリュームを変更するボタンを取得する関数
    [ContextMenu(methodSEName)]
    private void SEButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref SEButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            SEButtons[i] = buttonArray[i];
        }
    }
#endif
}
