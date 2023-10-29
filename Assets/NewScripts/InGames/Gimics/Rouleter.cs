using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rouleter : MonoBehaviour
{
    [Header("���[���b�g�Ɏg�p����摜(�v���X���[���b�g)")]
    [SerializeField] Sprite[] plusSprites;

    [Header("���[���b�g�Ɏg�p����摜(�}�C�i�X���[���b�g)")]
    [SerializeField] Sprite[] minusSprites;

    [Header("�\������X�v���C�g")]
    [SerializeField] Image rouletteSprite;

    [Header("�M�~�b�N�}�l�[�W���[�̎Q��")]
    [SerializeField] GimicManager gMane;
    //�Ώۂ̉摜�Q
    private Sprite[] InsertSprites;

    private int nowRouletteType;
    public void RouletteStart(int rouletteType)
    {
        nowRouletteType = rouletteType;
        switch (rouletteType)
        {
            case 0:
                Debug.Log("plus");
                InsertSprites = plusSprites;
                break;
            case 1:
                Debug.Log("minus");
                InsertSprites = minusSprites;
                break;
        }
        StartCoroutine("Roulette");
    }

    IEnumerator Roulette()
    {
        int returnNumber = 0;
        for (int i = 0; i < 30; i++)
        {
            int rand = Random.Range(0, 3);
            rouletteSprite.sprite = InsertSprites[rand];
            yield return new WaitForSeconds(0.1f);
            if(i == 29)
            {
                returnNumber = rand;
            }
        }
        gMane.RouletteDesicion(returnNumber, nowRouletteType);
    }
}
