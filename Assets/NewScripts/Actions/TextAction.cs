using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TextAction : MonoBehaviour
{
    const string ActiveTagName = "ActiveObject";
    const string methodScoreName = "AllScoreTextComponentGeter";
    const string methodMoneyName = "AllMoneyTextComponentGeter";

    const int levelMax = 10;

    [Header("���C�����j���[")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI[] moneyTexts;
    [SerializeField] TextMeshProUGUI[] shopTexts;
    [SerializeField] TextMeshProUGUI[] itemTexts;

    [SerializeField] DataAction dataAction;

    [Header("�C���Q�[��")]
    [SerializeField] TextMeshProUGUI kickText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI resultKickText;
    [SerializeField] TextMeshProUGUI resultCoinText;

    private shopPrices shopDatas = new shopPrices();

    /// <summary>
    /// ���C�����j���[�����s�֐�
    /// </summary>
    /// 

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

    public void ItemDataText()
    {
        Data useData = dataAction.DataCopy();
        //�R�C�����̏���
        string coinString = "";
        if(useData.coinLevel == levelMax) 
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
        if(useData.heartLevel == levelMax)
        {
            heartString = "Max";
        }
        else
        {
            heartString = shopDatas.heartPrices[useData.heartLevel - 1].ToString();
        }
        itemTexts[1].text = heartString;
    }

    //game�I�����̃��U���g�\���Fkick�񐔁Acoin��
    public void GameEndTextDisplay(int kickCount, int coinCount)
    {
        resultKickText.text = kickCount.ToString();
        resultCoinText.text = coinCount.ToString();
    }

    /// <summary>
    /// �C���Q�[�������s�֐�
    /// </summary>
    
    public void KickCountDisplay(int kickCount)
    {
        kickText.text = kickCount.ToString();
    }

    public void CoinCountDisplay(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }

#if UNITY_EDITOR

    /// <summary>
    /// �G�f�B�^����s�֐�
    /// </summary>
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

    [ContextMenu(methodMoneyName)]
    private void AllMoneyTextComponentGeter()
    {
        List<TextMeshProUGUI> texs = new List<TextMeshProUGUI>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI ob in children)
            {
                if (ob.tag != ActiveTagName) { continue; }
                texs.Add(ob);
            }
        }
        Array.Resize<TextMeshProUGUI>(ref moneyTexts, texs.Count);
        for (int i = 0; i < texs.Count; i++)
        {
            moneyTexts[i] = texs[i];
        }
    }
#endif
}
