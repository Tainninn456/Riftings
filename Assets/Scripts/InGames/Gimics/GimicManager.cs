using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ギミックに関する処理を管理するクラス
/// </summary>
public class GimicManager : MonoBehaviour
{
    [Header("ギミックの頻度")]
    [SerializeField] int gimicPoint;

    [Header("コイン発生頻度の変更値")]
    [SerializeField] int frequencyValue;

    [Header("プレイヤーの参照")]
    [SerializeField] Player playerReference;

    [Header("ボールの参照")]
    [SerializeField] Ball ballReference;

    [Header("ボールのトリガー参照")]
    [SerializeField] ballTrigger ballTriggerReference;

    [Header("ゲーム内データの参照")]
    [SerializeField] InGameStockData gameDatas;

    [Header("ルーレットの参照")]
    [SerializeField] Rouletter roulette;

    [Header("コインマネージャーの参照")]
    [SerializeField] coinManager coinManagerReference;

    [Header("ギミックスプライト")]
    [SerializeField] Sprite[] gimicSprites;

    [Header("実行中ギミックを表示するディスプレイ")]
    [SerializeField] Image gimicDisplay;

    //ギミック実行までのフレーム経過を保持
    private int gimicCounter;

    //実行するギミックの番号を保持
    private int gimicNumber;

    //プラスマイナスコインの配置に関するbool値を保持
    private bool gimicCoinning;

    //乱気流発生ギミックに関するbool値を保持
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
            GimicReseter();
            gimicNumber = Random.Range(0, 7);
            //ギミック実行命令
            switch(gimicNumber)
            {
                case 0:
                    //横方向に突風を吹かせるギミック
                    WideWindowGimic();
                    break;
                case 1:
                    //ボールの重力を変更するギミック
                    GravityChangerGimic();
                    break;
                case 2:
                    //ランダムにボールに対して風の力を与えるギミック
                    RandomWindowGimic(true);
                    break;
                case 3:
                    //プレイヤーを大きくするギミック
                    PlayerScaleChangerGimic(1);
                    break;
                case 4:
                    //プレイヤーを小さくするギミック
                    PlayerScaleChangerGimic(2);
                    break;
                case 5:
                    //壁の反射率を変更するギミック
                    WallChangerGimic();
                    break;
                case 6:
                    //コインの発生頻度を変更するギミック
                    CoinFrequenceGimic();
                    break;
            }
            RouletteCoinGimic();
            gimicDisplay.sprite = gimicSprites[gimicNumber];
            gimicCounter = 0;
        }
        if (randGimic) { ballReference.WindRandomAttackGimic(); }
    }

    /// <summary>
    /// デフォルトギミック
    /// </summary>

    //横方向の風ギミック
    private void WideWindowGimic()
    {
        ballTriggerReference.WideWindGimicStarter(true);
    }

    //重力変更ギミック
    private void GravityChangerGimic()
    {
        ballReference.GravityChanger(1);
    }

    //乱気流ギミック
    private void RandomWindowGimic(bool randStart)
    {
        randGimic = randStart;
    }

    //プレイヤーの大きさ変更ギミック
    private void PlayerScaleChangerGimic(int scaleDirection)
    {
        playerReference.ScaleChanger(scaleDirection);
    }

    //壁性質変化ギミック
    private void WallChangerGimic()
    {
        ballReference.WallGimicStarter(true);
    }

    //コイン発生頻度変更ギミック
    private void CoinFrequenceGimic()
    {
        coinManagerReference.CoinFrequencyChange(frequencyValue, true);
    }

    //ギミックによって変更された値等を元に戻す関数
    private void GimicReseter()
    {
        ballTriggerReference.WideWindGimicStarter(false);
        ballReference.WallGimicStarter(false);
        ballReference.GravityChanger(2);
        playerReference.ScaleChanger(0);
        RandomWindowGimic(false);
        coinManagerReference.CoinFrequencyChange(0, false);
    }

    /// <summary>
    /// コインによるギミック
    /// </summary>

    //コインの値を変更するギミック
    private void CoinAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                coinManagerReference.CoinValueChanger(2 * gameDatas.coinMultiplication);
                break;
            case 1:
                coinManagerReference.CoinValueChanger(-1);
                break;
            case 2:
                coinManagerReference.CoinValueChanger(1 * gameDatas.coinMultiplication);
                break;
        }
    }

    //キック時の値を変更するギミック
    private void KickAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                ballReference.KickAddValueChanger(2);
                break;
            case 1:
                ballReference.KickAddValueChanger(-1);
                break;
            case 2:
                ballReference.KickAddValueChanger(1);
                break;
        }
    }

    //ボールのスケールを変更するギミック
    private void BallScaleGimic(int actionType)
    {
        ballReference.BallScaleChanger(actionType);
    }

    //プラスマイナスコインを発生させる関数
    private void RouletteCoinGimic()
    {
        if (!gimicCoinning)
        {
            gimicCoinning = true;
            coinManagerReference.GimicCoin(1, true);
            coinManagerReference.GimicCoin(2, true);
        }
    }

    //ルーレットをスタートする関数
    public void RouletteStarter(int actionType)
    {
        roulette.RouletteStart(actionType);
    }

    //ルーレットの最終値によって実行する関数。actionNumber=4種類の内1種類を決定、actionType=プラスかマイナスか
    public void RouletteDecision(int actionNumber, int actionType)
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
        gimicCoinning = false;
    }

    //ルーレットにて変更したモノをリセットする関数
    public void RouletteReset()
    {
        CoinAmountGimic(2);
        KickAmountGimic(2);
        ballReference.BallScaleChanger(2);
    }
}
