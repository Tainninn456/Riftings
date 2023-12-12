using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// Inputer系の親クラス、Button取得のContextMenuでの関数を保持
/// </summary>
public class InputParent : MonoBehaviour
{
    //ボタンコンポーネントを持つオブジェクトのタグを変更する関数
#if UNITY_EDITOR
    const string ActiveTagName = "ActiveObject";
    protected virtual Button[] ButtonGetter()
    {
        Button[] returnButtons = new Button[0];
        List<Button> buttons = new List<Button>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<Button>(true);
            foreach (Button ob in children)
            {
                if (ob.tag != ActiveTagName) { continue; }
                Debug.Log(ob.name);
                buttons.Add(ob);
            }
        }
        Array.Resize<Button>(ref returnButtons, buttons.Count);
        for (int i = 0; i < buttons.Count; i++)
        {
            returnButtons[i] = buttons[i];
        }
        return returnButtons;
    }
#endif
}
