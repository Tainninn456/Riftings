using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataOperator: MonoBehaviour
{
    public static DataOperator Instance;

    private Data data;
    private string dataFilePath;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        dataFilePath = Application.persistentDataPath + "/Data.json";
        DataLoad();
    }

    public void DataLoad()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(dataFilePath);
        datastr = reader.ReadToEnd();
        reader.Close();
    }

    public void DataSave()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(data);
        writer = new StreamWriter(dataFilePath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}
