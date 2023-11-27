#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// エディタ上でtagを変更する
/// </summary>
public class TagChanger : MonoBehaviour
{
    //ContextMenuにて取得するコンポーネントを明示するためのtag名
    const string textActiveTagName = "ActiveObject";
    //ContextMenuにて取得する対象から外す際に使用する
    const string tagResetName = "Untagged";

    //Toolsから使用する際の名前
    const string textTagMethod = "Tools/TagChangeToActiveForText";
    const string imageTagMethod = "Tools/TagChangeToActiveForImage";
    const string buttonTagMethod = "Tools/TagChangeToActiveForButton";
    const string tagReseterMethod = "Tools/DangerTagReseter";

    //取得するテキストにActiveObjectタグを付与する関数
    [MenuItem(textTagMethod)]
    private static void TagChangeToActiveForText()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI ob in children)
            {
                ob.gameObject.tag = textActiveTagName;
            }
        }
    }

    //取得するイメージにActiveObjectタグを付与する関数
    //イメージはコンポーネントが階層で重なる可能性があるためrootオブジェクトのみ取得
    [MenuItem(imageTagMethod)]
    private static void TagChangeToActiveForImageParent()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            rootGameObject.gameObject.tag = textActiveTagName;
        }
    }

    //取得するボタンにActiveObjectタグを付与する関数
    [MenuItem(buttonTagMethod)]
    private static void TagChangeToActiveForButton()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<Button>(true);
            foreach (Button ob in children)
            {
                ob.gameObject.tag = textActiveTagName;
            }
        }
    }

    //使用注意：タグをリセットする関数
    [MenuItem(tagReseterMethod)]
    private static void DangerTagReseter()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform ob in children)
            {
                ob.gameObject.tag = tagResetName;
            }
        }
    }
}
#endif