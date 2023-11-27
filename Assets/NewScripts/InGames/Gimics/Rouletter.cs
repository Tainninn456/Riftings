using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ルーレットを担当するクラス
/// </summary>
public class Rouletter : MonoBehaviour
{
    [Header("ルーレットに使用する画像(プラスルーレット)")]
    [SerializeField] Sprite[] plusSprites;

    [Header("ルーレットに使用する画像(マイナスルーレット)")]
    [SerializeField] Sprite[] minusSprites;

    [Header("表示するスプライト")]
    [SerializeField] Image rouletteSprite;

    [Header("ギミックマネージャーの参照")]
    [SerializeField] GimicManager gimicManager;

    //プラスかマイナスのルーレットにて表示するスプライト群を保持する変数
    private Sprite[] InsertSprites;

    //回すルーレットがプラスかマイナスかを保持する変数
    private int nowRouletteType;

    //ルーレットを実行する関数
    public void RouletteStart(int rouletteType)
    {
        nowRouletteType = rouletteType;
        switch (rouletteType)
        {
            case 0:
                InsertSprites = plusSprites;
                break;
            case 1:
                InsertSprites = minusSprites;
                break;
        }
        gimicManager.RouletteReset();
        StartCoroutine("RotationRoulette");
    }

    //ルーレットの非同期処理
    IEnumerator RotationRoulette()
    {
        int returnNumber = 0;
        for (int i = 0; i < 30; i++)
        {
            int rand = Random.Range(0, 3);
            rouletteSprite.sprite = InsertSprites[rand];
            yield return new WaitForSeconds(0.1f);
            if(i == 29)
            {
                returnNumber = rand;
            }
        }
        gimicManager.RouletteDecision(returnNumber, nowRouletteType);
    }
}
