using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimicManager : MonoBehaviour
{
    [Header("ギミックの頻度")]
    [SerializeField] int gimicPoint;

    [Header("プレイヤーの参照")]
    [SerializeField] Newplayer playerReference;

    [Header("ボールの参照")]
    [SerializeField] Newboal boalReference;

    [Header("ボールのトリガー参照(当たった時の処理はこっちの方が多いかも?)")]
    [SerializeField] ballTrigger ballTriggerReference;

    [Header("ゲーム内データの参照")]
    [SerializeField] InGameStockData gameDatas;

    [Header("ルーレット")]
    [SerializeField] Rouleter roulette;

    [Header("コインマネージャー")]
    [SerializeField] coinManager cMane;

    [Header("ギミックスプライト")]
    [SerializeField] Sprite[] gimicSprites;

    [Header("ギミックを表示するディスプレイ")]
    [SerializeField] Image gimicDisplay;

    private int gimicCounter;

    private int gimicNumber;

    private bool gimicCoinning;
    private bool randGimic;

    private void FixedUpdate()
    {
        if (!gameDatas.GimicCalculating) { return; }
        gimicCounter++;
    }
    private void Update()
    {
        if(gimicCounter > gimicPoint)
        {
            GimicResetter();
            gimicNumber = Random.Range(0, 6);
            switch(gimicNumber)
            {
                case 0:
                    //横方向の風
                    WideWindowGimic();
                    break;
                case 1:
                    //ボールの重力を変更
                    GravityChangerGimic();
                    break;
                case 2:
                    //ランダムに風でボールを動かす
                    RandomWindowGimic(true);
                    break;
                case 3:
                    //大きい
                    PlayerScaleChangerGimic(1);
                    break;
                case 4:
                    //小さい
                    PlayerScaleChangerGimic(2);
                    break;
                case 5:
                    WallChangerGimic();
                    break;
            }
            RouletteCoinGimic();
            gimicDisplay.sprite = gimicSprites[gimicNumber];
            gimicCounter = 0;
        }
        if (randGimic) { boalReference.WindRandomAttackGimic(); }
    }
    private void WideWindowGimic()
    {
        ballTriggerReference.WallGimicStarter(true);
    }

    private void GravityChangerGimic()
    {
        boalReference.GravityChanger(1);
    }

    private void RandomWindowGimic(bool randStart)
    {
        randGimic = randStart;
    }

    private void PlayerScaleChangerGimic(int scaleDirection)
    {
        playerReference.ScaleChanger(scaleDirection);
    }

    private void WallChangerGimic()
    {
        boalReference.WallGimicStarter(true);
    }

    private void RouletteCoinGimic()
    {
        if (!gimicCoinning)
        {
            gimicCoinning = true;
            cMane.GimicCoin(1, true);
            cMane.GimicCoin(2, true);
        }
    }

    private void CoinAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                cMane.CoinValueChanger(2 * gameDatas.coinMultiplication);
                break;
            case 1:
                cMane.CoinValueChanger(-1);
                break;
            case 2:
                cMane.CoinValueChanger(1 * gameDatas.coinMultiplication);
                break;
        }
    }

    private void KickAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                boalReference.KickAddValueChanger(2);
                break;
            case 1:
                boalReference.KickAddValueChanger(-1);
                break;
            case 2:
                boalReference.KickAddValueChanger(1);
                break;
        }
    }

    private void BallScaleGimic(int actionType)
    {
        boalReference.BallScaleChanger(actionType);
    }
    private void GimicResetter()
    {
        ballTriggerReference.WallGimicStarter(false);
        boalReference.WallGimicStarter(false);
        boalReference.GravityChanger(2);
        playerReference.ScaleChanger(0);
        RandomWindowGimic(false);
    }

    public void RouletStarter(int actionType)
    {
        roulette.RouletteStart(actionType);
        gimicCoinning = false;
    }

    //actionNumber=4種類の内1種類を決定、actionType=プラスかマイナスか
    public void RouletteDesicion(int actionNumber, int actionType)
    {
        switch (actionNumber)
        {
            case 0:
                CoinAmountGimic(actionType);
                break;
            case 1:
                KickAmountGimic(actionType);
                break;
            case 2:
                BallScaleGimic(actionType);
                break;
        }
    }

    //ルーレット内容物をリセット
    public void RouletteReset()
    {
        CoinAmountGimic(2);
        KickAmountGimic(2);
        boalReference.BallScaleChanger(2);
    }
}
