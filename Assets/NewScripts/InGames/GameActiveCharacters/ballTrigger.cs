using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";
    const string coinPlusTagName = "pluscoin";
    const string coinMinusTagName = "minuscoin";
    const string gimicTagName = "Gimic";

    [Header("�R�C���̎擾����\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI coinTex;

    [Header("�{�[���̎Q��")]
    [SerializeField] Newboal parentBoalScript;

    [Header("�f�[�^�n�ւ̃A�N�Z�X")]
    [SerializeField] GameObject managerInformation;

    [Header("�e�L�X�g�ւ̃A�N�Z�X")]
    [SerializeField] TextAction textAction;

    InGameStockData parentGameStockData;
    coinManager coinMane;
    CoinInformation coinInfo;

    private bool GimicBool;

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
        else if (collision.gameObject.CompareTag(gimicTagName))
        {
            if (GimicBool)
            {
                parentBoalScript.WindAttackGimic();
            }
        }
        else if (collision.gameObject.CompareTag(coinPlusTagName))
        {
            coinMane.GimicCoin(1, false);
        }
        else if (collision.gameObject.CompareTag(coinMinusTagName))
        {
            coinMane.GimicCoin(2, false);
        }
    }

    public void WallGimicStarter(bool gimicBool)
    {
        GimicBool = gimicBool;
    }
}
