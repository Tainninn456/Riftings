using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rouleter : MonoBehaviour
{
    [Header("ルーレットに使用する画像(プラスルーレット)")]
    [SerializeField] Sprite[] plusSprites;

    [Header("ルーレットに使用する画像(マイナスルーレット)")]
    [SerializeField] Sprite[] minusSprites;

    [Header("表示するスプライト")]
    [SerializeField] Image rouletteSprite;

    [Header("ギミックマネージャーの参照")]
    [SerializeField] GimicManager gMane;
    //対象の画像群
    private Sprite[] InsertSprites;

    private int nowRouletteType;
    public void RouletteStart(int rouletteType)
    {
        nowRouletteType = rouletteType;
        switch (rouletteType)
        {
            case 0:
                Debug.Log("plus");
                InsertSprites = plusSprites;
                break;
            case 1:
                Debug.Log("minus");
                InsertSprites = minusSprites;
                break;
        }
        StartCoroutine("Roulette");
    }

    IEnumerator Roulette()
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
        gMane.RouletteDesicion(returnNumber, nowRouletteType);
    }
}
