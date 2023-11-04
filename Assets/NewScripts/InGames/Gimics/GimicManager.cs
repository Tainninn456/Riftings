using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimicManager : MonoBehaviour
{
    [Header("�M�~�b�N�̕p�x")]
    [SerializeField] int gimicPoint;

    [Header("�v���C���[�̎Q��")]
    [SerializeField] Newplayer playerReference;

    [Header("�{�[���̎Q��")]
    [SerializeField] Newboal boalReference;

    [Header("�{�[���̃g���K�[�Q��(�����������̏����͂������̕�����������?)")]
    [SerializeField] ballTrigger ballTriggerReference;

    [Header("�Q�[�����f�[�^�̎Q��")]
    [SerializeField] InGameStockData gameDatas;

    [Header("���[���b�g")]
    [SerializeField] Rouleter roulette;

    [Header("�R�C���}�l�[�W���[")]
    [SerializeField] coinManager cMane;

    [Header("�M�~�b�N�X�v���C�g")]
    [SerializeField] Sprite[] gimicSprites;

    [Header("�M�~�b�N��\������f�B�X�v���C")]
    [SerializeField] Image gimicDisplay;

    const int coinValueBig = 2;

    private int gimicCounter;
    private int hitgimicCounter;

    private int gimicNumber;

    private bool gimicCoinning;
    private bool randGimic;

    private void FixedUpdate()
    {
        if (!gameDatas.GimicCalculating) { return; }
        gimicCounter++;
    }
    private void Update()
    {
        if(gimicCounter > gimicPoint)
        {
            GimicResetter();
            gimicNumber = Random.Range(0, 6);
            switch(gimicNumber)
            {
                case 0:
                    WideWindowGimic();
                    Debug.Log("wide");
                    break;
                case 1:
                    GravityChangerGimic();
                    Debug.Log("gravity");
                    break;
                case 2:
                    RandomWindowGimic(true);
                    Debug.Log("random");
                    break;
                case 3:
                    //�傫��
                    PlayerScaleChangerGimic(1);
                    Debug.Log("scalebigger");
                    break;
                case 4:
                    //������
                    PlayerScaleChangerGimic(2);
                    Debug.Log("scalesmaller");
                    break;
                case 5:
                    WallChangerGimic();
                    Debug.Log("wallchanger");
                    break;
            }
            RouletteCoinGimic();
            gimicDisplay.sprite = gimicSprites[gimicNumber];
            gimicCounter = 0;
        }
        if (randGimic) { boalReference.WindRandomAttackGimic(); }
    }
    private void WideWindowGimic()
    {
        ballTriggerReference.WallGimicStarter(true);
    }

    private void GravityChangerGimic()
    {
        boalReference.GravityChanger(1);
    }

    private void RandomWindowGimic(bool randStart)
    {
        randGimic = randStart;
    }

    private void PlayerScaleChangerGimic(int scaleDirection)
    {
        playerReference.ScaleChanger(scaleDirection);
    }

    private void WallChangerGimic()
    {
        boalReference.WallGimicStarter(true);
    }

    private void RouletteCoinGimic()
    {
        if (!gimicCoinning)
        {
            gimicCoinning = true;
            cMane.GimicCoin(1, true);
            cMane.GimicCoin(2, true);
        }
    }

    private void CoinAmountGimic(int actionType)
    {
        cMane.CoinValueChanger(coinValueBig);
    }

    private void KickAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                boalReference.KickAddValueChanger(1);
                break;
            case 1:
                boalReference.KickAddValueChanger(2);
                break;
        }
        //boalReference.KickAddValueChanger(actionType);
    }

    private void BallScaleGimic(int actionType)
    {
        boalReference.BallScaleChanger(actionType);
    }
    private void GimicResetter()
    {
        ballTriggerReference.WallGimicStarter(false);
        boalReference.WallGimicStarter(false);
        boalReference.GravityChanger(2);
        playerReference.ScaleChanger(0);
        RandomWindowGimic(false);
    }

    public void RouletStarter(int actionType)
    {
        roulette.RouletteStart(actionType);
        gimicCoinning = false;
    }

    //actionNumber=4��ނ̓�1��ނ�����AactionType=�v���X���}�C�i�X��
    public void RouletteDesicion(int actionNumber, int actionType)
    {
        switch (actionNumber)
        {
            case 0:
                CoinAmountGimic(actionType);
                break;
            case 1:
                KickAmountGimic(actionType);
                break;
            case 2:
                BallScaleGimic(actionType);
                break;
        }
    }
}
