using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinManager : MonoBehaviour
{
    [Header("コインオブジェクト")]
    [SerializeField] GameObject coin;
    [Header("初めに生成しておくコインの枚数")]
    [SerializeField] int InitialCoinAmount;
    [Header("コインを生成する間隔")]
    [SerializeField] int coinCreateInterval;

    [Header("コインプール群の親オブジェクト")]
    [SerializeField] GameObject coinsParent;

    [SerializeField] CoinInformation coinInfo;

    [SerializeField] CoinPool coinPool;

    private int createCounter;

    public bool porzBool;
    private void Awake()
    {
        coinPool.CoinInformationInput(coin);
        CoinValueChanger(1);
    }
    private void Update()
    {
        if (porzBool) { return; }
        createCounter++;
        if (createCounter > coinCreateInterval)
        {
            coinPool.GetGameObject().transform.parent = coinsParent.transform;
            createCounter = 0;
        }
    }

    //CoinPoolに関する外部からの呼び出し関数
    public void CoinPoolReturn(GameObject returnObj)
    {
        coinPool.ReleaseGameObject(returnObj);
    }

    //CoinInformationに関する外部からの呼び出し関数
    public void CoinValueChanger(int value)
    {
        coinInfo.CoinValue = value;
    }
}
