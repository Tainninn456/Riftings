using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";

    [Header("�R�C���}�l�[�W���[")]
    [SerializeField] coinManager cMane;
    [Header("�R�C���̎擾����\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI coinTex;

    [Header("�f�[�^�n�ւ̃A�N�Z�X")]
    [SerializeField] GameObject managerInformation;

    [Header("�e�L�X�g�ւ̃A�N�Z�X")]
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
