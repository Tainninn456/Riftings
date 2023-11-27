using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundInputer : InputParent
{
    const string methodBGMName = "BGMButtonGetter";
    const string methodSEName = "SEButtonGetter";

    [SerializeField] SoundVolumeAction soundAction;

    [Header("0=up、1=down")]
    [SerializeField] Button[] BGMButtons = new Button[0];
    [SerializeField] Button[] SEButtons = new Button[0];

    private void Start()
    {
        BGMButtons[0].onClick.AddListener(() => soundAction.VolumeUp(SoundVolumeAction.SoundType.BGM));
        BGMButtons[1].onClick.AddListener(() => soundAction.VolumeDown(SoundVolumeAction.SoundType.BGM));
        SEButtons[0].onClick.AddListener(() => soundAction.VolumeUp(SoundVolumeAction.SoundType.SE));
        SEButtons[1].onClick.AddListener(() => soundAction.VolumeDown(SoundVolumeAction.SoundType.SE));
    }
#if UNITY_EDITOR

    /// <summary>
    /// エディタ上実行関数
    /// </summary>
    [ContextMenu(methodBGMName)]
    private void ClothButtonGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref BGMButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            BGMButtons[i] = buttonArray[i];
        }
    }

    [ContextMenu(methodSEName)]
    private void ItemButtonGetter()
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
