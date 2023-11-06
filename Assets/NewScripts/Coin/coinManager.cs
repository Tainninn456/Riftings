using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinManager : MonoBehaviour
{
    [Header("コインオブジェクト")]
    [SerializeField] GameObject coin;
    [Header("プラスコインオブジェクト")]
    [SerializeField] GameObject plusCoin;
    [Header("マイナスコインオブジェクト")]
    [SerializeField] GameObject minusCoin;
    [Header("初めに生成しておくコインの枚数")]
    [SerializeField] int InitialCoinAmount;
    [Header("コインを生成する間隔")]
    [SerializeField] int coinCreateInterval;

    [Header("コインプール群の親オブジェクト")]
    [SerializeField] GameObject coinsParent;

    [Header("ゲーム中のデータへのアクセス")]
    [SerializeField] InGameStockData gameDatas;

    [Header("ギミックマネージャーへのアクセス")]
    [SerializeField] GimicManager gMane;

    [SerializeField] CoinInformation coinInfo;

    [SerializeField] CoinPool coinPool;

    private int createCounter;

    private bool gimicCoinning;

    private float[] gimicCoinCreatePositions = new float[4];

    [HideInInspector]
    public bool porzBool;
    private void Awake()
    {
        coinPool.CoinInformationInput(coin);
    }
    private void Update()
    {
        if (porzBool || gameDatas.GameOver) { return; }
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

    public void GimicCoinPositionsGetter(float xunder, float xover, float yunder, float yover)
    {
        gimicCoinCreatePositions[0] = xunder;
        gimicCoinCreatePositions[1] = xover;
        gimicCoinCreatePositions[2] = yunder;
        gimicCoinCreatePositions[3] = yover;
    }

    //プラスとマイナスのコインに関する関数
    public void GimicCoin(int gimicCoinIndex, bool createPoint)
    {
        Transform coinTra = null;
        switch (gimicCoinIndex)
        {
            case 1:
                gameDatas.plusCoinCount++;
                coinTra = plusCoin.GetComponent<Transform>();
                break;
            case 2:
                gameDatas.minusCoinCount++;
                coinTra = minusCoin.GetComponent<Transform>();
                break;
        }
        coinTra.position = new Vector2(Random.Range(gimicCoinCreatePositions[0], gimicCoinCreatePositions[1]), Random.Range(gimicCoinCreatePositions[2], gimicCoinCreatePositions[3]));
        if (createPoint)
        {
            plusCoin.SetActive(true);
            minusCoin.SetActive(true);
        }
        
        if (gameDatas.plusCoinCount > 7)
        {
            gameDatas.plusCoinCount = 0;
            gameDatas.minusCoinCount = 0;
            plusCoin.SetActive(false);

            gMane.RouletStarter(0);
        }
        else if(gameDatas.minusCoinCount > 7)
        {
            gameDatas.plusCoinCount = 0;
            gameDatas.minusCoinCount = 0;
            minusCoin.SetActive(false);

            gMane.RouletStarter(1);
        }
    }
}
