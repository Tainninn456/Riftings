using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �G�f�B�^���tag��ύX����
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


    //Image�̓R���|�[�l���g���K�w�ŏd�Ȃ�\�������邽��root�I�u�W�F�N�g�̂ݎ擾
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

    //�g�p���ӁFtag�����Z�b�g
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
