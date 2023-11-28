#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

/// <summary>
/// 負荷の重いRaycastTargetを必要の無いものに関して全てオフにするためのクラス
/// </summary>
public class RaycastFalse : MonoBehaviour
{
    //下記にて使用する関数のstring名
    const string raycastOnMethod = "Tools/SetRaycastDisableNoButton";
    const string raycastOffMethod = "Tools/SetRaycastEnable";

    //RaycastTargetをオフにする関数
    // ボタンがあれば Raycast を false にしない   
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

    // Raycastをオンにする関数  
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