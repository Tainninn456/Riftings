using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// コインの値を保持するのみの関数
/// </summary>
public class CoinInformation : MonoBehaviour
{
    private int coinValue;
    public int CoinValue
    {
        get { return coinValue; }
        set { coinValue = value; }
    }
}
