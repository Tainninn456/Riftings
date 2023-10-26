using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinManager : MonoBehaviour
{
    [Header("�R�C���I�u�W�F�N�g")]
    [SerializeField] GameObject coin;
    [Header("���߂ɐ������Ă����R�C���̖���")]
    [SerializeField] int InitialCoinAmount;
    [Header("�R�C���𐶐�����Ԋu")]
    [SerializeField] int coinCreateInterval;

    [Header("�R�C�������ꏊ��x���Œ�l")]
    [SerializeField] float coinXposUnder;
    [Header("�R�C�������ꏊ��x���ō��l")]
    [SerializeField] float coinXposOver;
    [Header("�R�C�������ꏊ��y���Œ�l")]
    [SerializeField] float coinYposUnder;
    [Header("�R�C�������ꏊ��y���ō��l")]
    [SerializeField] float coinYposOver;


    [SerializeField] CoinPool coinPool;

    private int createCounter;

    private bool pozeRock;
    private void Awake()
    {
        if (GameManager.Instance.created) { return; }
        for (int i = 0; i < InitialCoinAmount; i++)
        {
            GameObject createObj = coinPool.InitialCreate(coin);
            createObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            coinPool.ReturnCoin(createObj);
        }
        GameManager.Instance.created = true;
    }
    private void Update()
    {
        createCounter++;
        if (createCounter > coinCreateInterval)
        {
            GameObject coinObj = coinPool.UseCoin(coin);
            coinObj.SetActive(true);
            coinObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            createCounter = 0;
        }
    }
}
