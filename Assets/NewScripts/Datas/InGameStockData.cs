using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ゲームプレイ中に一時的に値を保持しておくクラス
/// </summary>
public class InGameStockData: MonoBehaviour
{
    //キックのカウントを保持
    [HideInInspector]
    public int kickCount;
    //コイン獲得総数を保持
    [HideInInspector]
    public int coinCount;
    //プラスコインの取得回数を保持
    [HideInInspector]
    public int plusCoinCount;
    //マイナスコインの取得回数を保持
    [HideInInspector]
    public int minusCoinCount;
    //ハートの数を保持
    [HideInInspector]
    public int heartAmount;
    //コインのレベルに応じたコインの値を保持
    [HideInInspector]
    public int coinMultiplication = 1;
    //ゲームオーバー時のbool値を保持
    [HideInInspector]
    public bool GameOver;
    //ギミック実行カウントを進めるかのbool値を保持
    [HideInInspector]
    public bool GimicCalculating = true;
}