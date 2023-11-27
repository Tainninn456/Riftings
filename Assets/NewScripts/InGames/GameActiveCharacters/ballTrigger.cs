using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ボールのOnTrigger用script
/// </summary>
public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";
    const string coinPlusTagName = "pluscoin";
    const string coinMinusTagName = "minuscoin";
    const string gimicTagName = "Gimic";

    [Header("コインの取得数を表示するテキスト")]
    [SerializeField] TextMeshProUGUI coinTex;

    [Header("ボールの参照")]
    [SerializeField] NewBall parentBoalScript;

    [Header("データ系へのアクセス")]
    [SerializeField] GameObject managerInformation;

    [Header("テキストへのアクセス")]
    [SerializeField] TextAction textAction;

    [Header("イメージへのアクセス")]
    [SerializeField] ImageAction imageAction;

    //インゲームプレイ中データ、コインの管理クラス、コインの情報クラスへのアクセス
    InGameStockData parentGameStockData;
    coinManager coinMane;
    CoinInformation coinInfo;

    //ギミックを実行するかを判断
    private bool GimicBool;
    private void Start()
    {
        parentGameStockData = managerInformation.GetComponent<InGameStockData>();
        coinMane = managerInformation.GetComponent<coinManager>();
        coinInfo = managerInformation.GetComponent<CoinInformation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //コイン衝突時の処理
        if (collision.gameObject.CompareTag(coinTagName))
        {
            AudioManager.instance.PlaySE(AudioManager.SE.coin);
            coinMane.CoinPoolReturn(collision.gameObject);
            parentGameStockData.coinCount += coinInfo.CoinValue;
            textAction.CoinCountDisplay(parentGameStockData.coinCount);
        }
        //横方向の風処理
        else if (collision.gameObject.CompareTag(gimicTagName))
        {
            if (GimicBool)
            {
                parentBoalScript.WindAttackGimic();
            }
        }
        //プラスコイン衝突時の処理
        else if (collision.gameObject.CompareTag(coinPlusTagName))
        {
            AudioManager.instance.PlaySE(AudioManager.SE.coin);
            coinMane.GimicCoin(1, false);
        }
        //マイナスコイン衝突時の処理
        else if (collision.gameObject.CompareTag(coinMinusTagName))
        {
            AudioManager.instance.PlaySE(AudioManager.SE.coin);
            coinMane.GimicCoin(2, false);
        }
    }

    public void WallGimicStarter(bool gimicBool)
    {
        GimicBool = gimicBool;
    }
}
