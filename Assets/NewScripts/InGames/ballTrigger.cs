using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";

    [Header("コインマネージャー")]
    [SerializeField] coinManager cMane;
    [Header("コインの取得数を表示するテキスト")]
    [SerializeField] TextMeshProUGUI coinTex;

    [Header("データ系へのアクセス")]
    [SerializeField] GameObject managerInformation;

    [Header("テキストへのアクセス")]
    [SerializeField] TextAction textAction;

    InGameStockData parentGameStockData;
    coinManager coinMane;
    CoinInformation coinInfo;

    private void Start()
    {
        parentGameStockData = managerInformation.GetComponent<InGameStockData>();
        coinMane = managerInformation.GetComponent<coinManager>();
        coinInfo = managerInformation.GetComponent<CoinInformation>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(coinTagName))
        {
            coinMane.CoinPoolReturn(collision.gameObject);
            parentGameStockData.coinCount += coinInfo.CoinValue;
            textAction.CoinCountDisplay(parentGameStockData.coinCount);
        }
    }
}
