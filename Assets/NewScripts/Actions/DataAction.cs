using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// データに関するアクションを実行するクラス
/// </summary>
public class DataAction : MonoBehaviour
{
    const string menuSceneName = "menuScene";

    [Header("メインメニュー")]
    [Header("TextActionクラスの参照")]
    [SerializeField] TextAction textAction;
    [Header("ImageActionクラスの参照")]
    [SerializeField] ImageAction imageAction;

    [Header("インゲーム")]
    [Header("ゲームプレイ中にデータを集約させておくクラスの参照")]
    [SerializeField] InGameStockData gameDataStock;

    //他からアクセス出来ない真のデータ
    private Data data = new Data();
    //ショップの情報が入っているハードコーディングしたクラス
    private shopPrices shopData = new shopPrices();
    //現在のスポーツ各種のスプライトを保持し、他クラスでも使用する
    public Sprite[] sportSprites = new Sprite[9];

    //着せ替えの中で現在アクションを行っているスポーツのインデックス
    private int clothChangeIndex;
    //Jsonのデータを保存してあるファイルへのパス
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
            textAction.ItemDataIntoText();
        }
    }

    //消費するスポーツタイプを決定する関数
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
        imageAction.RockDataIntoImage(clothChangeIndex);
        textAction.ShopDataIntoText(clothChangeIndex);
    }

    //着せ替えの消費を確定する関数
    public void ClothConsume(int consumeIndex)
    {
        //もし既に購入していたらただ着せ替えを実行
        if (data.clothAchive[clothChangeIndex] > consumeIndex - 1)
        {
            AudioManager.instance.PlaySE(AudioManager.SE.ItemOk);
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
                textAction.ShopDataIntoText(clothChangeIndex);
                textAction.DataIntoText();
                AudioManager.instance.PlaySE(AudioManager.SE.ItemOk);
            }
            else
            {
                AudioManager.instance.PlaySE(AudioManager.SE.ItemMiss);
            }
        }
        DataSave();
        SpritesInsert();
    }

    //アイテムの消費を確定する関数
    public void ItemConsume(int itemIndex)
    {
        //コインの処理
        if(itemIndex == 0)
        {
            if(data.coinLevel == shopData.coinPrices.Length + 1) { return; }
            int moneyValue = shopData.coinPrices[data.coinLevel - 1];
            if (data.CoinAmount > moneyValue)
            {
                AudioManager.instance.PlaySE(AudioManager.SE.ItemOk);
                data.CoinAmount -= moneyValue;
                data.coinLevel++;
            }
            else
            {
                AudioManager.instance.PlaySE(AudioManager.SE.ItemMiss);
            }
        }
        //ハートの処理
        else if(itemIndex == 1)
        {
            if(data.heartLevel == shopData.heartPrices.Length + 1) { return; }
            int moneyValue = shopData.heartPrices[data.heartLevel - 1];
            if (data.CoinAmount > moneyValue)
            {
                AudioManager.instance.PlaySE(AudioManager.SE.ItemOk);
                data.CoinAmount -= moneyValue;
                data.heartLevel++;
            }
            else
            {
                AudioManager.instance.PlaySE(AudioManager.SE.ItemMiss);
            }
        }
        textAction.DataIntoText();
        textAction.ItemDataIntoText();
        DataSave();
    }

    //ゲームが終了した際にデータを集約していたクラスを元にセーブを行う関数
    public void GameEndDataSaveStarter(int scoreIndex)
    {
        if (gameDataStock.kickCount > data.GameScores[scoreIndex])
        {
            data.GameScores[scoreIndex] = gameDataStock.kickCount;
        }
        data.CoinAmount += gameDataStock.coinCount;
        DataSave();
    }

    //スポーツ各種の現在のスプライトを取得する関数
    private void SpritesInsert()
    {
        for(int i = 0; i < sportSprites.Length; i++)
        {
            sportSprites[i] = imageAction.sportSprites[i * 9 + data.sportCloth[i]];
        }
    }

    //ファイルからJsonのデータを取得する関数
    private void DataLoad()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(dataFilePath);
        datastr = reader.ReadToEnd();
        JsonUtility.FromJsonOverwrite(datastr, data);
        reader.Close();
    }

    //ファイルへJson形式でデータを保存する関数
    private void DataSave()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(data);
        writer = new StreamWriter(dataFilePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //Dataクラスへの書き込みや読み取りを他クラスで実行させないためにコピーを渡すための関数
    public Data DataCopy()
    {
        Data copyData = data;
        return copyData;
    }
}