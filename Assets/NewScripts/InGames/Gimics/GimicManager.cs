using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GimicManager : MonoBehaviour
{
    [Header("�M�~�b�N�̕p�x")]
    [SerializeField] int gimicPoint;

    [Header("�R�C�������p�x�̕ύX�l")]
    [SerializeField] int frequencyValue;

    [Header("�v���C���[�̎Q��")]
    [SerializeField] Newplayer playerReference;

    [Header("�{�[���̎Q��")]
    [SerializeField] NewBall ballReference;

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

    private int gimicCounter;

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
            gimicNumber = Random.Range(0, 7);
            switch(gimicNumber)
            {
                case 0:
                    //�������̕�
                    WideWindowGimic();
                    break;
                case 1:
                    //�{�[���̏d�͂�ύX
                    GravityChangerGimic();
                    break;
                case 2:
                    //�����_���ɕ��Ń{�[���𓮂���
                    RandomWindowGimic(true);
                    break;
                case 3:
                    //�傫��
                    PlayerScaleChangerGimic(1);
                    break;
                case 4:
                    //������
                    PlayerScaleChangerGimic(2);
                    break;
                case 5:
                    WallChangerGimic();
                    break;
                case 6:
                    CoinFrequenceGimic();
                    break;
            }
            RouletteCoinGimic();
            gimicDisplay.sprite = gimicSprites[gimicNumber];
            gimicCounter = 0;
        }
        if (randGimic) { ballReference.WindRandomAttackGimic(); }
    }
    //�������̕��M�~�b�N
    private void WideWindowGimic()
    {
        ballTriggerReference.WallGimicStarter(true);
    }

    //�d�͕ύX�M�~�b�N
    private void GravityChangerGimic()
    {
        ballReference.GravityChanger(1);
    }

    //���C���M�~�b�N
    private void RandomWindowGimic(bool randStart)
    {
        randGimic = randStart;
    }

    //�v���C���[�̑傫���ύX�M�~�b�N
    private void PlayerScaleChangerGimic(int scaleDirection)
    {
        playerReference.ScaleChanger(scaleDirection);
    }

    //�ǐ����ω��M�~�b�N
    private void WallChangerGimic()
    {
        ballReference.WallGimicStarter(true);
    }

    private void CoinFrequenceGimic()
    {
        cMane.CoinFrequencyChange(frequencyValue, true);
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
        switch (actionType)
        {
            case 0:
                cMane.CoinValueChanger(2 * gameDatas.coinMultiplication);
                break;
            case 1:
                cMane.CoinValueChanger(-1);
                break;
            case 2:
                cMane.CoinValueChanger(1 * gameDatas.coinMultiplication);
                break;
        }
    }

    private void KickAmountGimic(int actionType)
    {
        switch (actionType)
        {
            case 0:
                ballReference.KickAddValueChanger(2);
                break;
            case 1:
                ballReference.KickAddValueChanger(-1);
                break;
            case 2:
                ballReference.KickAddValueChanger(1);
                break;
        }
    }

    private void BallScaleGimic(int actionType)
    {
        ballReference.BallScaleChanger(actionType);
    }
    private void GimicResetter()
    {
        ballTriggerReference.WallGimicStarter(false);
        ballReference.WallGimicStarter(false);
        ballReference.GravityChanger(2);
        playerReference.ScaleChanger(0);
        RandomWindowGimic(false);
        cMane.CoinFrequencyChange(0, false);
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

    //���[���b�g���e�������Z�b�g
    public void RouletteReset()
    {
        CoinAmountGimic(2);
        KickAmountGimic(2);
        ballReference.BallScaleChanger(2);
    }
}
