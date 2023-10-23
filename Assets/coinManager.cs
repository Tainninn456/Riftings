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
        //�A�N�e�B�u�n
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
