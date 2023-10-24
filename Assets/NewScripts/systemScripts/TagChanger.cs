using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;


public class TagChanger : MonoBehaviour
{
    const string textActiveTagName = "ActiveObject";
    const string menuName = "menuScene";

    [MenuItem("Tools/TagChangeToActiveForText")]
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
}
