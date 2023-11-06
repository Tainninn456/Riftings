using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// �f�[�^�Ɋւ���A�N�V���������s�ATextAction��ImageAction�̓�������s����\���A��
/// </summary>
public class DataAction : MonoBehaviour
{
    [Header("���C�����j���[")]
    [SerializeField] TextAction textAction;
    [SerializeField] ImageAction imageAction;

    [Header("�C���Q�[��")]
    [SerializeField] InGameStockData gameDataStock;

    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    const string bothWrite = "TextAndImage";
    const string textWrite = "TextOnly";
    const string imageWrite = "ImageOnly";

    const int levelMax = 10;

    //������A�N�Z�X�o���Ȃ��^�̃f�[�^
    private Data data = new Data();
    //�V���b�v�̏�񂪓����Ă���萔�̃f�[�^
    private shopPrices shopData = new shopPrices();
    //���݂̃X�|�[�c�e��̃X�v���C�g��ێ�
    public Sprite[] sportSprites = new Sprite[9];

    //�����ւ��̒��Ō��݃A�N�V�������s���Ă���X�|�[�c�̃C���f�b�N�X
    private int clothChangeIndex;
    private string dataFilePath;
    private void Start()
    {
        //�f�[�^�֘A��initialize
        dataFilePath = Application.persistentDataPath + "/Data.json";
        data.CoinAmount = 2000000;
        DataSave();
        DataLoad();
        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            //���̑�initialize
            SpritesInsert();
            textAction.DataIntoText();
            textAction.ItemDataText();
        }
    }

    //�����X�|�[�c�^�C�v������
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
        imageAction.RockDataIntoImage(clothChangeIndex);
        textAction.ShopDataIntoText(clothChangeIndex);
    }

    //�����ւ��̏�����m��
    public void ClothConsume(int consumeIndex)
    {
        //�������ɍw�����Ă����炽�������ւ������s
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

    //�A�C�e���̏�����m��
    public void ItemConsume(int itemIndex)
    {
        //�R�C���̏���
        if(itemIndex == 0)
        {
            if(data.coinLevel == levelMax) { return; }
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
        //�n�[�g�̏���
        else if(itemIndex == 1)
        {
            if(data.heartLevel == levelMax) { return; }
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
        textAction.ItemDataText();
        DataSave();
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

    //�e�L�X�g�ƃC���[�W���f�[�^����ύX
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

    //�X�|�[�c�e��̌��݂̃X�v���C�g���擾
    private void SpritesInsert()
    {
        for(int i = 0; i < sportSprites.Length; i++)
        {
            sportSprites[i] = imageAction.sportSprites[i * 9 + data.sportCloth[i]];
        }
    }

    //�f�[�^�Ɋւ���֐��B
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