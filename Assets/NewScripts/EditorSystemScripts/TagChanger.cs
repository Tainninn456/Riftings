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
    const string tagResetName = "Untagged";
    const string textActiveTagName = "ActiveObject";

    const string textTagMethod = "Tools/TagChangeToActiveForText";
    const string imageTagMethod = "Tools/TagChangeToActiveForImage";
    const string buttonTagMethod = "Tools/TagChangeToActiveForButton";

    const string tagReseter = "Tools/DangerTagReseter";

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


    //Imageはコンポーネントが階層で重なる可能性があるためrootオブジェクトのみ取得
    [MenuItem(imageTagMethod)]
    private static void TagChangeToActiveForImageParent()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            rootGameObject.gameObject.tag = textActiveTagName;
        }
    }

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

    //使用注意：tagをリセット
    [MenuItem(tagReseter)]
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
