using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using TMPro;

/// <summary>
/// �e�L�X�g�n�̓�����s���N���X
/// </summary>
public class TextAction : MonoBehaviour
{
    const string ActiveTagName = "ActiveObject";
    const string methodScoreName = "AllScoreTextComponentGeter";

    [Header("���C�����j���[")]

    [Header("�e�X�|�[�c�̃X�R�A��\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [Header("�����R�C���A�R�C���̃��x���A�n�[�g�̃��x�������ꂼ��\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI[] moneyTexts;
    [Header("�e�X�|�[�c�̒����ւ��l�i��\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI[] shopTexts;
    [Header("�R�C���ƃn�[�g�̒l�i��\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI[] itemTexts;

    [Header("�v���C���[�̃f�[�^�ւ̎Q��")]
    [SerializeField] DataAction dataAction;

    [Header("�C���Q�[��")]

    [Header("�v���C���ɃL�b�N�J�E���g��\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI kickCountDisplay;
    [Header("�v���C���Ɋl�������R�C���̑�����\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI coinCountDisplay;
    [Header("��L�̂��ꂼ������U���g�ɂĕ\������e�L�X�g")]
    [SerializeField] TextMeshProUGUI resultKickText;
    [SerializeField] TextMeshProUGUI resultCoinText;

    //�e�����ւ���R�C�����x���ƃn�[�g���x���̒l�i���n�[�h�R�[�f�B���O�����N���X
    private shopPrices shopDatas = new shopPrices();

    /// <summary>
    /// ���C�����j���[�����s�֐�
    /// </summary>
    /// 

    //�����ւ��w����ʂɂāA�e�����ւ��̒l�i��\������֐�
    public void ShopDataIntoText(int shopValueIndex)
    {
        Data useData = dataAction.DataCopy();
        for(int i = 0; i < 9; i++)
        {
            if (useData.clothAchive[shopValueIndex] >= i)
            {
                shopTexts[i].text = "�g�p";
            }
            else
            {
                shopTexts[i].text = shopDatas.shopConsumePrices[shopValueIndex, i].ToString();
            }
        }
    }

    //�V���b�v��ʂɂ��鏊���R�C���A�R�C���̃��x���A�n�[�g�̃��x����\������֐�
    public void DataIntoText()
    {
        Data useData = dataAction.DataCopy();
        for(int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = useData.GameScores[i].ToString();
        }
        //�����̑��z��\��
        moneyTexts[0].text = useData.CoinAmount.ToString();
        //�R�C���̃��x����\��
        moneyTexts[1].text = "�~ " + useData.coinLevel.ToString();
        //�n�[�g�̃��x����\��
        moneyTexts[2].text = "�~ " + useData.heartLevel.ToString();
    }

    //�R�C�����x���ƃn�[�g���x���̏㏸�ɕK�v�ȃR�C������\������֐�
    public void ItemDataIntoText()
    {
        Data useData = dataAction.DataCopy();
        //�R�C�����̏���
        string coinString = "";
        if(useData.coinLevel == shopDatas.coinPrices.Length + 1) 
        {
            coinString = "Max";
        }
        else
        {
            coinString = shopDatas.coinPrices[useData.coinLevel - 1].ToString();
        }
        itemTexts[0].text = coinString;
        //�n�[�g���̏���
        string heartString = "";
        if(useData.heartLevel == shopDatas.coinPrices.Length + 1)
        {
            heartString = "Max";
        }
        else
        {
            heartString = shopDatas.heartPrices[useData.heartLevel - 1].ToString();
        }
        itemTexts[1].text = heartString;
    }

    //�Q�[���v���C�I�����̃��U���g�\���Fkick�񐔁Acoin����
    public void GameEndTextDisplay(int kickCount, int coinCount)
    {
        resultKickText.text = kickCount.ToString();
        resultCoinText.text = coinCount.ToString();
    }

    //�Q�[���v���C���ɃL�b�N�J�E���g��\������֐�
    public void KickCountDisplay(int Value)
    {
        kickCountDisplay.text = Value.ToString();
    }

    //�Q�[���v���C���Ɋl�������R�C���̑�����\������֐�
    public void CoinCountDisplay(int Value)
    {
        coinCountDisplay.text = Value.ToString();
    }

#if UNITY_EDITOR

    /// <summary>
    /// �G�f�B�^����s�֐��Aserialize�ֈ�x�ɒl��o�^���邽�߂̊֐�
    /// </summary>
    /// 

    //�e�X�|�[�c�̃X�R�A��\������e�L�X�g����x�Ɏ擾����֐�
    [ContextMenu(methodScoreName)]
    private void AllScoreTextComponentGeter()
    {
        List<TextMeshProUGUI> texs = new List<TextMeshProUGUI>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI ob in children)
            {
                if(ob.tag != ActiveTagName) {continue; }
                texs.Add(ob);
            }
        }
        Array.Resize<TextMeshProUGUI>(ref scoreTexts, texs.Count);
        for(int i = 0; i < texs.Count; i++)
        {
            scoreTexts[i] = texs[i];
        }
    }
#endif
}
