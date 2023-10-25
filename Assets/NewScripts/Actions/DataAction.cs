using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// �f�[�^�Ɋւ���A�N�V���������s�ATextAction��ImageAction�̓�������s����\���A��
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

    //�����X�|�[�c�^�C�v������
    public void ClothDesicion(int inputIndex)
    {
        clothChangeIndex = inputIndex;
    }

    //�����ւ��̏�����m��
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

    //�A�C�e���̏�����m��
    public void ItemConsume(int itemIndex)
    {
        //�R�C���̏���
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
        //�n�[�g�̏���
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

    //�e�L�X�g�ƃC���[�W���f�[�^����ύX
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

    //�f�[�^�Ɋւ���֐��B
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