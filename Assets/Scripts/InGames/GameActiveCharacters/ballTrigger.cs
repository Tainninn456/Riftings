using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ボールのTrigger用クラス
/// </summary>
public class ballTrigger : MonoBehaviour
{
    //OnTrigger実行時に判別するタグ名を定数で保持
    const string coinTagName = "coin";
    const string coinPlusTagName = "pluscoin";
    const string coinMinusTagName = "minuscoin";
    const string gimicTagName = "Gimic";

    [Header("ボールの参照")]
    [SerializeField] Ball parentBoalScript;

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
            AudioManager.Instance.PlaySE(AudioManager.SE.coin);
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
            AudioManager.Instance.PlaySE(AudioManager.SE.coin);
            coinMane.GimicCoin(1, false);
        }
        //マイナスコイン衝突時の処理
        else if (collision.gameObject.CompareTag(coinMinusTagName))
        {
            AudioManager.Instance.PlaySE(AudioManager.SE.coin);
            coinMane.GimicCoin(2, false);
        }
    }

    //壁衝突時の処理を変更する関数
    public void WideWindGimicStarter(bool gimicBool)
    {
        GimicBool = gimicBool;
    }
}
