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

    [Header("�R�C���v�[���Q�̐e�I�u�W�F�N�g")]
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
}
