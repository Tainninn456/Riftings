using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// データに関するアクションを実行、TextActionとImageActionの動作も実行する可能性アリ
/// </summary>
public class DataAction : MonoBehaviour
{
    [SerializeField] TextAction textAction;
    [SerializeField] ImageAction imageAction;

    const string bothWrite = "TextAndImage";
    const string textWrite = "TextOnly";
    const string imageWrite = "ImageOnly";

    private Data data = new Data();
    private shopPrices shopData = new shopPrices();

    private int clothChangeIndex;
    private string dataFilePath;
    private void Start()
    {
        dataFilePath = Application.persistentDataPath + "/Data.json";
        DataLoad();
        data.CoinAmount = 500;
        DataSave();
    }

    //消費するスポーツタイプを決定
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
    }

    //着せ替えの消費を確定
    public void ClothConsume(int consumeIndex)
    {
        if(data.CoinAmount > shopData.shopConsumePrices[clothChangeIndex, consumeIndex])
        {
            Debug.Log("bye");
        }
        else
        {
            Debug.Log("miss");
        }
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

    //テキストとイメージをデータから変更
    private void DisplayWriting(string operationName)
    {
        switch (operationName)
        {
            case bothWrite:
                textAction.DataIntoImage();
                imageAction.DataIntoText();
                break;
            case textWrite:
                textAction.DataIntoImage();
                break;
            case imageWrite:
                imageAction.DataIntoText();
                break;
        }
    }

    //データに関する関数達
    private void DataLoad()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(dataFilePath);
        datastr = reader.ReadToEnd();
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
}