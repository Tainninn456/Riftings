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

    [Header("コイン生成場所のx軸最低値")]
    [SerializeField] float coinXposUnder;
    [Header("コイン生成場所のx軸最高値")]
    [SerializeField] float coinXposOver;
    [Header("コイン生成場所のy軸最低値")]
    [SerializeField] float coinYposUnder;
    [Header("コイン生成場所のy軸最高値")]
    [SerializeField] float coinYposOver;

    [System.NonSerialized]
    public CoinInformation coinInfo= new CoinInformation();

    private int createCounter;

    private bool pozeRock;
    private void Awake()
    {
        if (GameManager.Instance.created) { return; }
        for(int i = 0; i < InitialCoinAmount; i++)
        {
            GameObject createObj = coinInfo.InitialCreate(coin);
            createObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            coinInfo.ReturnCoin(createObj);
        }
        GameManager.Instance.created = true;
    }
    private void Update()
    {
        //アクティブ系
        if(GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.game) != (int)GameManager.State.game) { coinInfo.UnActivator(); pozeRock = true; return; }
        else if (pozeRock) { coinInfo.DoActivator(); pozeRock = false; }
        createCounter++;
        if(createCounter > coinCreateInterval)
        {
            GameObject coinObj = coinInfo.UseCoin(coin);
            coinObj.SetActive(true);
            coinObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            createCounter = 0;
        }
    }
}
