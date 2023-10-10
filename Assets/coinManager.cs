using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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


    ObjectPool<GameObject> pools;

    private int createCounter;

    bool once;
    private void Awake()
    {
        pools = new ObjectPool<GameObject>(() => Instantiate(coin), (GameObject obj) => obj.SetActive(true), (GameObject obj) => obj.SetActive(false), (GameObject obj) => Destroy(obj), false, 10, 70);
    }

    private void Update()
    {
        if (!once)
        {
            GameManager.Instance.StateChanger(GameManager.State.Play);
            once = true;
        }
        createCounter++;
        if(createCounter > coinCreateInterval)
        {
            //GameObject coinObj = Instantiate(coin, new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver)), Quaternion.identity);
            GameObject coinObj =  pools.Get();
            coinObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            createCounter = 0;
        }
    }
}
