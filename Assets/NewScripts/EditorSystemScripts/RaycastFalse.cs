#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class RaycastFalse : MonoBehaviour
{
    // �{�^��������� Raycast �� false �ɂ��Ȃ�   
    [MenuItem("Tools/SetRaycastDisableNoButton")]
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

    // Raycast �� true �ɂ���   
    [MenuItem("Tools/SetRaycastEnable")]
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