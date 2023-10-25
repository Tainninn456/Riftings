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

    public int[] clothAchive = new int[9];
    public int coinLevel = 1;
    public int heartLevel = 1;
}
