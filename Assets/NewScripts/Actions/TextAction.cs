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

    [Header("メインメニュー")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI[] moneyTexts;
    [SerializeField] TextMeshProUGUI[] shopTexts;
    [SerializeField] TextMeshProUGUI[] itemTexts;

    [SerializeField] DataAction dataAction;

    [Header("インゲーム")]
    [SerializeField] TextMeshProUGUI kickText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI resultKickText;
    [SerializeField] TextMeshProUGUI resultCoinText;

    private shopPrices shopDatas = new shopPrices();

    /// <summary>
    /// メインメニュー内実行関数
    /// </summary>
    /// 

    public void ShopDataIntoText(int shopValueIndex)
    {
        Data useData = dataAction.DataCopy();
        for(int i = 0; i < 9; i++)
        {
            if (useData.clothAchive[shopValueIndex] >= i)
            {
                shopTexts[i].text = "使用";
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
        //お金の総額を表示
        moneyTexts[0].text = useData.CoinAmount.ToString();
        //コインのレベルを表示
        moneyTexts[1].text = "× " + useData.coinLevel.ToString();
        //ハートのレベルを表示
        moneyTexts[2].text = "× " + useData.heartLevel.ToString();
    }

    public void ItemDataText()
    {
        Data useData = dataAction.DataCopy();
        //コイン側の処理
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
        //ハート側の処理
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

    //game終了時のリザルト表示：kick回数、coin個数
    public void GameEndTextDisplay(int kickCount, int coinCount)
    {
        resultKickText.text = kickCount.ToString();
        resultCoinText.text = coinCount.ToString();
    }

    /// <summary>
    /// インゲーム内実行関数
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
    /// エディタ上実行関数
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
