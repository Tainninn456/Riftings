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

    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    const string bothWrite = "TextAndImage";
    const string textWrite = "TextOnly";
    const string imageWrite = "ImageOnly";

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
        DataLoad();
        data.CoinAmount = 500;
        DataSave();

        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            //���̑�initialize
            SpritesInsert();
        }
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
                textAction.DataIntoText();
                imageAction.DataIntoImage();
                break;
            case textWrite:
                textAction.DataIntoText();
                break;
            case imageWrite:
                imageAction.DataIntoImage();
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