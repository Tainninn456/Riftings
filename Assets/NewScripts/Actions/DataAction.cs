using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// データに関するアクションを実行、TextActionとImageActionの動作も実行する可能性アリ
/// </summary>
public class DataAction : MonoBehaviour
{
    [Header("メインメニュー")]
    [SerializeField] TextAction textAction;
    [SerializeField] ImageAction imageAction;

    [Header("インゲーム")]
    [SerializeField] InGameStockData gameDataStock;

    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    const string bothWrite = "TextAndImage";
    const string textWrite = "TextOnly";
    const string imageWrite = "ImageOnly";

    //他からアクセス出来ない真のデータ
    private Data data = new Data();
    //ショップの情報が入っている定数のデータ
    private shopPrices shopData = new shopPrices();
    //現在のスポーツ各種のスプライトを保持
    public Sprite[] sportSprites = new Sprite[9];

    //着せ替えの中で現在アクションを行っているスポーツのインデックス
    private int clothChangeIndex;
    private string dataFilePath;
    private void Start()
    {
        //データ関連のinitialize
        dataFilePath = Application.persistentDataPath + "/Data.json";
        DataLoad();
        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            //その他initialize
            SpritesInsert();
            textAction.DataIntoText();
        }
    }

    //消費するスポーツタイプを決定
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
        imageAction.RockDataIntoImage(clothChangeIndex);
        textAction.ShopDataIntoText(clothChangeIndex);
    }

    //着せ替えの消費を確定
    public void ClothConsume(int consumeIndex)
    {
        //もし既に購入していたらただ着せ替えを実行
        if (data.clothAchive[clothChangeIndex] > consumeIndex - 1)
        {
            data.sportCloth[clothChangeIndex] = consumeIndex;
        }
        else
        {
            int moneyValue = shopData.shopConsumePrices[clothChangeIndex, consumeIndex];
            if (data.CoinAmount > moneyValue)
            {
                data.CoinAmount -= moneyValue;
                data.clothAchive[clothChangeIndex]++;
                data.sportCloth[clothChangeIndex] = consumeIndex;
                imageAction.RockDataIntoImage(clothChangeIndex);
                textAction.DataIntoText();
                Debug.Log("bye");
            }
            else
            {
                Debug.Log("miss");
            }
        }
        DataSave();
        SpritesInsert();
    }

    //アイテムの消費を確定
    public void ItemConsume(int itemIndex)
    {
        //コインの処理
        if(itemIndex == 0)
        {
            if (data.CoinAmount > shopData.coinPrices[data.coinLevel])
            {
                Debug.Log("bye");
            }
            else
            {
                Debug.Log("miss");
            }
        }
        //ハートの処理
        else if(itemIndex == 1)
        {
            if (data.CoinAmount > shopData.coinPrices[data.heartLevel])
            {
                Debug.Log("bye");
            }
            else
            {
                Debug.Log("miss");
            }
        }
    }

    public void GameEndDataSaveStarter(int scoreIndex)
    {
        if (gameDataStock.kickCount > data.GameScores[scoreIndex])
        {
            data.GameScores[scoreIndex] = gameDataStock.kickCount;
        }
        data.CoinAmount += gameDataStock.coinCount;
        DataSave();
    }

    //テキストとイメージをデータから変更
    private void DisplayWriting(string operationName)
    {
        switch (operationName)
        {
            case bothWrite:
                textAction.DataIntoText();
                //imageAction.DataIntoImage();
                break;
            case textWrite:
                textAction.DataIntoText();
                break;
            case imageWrite:
                //imageAction.DataIntoImage();
                break;
        }
    }

    //スポーツ各種の現在のスプライトを取得
    private void SpritesInsert()
    {
        for(int i = 0; i < sportSprites.Length; i++)
        {
            sportSprites[i] = imageAction.sportSprites[i * 9 + data.sportCloth[i]];
        }
    }

    //データに関する関数達
    private void DataLoad()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(dataFilePath);
        datastr = reader.ReadToEnd();
        JsonUtility.FromJsonOverwrite(datastr, data);
        reader.Close();
    }

    private void DataSave()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(data);
        writer = new StreamWriter(dataFilePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    public Data DataCopy()
    {
        Data copyData = data;
        return copyData;
    }
}