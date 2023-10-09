using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// あらゆる計算を行うscript
/// </summary>
public class Calculation : MonoBehaviour
{
    //縦と横の跳ね返り倍率を計算して返す
    public (float, float) BouncePowerCalculation(Vector2 positionDifference)
    {
        float total = Mathf.Abs(positionDifference.x) + Mathf.Abs(positionDifference.y);
        return (positionDifference.x / total, positionDifference.y / total);
    }
}
