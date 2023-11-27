using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// �f�[�^�Ɋւ���A�N�V���������s����N���X
/// </summary>
public class DataAction : MonoBehaviour
{
    const string menuSceneName = "menuScene";

    [Header("���C�����j���[")]
    [Header("TextAction�N���X�̎Q��")]
    [SerializeField] TextAction textAction;
    [Header("ImageAction�N���X�̎Q��")]
    [SerializeField] ImageAction imageAction;

    [Header("�C���Q�[��")]
    [Header("�Q�[���v���C���Ƀf�[�^���W�񂳂��Ă����N���X�̎Q��")]
    [SerializeField] InGameStockData gameDataStock;

    //������A�N�Z�X�o���Ȃ��^�̃f�[�^
    private Data data = new Data();
    //�V���b�v�̏�񂪓����Ă���n�[�h�R�[�f�B���O�����N���X
    private shopPrices shopData = new shopPrices();
    //���݂̃X�|�[�c�e��̃X�v���C�g��ێ����A���N���X�ł��g�p����
    public Sprite[] sportSprites = new Sprite[9];

    //�����ւ��̒��Ō��݃A�N�V�������s���Ă���X�|�[�c�̃C���f�b�N�X
    private int clothChangeIndex;
    //Json�̃f�[�^��ۑ����Ă���t�@�C���ւ̃p�X
    private string dataFilePath;
    private void Start()
    {
        //�f�[�^�֘A��initialize
        dataFilePath = Application.persistentDataPath + "/Data.json";
        DataLoad();
        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            //���̑�initialize
            SpritesInsert();
            textAction.DataIntoText();
            textAction.ItemDataIntoText();
        }
    }

    //�����X�|�[�c�^�C�v�����肷��֐�
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
        imageAction.RockDataIntoImage(clothChangeIndex);
        textAction.ShopDataIntoText(clothChangeIndex);
    }

    //�����ւ��̏�����m�肷��֐�
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

    //�A�C�e���̏�����m�肷��֐�
    public void ItemConsume(int itemIndex)
    {
        //�R�C���̏���
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
        //�n�[�g�̏���
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

    //�Q�[�����I�������ۂɃf�[�^���W�񂵂Ă����N���X�����ɃZ�[�u���s���֐�
    public void GameEndDataSaveStarter(int scoreIndex)
    {
        if (gameDataStock.kickCount > data.GameScores[scoreIndex])
        {
            data.GameScores[scoreIndex] = gameDataStock.kickCount;
        }
        data.CoinAmount += gameDataStock.coinCount;
        DataSave();
    }

    //�X�|�[�c�e��̌��݂̃X�v���C�g���擾����֐�
    private void SpritesInsert()
    {
        for(int i = 0; i < sportSprites.Length; i++)
        {
            sportSprites[i] = imageAction.sportSprites[i * 9 + data.sportCloth[i]];
        }
    }

    //�t�@�C������Json�̃f�[�^���擾����֐�
    private void DataLoad()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(dataFilePath);
        datastr = reader.ReadToEnd();
        JsonUtility.FromJsonOverwrite(datastr, data);
        reader.Close();
    }

    //�t�@�C����Json�`���Ńf�[�^��ۑ�����֐�
    private void DataSave()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(data);
        writer = new StreamWriter(dataFilePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //Data�N���X�ւ̏������݂�ǂݎ��𑼃N���X�Ŏ��s�����Ȃ����߂ɃR�s�[��n�����߂̊֐�
    public Data DataCopy()
    {
        Data copyData = data;
        return copyData;
    }
}