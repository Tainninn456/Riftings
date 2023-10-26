using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    public int[] GameScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int Heart;
    public int CoinLevel = 1;
    public int CoinAmount = 0;
    public int chainLevel = 8;
    public int[] cloths = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int[] nowCloth = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int SEVolume = 2;
    public int BGMVolume = 2;

    //着せ替えをどこまでアンロックしているか
    public int[] clothAchive = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //コインのアンロックレベル
    public int coinLevel = 1;
    //ハートのアンロックレベル
    public int heartLevel = 1;
    //インデックスに対応した着せ替えの内容
    public int[] sportCloth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //ゲームの記録
    public int[] PlayScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}
