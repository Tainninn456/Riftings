using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static string dataPath;
    void Start()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "Data.json");
        if (File.Exists(dataPath) != true)
        {
            FileStream fs1;
            fs1 = File.Create(dataPath);
            fs1.Dispose();
            for(int i = 0; i < 9; i++)
            {
                Main.memory.GameScores[i] = 0;
                Main.memory.cloths[i] = 9;
                Main.memory.nowCloth[i] = 9;
            }
            Main.memory.CoinAmount = 0;
            Save1();
        }
        Load1();
        Main.HeartLevel = Main.panels[4].transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();
        Main.CoinLevel = Main.panels[4].transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>();
        Main.HeartLevel.text = Main.memory.Heart.ToString();//shopのハートとコインの買った回数
        Main.CoinLevel.text = Main.memory.CoinLevel.ToString();
        Main.ChainChecker();
    }
    void Load1()
    {
        string datastr;
        StreamReader reader;
        reader = new StreamReader(dataPath);
        datastr = reader.ReadToEnd();
        JsonUtility.FromJsonOverwrite(datastr, Main.memory);
        reader.Close();
    }
    void Save1()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(Main.memory, false);
        writer = new StreamWriter(dataPath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}
