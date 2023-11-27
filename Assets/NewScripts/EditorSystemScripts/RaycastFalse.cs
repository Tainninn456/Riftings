#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// ���ׂ̏d��RaycastTarget��K�v�̖������̂Ɋւ��đS�ăI�t�ɂ��邽�߂̃N���X
/// </summary>
public class RaycastFalse : MonoBehaviour
{
    //���L�ɂĎg�p����֐���string��
    const string raycastOnMethod = "Tools/SetRaycastDisableNoButton";
    const string raycastOffMethod = "Tools/SetRaycastEnable";

    //RaycastTarget���I�t�ɂ���֐�
    // �{�^��������� Raycast �� false �ɂ��Ȃ�   
    [MenuItem(raycastOnMethod)]
    private static void SetRaycastDisableNoButton()
    {
        foreach (var obj in Selection.gameObjects)
        {
            var graphics = obj.GetComponentsInChildren<Graphic>(true);
            foreach (var graphic in graphics)
            {
                if (graphic.GetComponent<Button>() != null)
                    continue;

                graphic.raycastTarget = false;
            }
        }
    }

    // Raycast���I���ɂ���֐�  
    [MenuItem(raycastOffMethod)]
    private static void SetRaycastEnable()
    {
        foreach (var obj in Selection.gameObjects)
        {
            var graphics = obj.GetComponentsInChildren<Graphic>(true);
            foreach (var graphic in graphics)
            {
                graphic.raycastTarget = true;
            }
        }
    }
}
#endif