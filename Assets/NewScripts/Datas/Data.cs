using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Data
{
    public int[] GameScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int Heart;
    public int CoinLevel = 1;
    public int CoinAmount = 0;
    public int chainLevel = 8;
    public int[] cloths = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int[] nowCloth = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int SEVolume = 2;
    public int BGMVolume = 2;

    //�����ւ����ǂ��܂ŃA�����b�N���Ă��邩
    public int[] clothAchive = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //�R�C���̃A�����b�N���x��
    public int coinLevel = 1;
    //�n�[�g�̃A�����b�N���x��
    public int heartLevel = 1;
    //�C���f�b�N�X�ɑΉ����������ւ��̓��e
    public int[] sportCloth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //�Q�[���̋L�^
    public int[] PlayScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}
