using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// コインに関する操作を行うクラス
/// </summary>
public class coinManager : MonoBehaviour
{
    //各コインのPrefab情報を保持し、ここからscript内で使用する
    [Header("コインオブジェクト")]
    [SerializeField] GameObject coin;
    [Header("プラスコインオブジェクト")]
    [SerializeField] GameObject plusCoin;
    [Header("マイナスコインオブジェクト")]
    [SerializeField] GameObject minusCoin;

    [Header("初めに生成しておくコインの枚数")]
    [SerializeField] int InitialCoinAmount;
    [Header("コインを生成する間隔のデフォルト値")]
    [SerializeField] int coinCreateIntervalDefault;

    [Header("コイン生成場所のx軸最低値")]
    [SerializeField] float coinXposUnder;
    [Header("コイン生成場所のx軸最高値")]
    [SerializeField] float coinXposOver;
    [Header("コイン生成場所のy軸最低値")]
    [SerializeField] float coinYposUnder;
    [Header("コイン生成場所のy軸最高値")]
    [SerializeField] float coinYposOver;

    [Header("コインプール群の親オブジェクト")]
    [SerializeField] GameObject coinsParent;

    [Header("ゲーム中のデータへのアクセス")]
    [SerializeField] InGameStockData gameDatas;

    [Header("ギミックマネージャーへのアクセス")]
    [SerializeField] GimicManager gMane;

    [Header("コイン値の参照")]
    [SerializeField] CoinInformation coinInfo;

    [Header("コインプールクラスの参照")]
    [SerializeField] CoinPool coinPool;

    //フレームの経過を記録
    private int createCounter;

    //コインの発生頻度
    private int coinCreateInterval;

    //外部からコインの生成を止める為の変数
    [HideInInspector]
    public bool porzBool;

    //コインプールに生成するprefabコイン情報を渡し、コインの生成間隔を変数へ代入している
    private void Awake()
    {
        coinPool.CoinInformationInput(coin);
        coinPool.CoinPositionSetter(coinXposUnder, coinXposOver, coinYposUnder, coinYposOver);
        coinCreateInterval = coinCreateIntervalDefault;
    }
    private void Update()
    {
        //ポーズ中かゲームオーバーでなければコイン生成カウントを進める
        if (porzBool || gameDatas.GameOver) { return; }
        createCounter++;
        if (createCounter > coinCreateInterval)
        {
            coinPool.GetGameObject().transform.parent = coinsParent.transform;
            createCounter = 0;
        }
    }

    //CoinPoolへオブジェクトを返却する外部からの呼び出し関数
    public void CoinPoolReturn(GameObject returnObj)
    {
        coinPool.ReleaseGameObject(returnObj);
    }

    //CoinInformationに値を代入する外部からの呼び出し関数
    public void CoinValueChanger(int value)
    {
        coinInfo.CoinValue = value;
    }

    //コインの発生頻度を変更する関数
    public void CoinFrequencyChange(int frequencyValue, bool valueChange)
    {
        if (valueChange)
        {
            coinCreateInterval = frequencyValue;
        }
        else
        {
            coinCreateInterval = coinCreateIntervalDefault;
        }
    }

    //プラスとマイナスのコインに関する処理を行う関数
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
        coinTra.position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
        if (createPoint)
        {
            plusCoin.SetActive(true);
            minusCoin.SetActive(true);
        }
        
        if (gameDatas.plusCoinCount > 7)
        {
            GimicCoinChanger();

            gMane.RouletteStarter(0);
        }
        else if(gameDatas.minusCoinCount > 7)
        {
            GimicCoinChanger();

            gMane.RouletteStarter(1);
        }
    }

    private void GimicCoinChanger()
    {
        gameDatas.plusCoinCount = 0;
        gameDatas.minusCoinCount = 0;

        plusCoin.SetActive(false);
        minusCoin.SetActive(false);
    }
}
