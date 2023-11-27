#if UNITY_EDITOR
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
    //ContextMenu�ɂĎ擾����R���|�[�l���g�𖾎����邽�߂�tag��
    const string textActiveTagName = "ActiveObject";
    //ContextMenu�ɂĎ擾����Ώۂ���O���ۂɎg�p����
    const string tagResetName = "Untagged";

    //Tools����g�p����ۂ̖��O
    const string textTagMethod = "Tools/TagChangeToActiveForText";
    const string imageTagMethod = "Tools/TagChangeToActiveForImage";
    const string buttonTagMethod = "Tools/TagChangeToActiveForButton";
    const string tagReseterMethod = "Tools/DangerTagReseter";

    //�擾����e�L�X�g��ActiveObject�^�O��t�^����֐�
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

    //�擾����C���[�W��ActiveObject�^�O��t�^����֐�
    //�C���[�W�̓R���|�[�l���g���K�w�ŏd�Ȃ�\�������邽��root�I�u�W�F�N�g�̂ݎ擾
    [MenuItem(imageTagMethod)]
    private static void TagChangeToActiveForImageParent()
    {
        foreach (var rootGameObject in Selection.gameObjects)
        {
            rootGameObject.gameObject.tag = textActiveTagName;
        }
    }

    //�擾����{�^����ActiveObject�^�O��t�^����֐�
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

    //�g�p���ӁF�^�O�����Z�b�g����֐�
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