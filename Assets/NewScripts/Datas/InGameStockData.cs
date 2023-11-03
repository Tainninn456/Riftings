using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameStockData: MonoBehaviour
{
    [HideInInspector]
    public int kickCount;
    [HideInInspector]
    public int coinCount;
    [HideInInspector]
    public int plusCoinCount;
    [HideInInspector]
    public int minusCoinCount;
    [HideInInspector]
    public int heartAmount;
    [HideInInspector]
    public bool GameOver;
    [HideInInspector]
    public bool AnimationEnd;
}