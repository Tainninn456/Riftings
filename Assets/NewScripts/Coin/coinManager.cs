using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coinManager : MonoBehaviour
{
    [Header("�R�C���I�u�W�F�N�g")]
    [SerializeField] GameObject coin;
    [Header("�v���X�R�C���I�u�W�F�N�g")]
    [SerializeField] GameObject plusCoin;
    [Header("�}�C�i�X�R�C���I�u�W�F�N�g")]
    [SerializeField] GameObject minusCoin;
    [Header("���߂ɐ������Ă����R�C���̖���")]
    [SerializeField] int InitialCoinAmount;
    [Header("�R�C���𐶐�����Ԋu")]
    [SerializeField] int coinCreateInterval;

    [Header("�R�C���v�[���Q�̐e�I�u�W�F�N�g")]
    [SerializeField] GameObject coinsParent;

    [Header("�Q�[�����̃f�[�^�ւ̃A�N�Z�X")]
    [SerializeField] InGameStockData gameDatas;

    [Header("�M�~�b�N�}�l�[�W���[�ւ̃A�N�Z�X")]
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

    //CoinPool�Ɋւ���O������̌Ăяo���֐�
    public void CoinPoolReturn(GameObject returnObj)
    {
        coinPool.ReleaseGameObject(returnObj);
    }

    //CoinInformation�Ɋւ���O������̌Ăяo���֐�
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

    //�v���X�ƃ}�C�i�X�̃R�C���Ɋւ���֐�
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
