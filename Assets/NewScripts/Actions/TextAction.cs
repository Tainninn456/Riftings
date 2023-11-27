using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using TMPro;

/// <summary>
/// テキスト系の動作を行うクラス
/// </summary>
public class TextAction : MonoBehaviour
{
    const string ActiveTagName = "ActiveObject";
    const string methodScoreName = "AllScoreTextComponentGeter";

    [Header("メインメニュー")]

    [Header("各スポーツのスコアを表示するテキスト")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [Header("所持コイン、コインのレベル、ハートのレベルをそれぞれ表示するテキスト")]
    [SerializeField] TextMeshProUGUI[] moneyTexts;
    [Header("各スポーツの着せ替え値段を表示するテキスト")]
    [SerializeField] TextMeshProUGUI[] shopTexts;
    [Header("コインとハートの値段を表示するテキスト")]
    [SerializeField] TextMeshProUGUI[] itemTexts;

    [Header("プレイヤーのデータへの参照")]
    [SerializeField] DataAction dataAction;

    [Header("インゲーム")]

    [Header("プレイ中にキックカウントを表示するテキスト")]
    [SerializeField] TextMeshProUGUI kickCountDisplay;
    [Header("プレイ中に獲得したコインの総数を表示するテキスト")]
    [SerializeField] TextMeshProUGUI coinCountDisplay;
    [Header("上記のそれぞれをリザルトにて表示するテキスト")]
    [SerializeField] TextMeshProUGUI resultKickText;
    [SerializeField] TextMeshProUGUI resultCoinText;

    //各着せ替えやコインレベルとハートレベルの値段をハードコーディングしたクラス
    private shopPrices shopDatas = new shopPrices();

    /// <summary>
    /// メインメニュー内実行関数
    /// </summary>
    /// 

    //着せ替え購入画面にて、各着せ替えの値段を表示する関数
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

    //ショップ画面にある所持コイン、コインのレベル、ハートのレベルを表示する関数
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

    //コインレベルとハートレベルの上昇に必要なコイン数を表示する関数
    public void ItemDataIntoText()
    {
        Data useData = dataAction.DataCopy();
        //コイン側の処理
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
        //ハート側の処理
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

    //ゲームプレイ終了時のリザルト表示：kick回数、coin枚数
    public void GameEndTextDisplay(int kickCount, int coinCount)
    {
        resultKickText.text = kickCount.ToString();
        resultCoinText.text = coinCount.ToString();
    }

    //ゲームプレイ中にキックカウントを表示する関数
    public void KickCountDisplay(int Value)
    {
        kickCountDisplay.text = Value.ToString();
    }

    //ゲームプレイ中に獲得したコインの総数を表示する関数
    public void CoinCountDisplay(int Value)
    {
        coinCountDisplay.text = Value.ToString();
    }

#if UNITY_EDITOR

    /// <summary>
    /// エディタ上実行関数、serializeへ一度に値を登録するための関数
    /// </summary>
    /// 

    //各スポーツのスコアを表示するテキストを一度に取得する関数
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
