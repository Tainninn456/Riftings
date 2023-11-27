using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
public class ButtonChil : MonoBehaviour//ButtonParent
{
    /*public static int gameMord = 9; public static bool startKey; public static bool porzKey;
    static GameObject[] cloChain = new GameObject[8];
    static GameObject[] bgmval = new GameObject[5];
    static GameObject[] seval = new GameObject[5];
    static Image nowImage; static Rigidbody2D ImaRig;
    static int[] coinNeed = new int[] { 3000, 4000, 5000, 6000, 8000, 20000, 50000 };
    static int[] heartNeed = new int[] { 3000, 4000, 5000, 6000, 8000, 30000, 75000 };
    static Image[] backsimages = new Image[9]; static GameObject[] objects = new GameObject[9]; static GameObject[] clothPanels = new GameObject[9];
    static int gameNow = 9;
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            backsimages[i] = Main.panels[3].transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<Image>();
            clothPanels[i] = Main.panels[4].transform.GetChild(10).transform.GetChild(i).gameObject;
            objects[i] = Main.panels[4].transform.GetChild(i + 1).gameObject;
            if (i > 3)
            {
                bgmval[i - 4] = Main.panels[6].transform.GetChild(5).transform.GetChild(i - 4).gameObject;
                seval[i - 4] = Main.panels[6].transform.GetChild(6).transform.GetChild(i - 4).gameObject;
            }
            if (i > 0)
            {
                cloChain[i - 1] = Main.panels[4].transform.GetChild(10).transform.GetChild(10).transform.GetChild(i - 1).gameObject;
            }

        }
        for (int i = 0; i < Main.memory.BGMVolume + 1; i++)
        {
            bgmval[i].SetActive(true);
        }
        for (int i = 0; i < Main.memory.SEVolume + 1; i++)
        {
            seval[i].SetActive(true);
        }
        nowImage = Main.panels[4].transform.GetChild(10).transform.GetChild(9).GetComponent<Image>();
        ImaRig = Main.panels[4].transform.GetChild(10).transform.GetChild(9).GetComponent<Rigidbody2D>();
    }
    /*public override void OnClick(string objectName)
    {
        switch (objectName)
        {
            case "Soccer_start":
                gameMord = 0;
                Main.BGMstop();
                Main.BGMplayer(2);
                Main.panels[1].SetActive(false);
                break;
            case "tennis_start":
                if (Main.memory.chainLevel < 8)
                {
                    gameMord = 1;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "baseball_start":
                if (Main.memory.chainLevel < 7)
                {
                    gameMord = 2;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "boring_start":
                if (Main.memory.chainLevel < 6)
                {
                    gameMord = 3;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "panchi_start":
                if (Main.memory.chainLevel < 5)
                {
                    gameMord = 4;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "tableteniss_start":
                if (Main.memory.chainLevel < 4)
                {
                    gameMord = 5;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "ragby_start":
                if (Main.memory.chainLevel < 3)
                {
                    gameMord = 6;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "biriya-d_start":
                if (Main.memory.chainLevel < 2)
                {
                    gameMord = 7;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "vary_start":
                if (Main.memory.chainLevel < 1)
                {
                    gameMord = 8;
                    Main.BGMstop();
                    Main.BGMplayer(2);
                    Main.panels[1].SetActive(false);
                }
                break;
            case "Reset":
                Main.memory.CoinAmount += Main.coinCount;
                Main.ChainChecker();
                Main.Save();
                Main.panels[5].SetActive(false);
                Main.panels[7].SetActive(false);
                Main.panels[0].SetActive(true);
                Main.panels[1].SetActive(true);
                Main.gaming = false;
                Main.over = false;
                Time.timeScale = 1;
                Main.stop = false;
                for (int i = 0; i < 50; i++)
                {
                    Main.coins[i].SetActive(false);
                    if (i > 41)//8
                    {
                        Main.phases[i - 42] = 1;
                    }
                    if (i > 42)//7
                    {
                        Main.numberCoins[i - 43].SetActive(false);
                        Main.minusCoins[i - 43].SetActive(false);
                    }
                    if (i > 45)
                    {
                        Main.powerUps[i - 46] = 0;
                        Main.debuf[i - 46] = 0;
                    }
                }
                gameMord = 9;
                PlayParent.timeCount = 0;
                Main.brigi.Sleep();
                Main.pTra.transform.localPosition = new Vector2(0, -275.1f);
                Main.bTra.transform.localPosition = new Vector2(0, 0);
                Main.bTra.localRotation = new Quaternion(0, 0, 0, 0);
                PlayParent.px = 0;
                Main.brigi.gravityScale = 0;
                PlayParent.addY = 0;
                PlayParent.backIndex = 0;
                Main.situation = 0;
                Main.BGMstop();
                Main.BGMplayer(1);
                porzKey = false;
                Sound.playBGMSound = true;
                Main.pTra.localScale = new Vector3(4, 2, 1);
                Main.bTra.localScale = new Vector3(2.1f, 1.2f, 1);
                Main.HeartAmount = Main.memory.Heart;
                Main.typhoon.SetActive(false);
                Main.warp.SetActive(false);
                Main.warpbool = false;
                warp.hit = true;
                PlayParent.nc = true;
                PlayParent.mc = true;
                Main.bom.SetActive(false);
                Main.brigi.constraints = RigidbodyConstraints2D.None;
                Main.coinCount = 0;
                Main.coincounter.text = 0.ToString();
                Main.pImage.color = new Color(1, 1, 1, 1);
                Main.comentTex.text = "Next...";
                numberCoin.coinumber = 0;
                minusNumberCoin.minusCoinNumber = 0;
                break;
            case "restart":
                Main.BGMstop();
                Main.BGMplayer(2);
                porzKey = false;
                Main.memory.CoinAmount += Main.coinCount;
                Main.ChainChecker();
                Main.Save();
                Main.panels[5].SetActive(false);
                Main.panels[7].SetActive(false);
                Time.timeScale = 1;
                Main.stop = false;
                Main.gaming = false;
                for (int i = 0; i < 50; i++)
                {
                    Main.coins[i].SetActive(false);
                    if (i > 41)//8
                    {
                        Main.phases[i - 42] = 1;
                    }
                    if (i > 42)//7
                    {
                        Main.numberCoins[i - 43].SetActive(false);
                        Main.minusCoins[i - 43].SetActive(false);
                    }
                    if (i > 45)
                    {
                        Main.powerUps[i - 46] = 0;
                        Main.debuf[i - 46] = 0;
                    }
                }
                PlayParent.timeCount = 0;
                Main.brigi.Sleep();
                Main.pTra.transform.localPosition = new Vector2(0, -275.1f);
                Main.bTra.transform.localPosition = new Vector2(0, 0);
                Main.bTra.localRotation = new Quaternion(0, 0, 0, 0);
                PlayParent.px = 0;
                Main.brigi.gravityScale = 0;
                PlayParent.addY = 0;
                PlayParent.backIndex = 0;
                Main.situation = 0;
                Sound.playBGMSound = true;
                Main.pTra.localScale = new Vector3(4, 2, 1);
                Main.bTra.localScale = new Vector3(2.1f, 1.2f, 1);
                Main.HeartAmount = Main.memory.Heart;
                Main.typhoon.SetActive(false);
                Main.warp.SetActive(false);
                Main.warpbool = false;
                warp.hit = true;
                PlayParent.nc = true;
                PlayParent.mc = true;
                Main.bom.SetActive(false);
                Main.brigi.constraints = RigidbodyConstraints2D.None;
                Main.coinCount = 0;
                Main.coincounter.text = 0.ToString();
                Main.pImage.color = new Color(1, 1, 1, 1);
                Main.comentTex.text = "Next...";
                numberCoin.coinumber = 0;
                minusNumberCoin.minusCoinNumber = 0;
                Main.slotDef = true;
                Main.startDef = true;
                Main.over = false;
                break;
            case "Heart":
                if (Main.memory.CoinAmount > heartNeed[Main.memory.Heart])
                {
                    Main.coinStock.text = Main.memory.CoinAmount.ToString();
                    if (Main.memory.Heart < 5)
                    {
                        Main.memory.CoinAmount -= heartNeed[Main.memory.Heart];
                        Main.memory.Heart++;
                    }
                    Main.HeartAmount = Main.memory.Heart;
                    Main.HeartLevel.text = Main.memory.Heart.ToString();
                    Main.coinStock.text = Main.memory.CoinAmount.ToString();
                    Main.Save();
                }
                break;
            case "Coin":
                if (Main.memory.CoinAmount > coinNeed[Main.memory.CoinLevel - 1])
                {
                    Main.coinStock.text = Main.memory.CoinAmount.ToString();
                    if (Main.memory.CoinLevel < 6)
                    {
                        Main.memory.CoinAmount -= coinNeed[Main.memory.CoinLevel - 1];
                        Main.memory.CoinLevel++;
                    }
                    Main.CoinLevel.text = Main.memory.CoinLevel.ToString();
                    Main.coinStock.text = Main.memory.CoinAmount.ToString();
                    Main.Save();
                }
                break;
            case "music":
                Main.panels[6].SetActive(true);
                break;
            case "startButton":
                startKey = true;
                break;
            case "porzButton":
                if (porzKey != true)
                {
                    porzKey = true;
                }
                else
                {
                    porzKey = false;
                }
                break;
            case "BGMup":
                if (Main.memory.BGMVolume < 4)
                {
                    //Main.memory.BGMVolume++;
                    bgmval[Main.memory.BGMVolume + 1].SetActive(true);
                    Main.memory.BGMVolume++;
                    Sound.BGMAudio.volume = (Main.memory.BGMVolume + 1) * 0.2f;
                }
                break;
            case "SEup":
                if (Main.memory.SEVolume < 4)
                {
                    //Main.memory.SEVolume++;
                    seval[Main.memory.SEVolume + 1].SetActive(true);
                    Main.memory.SEVolume++;
                    Sound.SEAudio.volume = (Main.memory.SEVolume + 1) * 0.2f;
                }
                break;
            case "BGMdown":
                if (Main.memory.BGMVolume > 0)
                {
                    //Main.memory.BGMVolume--;
                    bgmval[Main.memory.BGMVolume].SetActive(false);
                    Main.memory.BGMVolume--;
                    Sound.BGMAudio.volume = (Main.memory.BGMVolume + 1) * 0.2f;
                }
                break;
            case "SEdown":
                if (Main.memory.SEVolume > 0)
                {
                    //Main.memory.SEVolume--;
                    seval[Main.memory.SEVolume].SetActive(false);
                    Main.memory.SEVolume--;
                    Sound.SEAudio.volume = (Main.memory.SEVolume + 1) * 0.2f;
                }
                break;
            case "RNext"://スコアパネル表示
                Main.panels[3].SetActive(true);
                for (int i = 0; i < 9; i++)
                {
                    Main.scoreTexts[i].text = Main.memory.GameScores[i].ToString();
                    if (Main.memory.GameScores[i] > 899)
                    {
                        backsimages[i].sprite = Main.backstocks[4];
                    }
                    if (Main.memory.GameScores[i] > 499)
                    {
                        backsimages[i].sprite = Main.backstocks[3];
                    }
                    else if (Main.memory.GameScores[i] > 349)
                    {
                        backsimages[i].sprite = Main.backstocks[2];
                    }
                    else if (Main.memory.GameScores[i] > 99)
                    {
                        backsimages[i].sprite = Main.backstocks[1];
                    }
                }
                break;
            case "LNext":
                Main.panels[4].SetActive(true);
                Main.coinStock.text = Main.memory.CoinAmount.ToString();
                break;
            case "Back":
                Main.panels[4].SetActive(false);
                Main.panels[3].SetActive(false);
                Main.panels[1].SetActive(true);//game画面をactive
                break;
            case "soundBack":
                Main.panels[6].SetActive(false);
                Main.Save();
                break;
            case "SoccerB":
                Nactiver(0, 0);
                clothPanels[0].SetActive(true);
                break;
            case "TennisB":
                Nactiver(0, 1);
                clothPanels[1].SetActive(true);
                break;
            case "baseballB":
                Nactiver(0, 2);
                clothPanels[2].SetActive(true);
                break;
            case "boringB":
                Nactiver(0, 3);
                clothPanels[3].SetActive(true);
                break;
            case "panchiB":
                Nactiver(0, 4);
                clothPanels[4].SetActive(true);
                break;
            case "tabletennisB":
                Nactiver(0, 5);
                clothPanels[5].SetActive(true);
                break;
            case "ragbyB":
                Nactiver(0, 6);
                clothPanels[6].SetActive(true);
                break;
            case "biriya_dB":
                Nactiver(0, 7);
                clothPanels[7].SetActive(true);
                break;
            case "varyB":
                Nactiver(0, 8);
                clothPanels[8].SetActive(true);
                break;
            case "clothBack":
                Nactiver(1, 9);
                for (int i = 0; i < 9; i++)
                {
                    clothPanels[i].SetActive(false);
                }
                Main.Save();
                break;
            case "Soccer1":
                Clother(800, 0, 9);
                break;
            case "Soccer2":
                Clother(1600, 0, 8);
                break;
            case "Soccer3":
                Clother(2400, 0, 7);
                break;
            case "Soccer4":
                Clother(3200, 0, 6);
                break;
            case "Soccer5":
                Clother(4000, 0, 5);
                break;
            case "Soccer6":
                Clother(4800, 0, 4);
                break;
            case "Soccer7":
                Clother(5600, 0, 3);
                break;
            case "Soccer8":
                Clother(6400, 0, 2);
                break;
            case "Soccer9":
                Clother(7200, 0, 1);
                break;
            case "Tennis1":
                Clother(800, 1, 9);
                break;
            case "Tennis2":
                Clother(1600, 1, 8);
                break;
            case "Tennis3":
                Clother(2400, 1, 7);
                break;
            case "Tennis4":
                Clother(3200, 1, 6);
                break;
            case "Tennis5":
                Clother(4000, 1, 5);
                break;
            case "Tennis6":
                Clother(4800, 1, 4);
                break;
            case "Tennis7":
                Clother(5600, 1, 3);
                break;
            case "Tennis8":
                Clother(6400, 1, 2);
                break;
            case "Tennis9":
                Clother(7200, 1, 1);
                break;
            case "baseball1":
                Clother(800, 2, 9);
                break;
            case "baseball2":
                Clother(1600, 2, 8);
                break;
            case "baseball3":
                Clother(2400, 2, 7);
                break;
            case "baseball4":
                Clother(3200, 2, 6);
                break;
            case "baseball5":
                Clother(4000, 2, 5);
                break;
            case "baseball6":
                Clother(4800, 2, 4);
                break;
            case "baseball7":
                Clother(5600, 2, 3);
                break;
            case "baseball8":
                Clother(6400, 2, 2);
                break;
            case "baseball9":
                Clother(7200, 2, 1);
                break;
            case "boring1":
                Clother(800, 3, 9);
                break;
            case "boring2":
                Clother(1600, 3, 8);
                break;
            case "boring3":
                Clother(2400, 3, 7);
                break;
            case "boring4":
                Clother(3200, 3, 6);
                break;
            case "boring5":
                Clother(4000, 3, 5);
                break;
            case "boring6":
                Clother(4800, 3, 4);
                break;
            case "boring7":
                Clother(5600, 3, 3);
                break;
            case "boring8":
                Clother(6400, 3, 2);
                break;
            case "boring9":
                Clother(7200, 3, 1);
                break;
            case "panchi1":
                Clother(900, 4, 9);
                break;
            case "panchi2":
                Clother(1800, 4, 8);
                break;
            case "panchi3":
                Clother(2700, 4, 7);
                break;
            case "panchi4":
                Clother(3600, 4, 6);
                break;
            case "panchi5":
                Clother(4500, 4, 5);
                break;
            case "panchi6":
                Clother(5400, 4, 4);
                break;
            case "panchi7":
                Clother(6300, 4, 3);
                break;
            case "panchi8":
                Clother(7200, 4, 2);
                break;
            case "panchi9":
                Clother(8100, 4, 1);
                break;
            case "tabletennis1":
                Clother(900, 5, 9);
                break;
            case "tabletennis2":
                Clother(1800, 5, 8);
                break;
            case "tabletennis3":
                Clother(2700, 5, 7);
                break;
            case "tabletennis4":
                Clother(3600, 5, 6);
                break;
            case "tabletennis5":
                Clother(4500, 5, 5);
                break;
            case "tabletennis6":
                Clother(5400, 5, 4);
                break;
            case "tabletennis7":
                Clother(6300, 5, 3);
                break;
            case "tabletennis8":
                Clother(7200, 5, 2);
                break;
            case "tabletennis9":
                Clother(8100, 5, 1);
                break;
            case "ragby1":
                Clother(900, 6, 9);
                break;
            case "ragby2":
                Clother(1800, 6, 8);
                break;
            case "ragby3":
                Clother(2700, 6, 7);
                break;
            case "ragby4":
                Clother(3600, 6, 6);
                break;
            case "ragby5":
                Clother(4500, 6, 5);
                break;
            case "ragby6":
                Clother(5400, 6, 4);
                break;
            case "ragby7":
                Clother(6300, 6, 3);
                break;
            case "ragby8":
                Clother(7200, 6, 2);
                break;
            case "ragby9":
                Clother(8100, 6, 1);
                break;
            case "biriya_d1":
                Clother(900, 7, 9);
                break;
            case "biriya_d2":
                Clother(1800, 7, 8);
                break;
            case "biriya_d3":
                Clother(2700, 7, 7);
                break;
            case "biriya_d4":
                Clother(3600, 7, 6);
                break;
            case "biriya_d5":
                Clother(4500, 7, 5);
                break;
            case "biriya_d6":
                Clother(5400, 7, 4);
                break;
            case "biriya_d7":
                Clother(6300, 7, 3);
                break;
            case "biriya_d8":
                Clother(7200, 7, 2);
                break;
            case "biriya_d9":
                Clother(8100, 7, 1);
                break;
            case "vary1":
                Clother(1000, 8, 9);
                break;
            case "vary2":
                Clother(2000, 8, 8);
                break;
            case "vary3":
                Clother(3000, 8, 7);
                break;
            case "vary4":
                Clother(4000, 8, 6);
                break;
            case "vary5":
                Clother(5000, 8, 5);
                break;
            case "vary6":
                Clother(6000, 8, 4);
                break;
            case "vary7":
                Clother(7000, 8, 3);
                break;
            case "vary8":
                Clother(8000, 8, 2);
                break;
            case "vary9":
                Clother(9000, 8, 1);
                break;
            case "ResetB":
                nowImage.sprite = Main.buttoncloth[gameNow];
                Main.memory.nowCloth[gameNow] = 9;
                break;
            default:
                break;
        }
    }
    static void Nactiver(int j, int c)
    {
        if (j == 0)
        {
            for (int i = 0; i < 9; i++)
            {
                objects[i].SetActive(false);
                if (i > 0)
                {
                    if (i < Main.memory.cloths[c])
                    {
                        cloChain[8 - i].SetActive(true);
                    }
                }
            }
            nowImage.color = new Color(1, 1, 1, 1);
            if (Main.memory.cloths[c] == 9)
            {
                nowImage.sprite = Main.buttoncloth[c];
            }
            else
            {
                nowImage.sprite = Main.buttoncloth[9 * c + Main.memory.nowCloth[c] + 9];
            }
            ImaRig.AddTorque(Random.Range(-0.3f, 0.4f), ForceMode2D.Impulse);
            gameNow = c;
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                objects[i].SetActive(true);
                if (i > 0)
                {
                    cloChain[i - 1].SetActive(false);
                }
            }
            nowImage.color = new Color(1, 1, 1, 0);
            Main.coinStock.text = Main.memory.CoinAmount.ToString();
            Main.Save();
            gameNow = 9;
        }
        if (c != 9 && Main.memory.cloths[c] == 9)
        {
            nowImage.sprite = Main.buttoncloth[c];
        }
    }
    static void Clother(int cost, int clothLevel, int level)
    {
        if (Main.memory.cloths[clothLevel] > 0 && Main.memory.cloths[clothLevel] < level + 1)
        {
            if (Main.memory.CoinAmount > cost && Main.memory.cloths[clothLevel] == level)
            {
                Main.memory.CoinAmount -= cost;
                Main.memory.cloths[clothLevel]--;
                if (level != 1)
                {
                    cloChain[9 - level].SetActive(false);
                    nowImage.sprite = Main.buttoncloth[9 * clothLevel + (10 - level) + 8];
                }
                else
                {
                    cloChain[7].SetActive(false);
                    nowImage.sprite = Main.buttoncloth[9 * clothLevel + 9 + 8];
                }
            }
            else if (Main.memory.cloths[clothLevel] < level)
            {
                nowImage.sprite = Main.buttoncloth[9 * clothLevel + (10 - level) + 8];
            }
            Main.memory.nowCloth[clothLevel] = 9 - level;
        }
        else
        {
            Main.memory.nowCloth[clothLevel] = 9 - level;
            nowImage.sprite = Main.buttoncloth[9 * clothLevel + (10 - level) + 8];
        }
        ImaRig.AddTorque(Random.Range(-0.3f, 0.4f), ForceMode2D.Impulse);
    }*/
}
