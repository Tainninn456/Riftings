using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Data memory = new Data();
    public static GameObject ball; public static GameObject player; public static GameObject typhoon; public static GameObject warp; public static GameObject bom;
    public static GameObject[] chains = new GameObject[8]; public static GameObject[] coins = new GameObject[50]; public static GameObject[] Hearts = new GameObject[5]; public static GameObject[] minusCoins = new GameObject[7]; public static GameObject[] numberCoins = new GameObject[7];
    public static GameObject[] panels = new GameObject[8];
    public GameObject coinSample;
    static GameObject minuslots; static GameObject slots;
    GameObject canvas; GameObject panchis; GameObject loadPanel;
    public static Transform bTra; public static Transform pTra; public static Transform typTra; public static Transform bomTra;
    public static Transform[] coinTrans = new Transform[50]; public static Transform[] minusNumberTrans = new Transform[7]; public static Transform[] numberTrans = new Transform[7];
    public static Transform[] panchiTra = new Transform[2];
    public static Rigidbody2D brigi; public static Rigidbody2D prigi;
    public static TextMeshProUGUI coincounter; public static TextMeshProUGUI CoinLevel; public static TextMeshProUGUI coinStock; public static TextMeshProUGUI counter; public static TextMeshProUGUI HeartLevel; public static TextMeshProUGUI LastCoinTex; public static TextMeshProUGUI LastScoreTex; public static TextMeshProUGUI comentTex; public static TextMeshProUGUI nextTex;
    public static TextMeshProUGUI[] scoreTexts = new TextMeshProUGUI[9];
    TextMeshProUGUI startTex;
    public static Image bImage; public static Image pImage;
    static Image[] minusimages = new Image[4]; static Image[] simages = new Image[4];
    Image phaseImage; Image loadImage;
    Image[] walls = new Image[4];
    public static Sprite[] backstocks = new Sprite[5]; public static Sprite[] buttoncloth = new Sprite[90];
    public Sprite[] backs; public Sprite[] buttonBacks = new Sprite[4]; public Sprite[] clothes = new Sprite[81]; public Sprite[] Images; public Sprite[] phaseSprite; public Sprite[] Wsprite = new Sprite[3];
    public static AudioSource pAudio;
    public static int coinCount; public static int HeartAmount; public static int minuslot; public static int situation; public static int slot = 0;
    int MainCount = 1; int minuslotingCount; int slotingCount; int stcount; int lastCount;
    public static float[] phases = new float[10];
    public static float[] debuf = new float[4]; public static float[] powerUps = new float[4];
    float enableCount; float phaseCount = 1; float slotCounter = 255; float loadCount;
    public static bool gaming; public static bool over; public static bool startDef; public static bool slotDef; public static bool warpbool; public static bool stop;
    bool enabool; bool slotend;
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        for (int i = 0; i < 8; i++)
        {
            panels[i] = canvas.transform.GetChild(i).gameObject;
        }
        Transform tr = panels[0].GetComponent<Transform>();
        for (int i = 0; i < 50; i++)
        {
            coins[i] = Instantiate(coinSample, Vector3.zero, Quaternion.identity, tr);
            coinTrans[i] = coins[i].GetComponent<Transform>();
            coins[i].SetActive(false);
        }
    }
    void Start()
    {
        PlayParent.GameBackImage = panels[0].GetComponent<Image>();
        player = panels[0].transform.GetChild(0).gameObject;//初期取得欄
        ball = panels[0].transform.GetChild(1).gameObject; slots = panels[0].transform.GetChild(11).gameObject; minuslots = panels[0].transform.GetChild(13).gameObject;
        panchis = panels[0].transform.GetChild(14).gameObject; typhoon = panels[0].transform.GetChild(15).gameObject; warp = panels[0].transform.GetChild(16).gameObject; bom = panels[0].transform.GetChild(19).gameObject;
        pTra = player.GetComponent<Transform>(); 
        bTra = ball.GetComponent<Transform>(); 
        typTra = typhoon.GetComponent<Transform>(); 
        bomTra = bom.GetComponent<Transform>();
        prigi = player.GetComponent<Rigidbody2D>(); brigi = ball.GetComponent<Rigidbody2D>();
        pImage = player.GetComponent<Image>(); bImage = ball.GetComponent<Image>(); phaseImage = panels[0].transform.GetChild(5).GetComponent<Image>();
        counter = panels[0].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>(); coincounter = panels[0].transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        startTex = panels[2].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); loadPanel = panels[2].transform.GetChild(3).gameObject; loadImage = panels[2].transform.GetChild(3).transform.GetChild(1).GetComponent<Image>();
        LastScoreTex = panels[7].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>(); LastCoinTex = panels[7].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>(); nextTex = panels[7].transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>(); comentTex = panels[7].transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        coinStock = panels[4].transform.GetChild(8).gameObject.GetComponent<TextMeshProUGUI>();
        HeartLevel = panels[4].transform.GetChild(6).gameObject.GetComponent<TextMeshProUGUI>();
        CoinLevel = panels[4].transform.GetChild(7).gameObject.GetComponent<TextMeshProUGUI>();
        pAudio = player.GetComponent<AudioSource>();
        for (int i = 0; i < 81; i++)
        {
            if (i < 9)
            {
                buttoncloth[i] = Images[i + 9];
            }
            buttoncloth[i + 9] = clothes[i];
            if (i > 71)//9
            {
                scoreTexts[i - 72] = panels[3].transform.GetChild(i - 70).gameObject.GetComponent<TextMeshProUGUI>();
            }
            if (i > 72)//8
            {
                int k = i - 73;
                phases[k] = 1;
                chains[k] = panels[1].transform.GetChild(10).gameObject.transform.GetChild(k).gameObject;
            }
            if (i > 73)//7
            {
                int k = i - 74;
                numberCoins[k] = panels[0].transform.GetChild(10).transform.GetChild(k).gameObject;
                numberTrans[k] = numberCoins[k].GetComponent<Transform>();
                minusCoins[k] = panels[0].transform.GetChild(12).transform.GetChild(k).gameObject;
                minusNumberTrans[k] = minusCoins[k].GetComponent<Transform>();
            }
            if (i > 75)//5
            {
                Hearts[i - 76] = panels[0].transform.GetChild(9).transform.GetChild(i - 76).gameObject;
                backstocks[i - 76] = buttonBacks[i - 76];
            }
            if (i > 76)//4
            {
                int k = i - 77;
                walls[k] = panels[0].transform.GetChild(4).transform.GetChild(k).gameObject.GetComponent<Image>();
                simages[k] = slots.transform.GetChild(k).gameObject.GetComponent<Image>();
                simages[k].color = new Color(1, 1, 1, 0.407f);
                minusimages[k] = minuslots.transform.GetChild(k).gameObject.GetComponent<Image>();
                minusimages[k].color = new Color(1, 1, 1, 0.407f);
            }
            if (i > 77)//3
            {
                PlayParent.BackImages[i - 78] = backs[i - 78];
            }
            if (i > 78)//2
            {
                panchiTra[i - 79] = panels[0].transform.GetChild(14).transform.GetChild(i - 79).GetComponent<Transform>();
            }
        }
        PlayParent.GameBackImage.sprite = backs[0];
        phaseImage.sprite = phaseSprite[8];//どのフェーズかを現す絵
        HeartAmount = 0;//ハートの数設定
        BGMplayer(1);
    }
    void Update()
    {
        if (gaming)
        {
            if (Sound.BGMAudio.isPlaying != true)
            {
                BGMplayer(2);
            }
            if (stop) { }
            else
            {
                phaseCount -= 0.03f;
                phaseImage.color = new Color(1, 1, 1, phaseCount);
                MainCount++;
                if (MainCount % 200 == 0)
                {
                    Phase();
                }
                else if (MainCount % 100 == 0)
                {
                    PlayParent.SpeCoin();
                    PlayParent.MinusSpeCoint();
                }
                switch (ButtonChil.gameMord)
                {
                    case 0:
                        Soccer_Player.move();
                        break;
                    case 1:
                        Tennis_Player.move();
                        break;
                    case 2:
                        Baseball_Player.move();
                        break;
                    case 3:
                        Boring_Player.move();
                        break;
                    case 4:
                        Panchi_Player.move();
                        break;
                    case 5:
                        Tableteniss_Player.move();
                        break;
                    case 6:
                        Ragby_Player.move();
                        break;
                    case 7:
                        Biriya_d_Player.move();
                        break;
                    case 8:
                        Vary_Player.move();
                        break;
                    default:
                        break;
                }
            }
            if (over)
            {
                gaming = false;
                lastCount = 0;
            }
            else if (ButtonChil.porzKey)
            {
                Time.timeScale = 0;
                stop = true;
                panels[5].SetActive(true);
            }
            else if (ButtonChil.porzKey != true)
            {
                Time.timeScale = 1;
                stop = false;
                panels[5].SetActive(false);
            }
            if (slot != 0)
            {
                slots.SetActive(true);
                minuslots.SetActive(false);
                PlayPowerUp(slot);
            }
            else if (minuslot != 0)
            {
                minuslots.SetActive(true);
                slots.SetActive(false);
                Debuf(minuslot);
            }
            else if (slotend)
            {
                if (stcount > 30)
                {
                    if (slotCounter > 1)
                    {
                        slotCounter -= 2;
                        for (int i = 0; i < 4; i++)
                        {
                            float col = slotCounter / 255;
                            simages[i].color = new Color(col, col, col, col);
                            minusimages[i].color = new Color(col, col, col, col);
                        }
                    }
                    else if (slotCounter < 1)
                    {
                        slotDEF();
                    }

                }
                else
                {
                    stcount++;
                }
            }
        }
        else if (over)
        {

            if (bTra.transform.localPosition.y < -380)
            {
                bom.SetActive(true);
                brigi.Sleep();
                brigi.constraints = RigidbodyConstraints2D.FreezePositionX;
                brigi.constraints = RigidbodyConstraints2D.FreezePositionY;
                bomTra.transform.localPosition = new Vector2(bTra.transform.localPosition.x, bTra.transform.localPosition.y);
                lastCount++;
                if (lastCount % 10 == 0)
                {
                    if (bomTra.transform.localScale.x == 1.5f)
                    {
                        bomTra.localScale = new Vector2(3, 3);
                    }
                    else
                    {
                        bomTra.localScale = new Vector2(1.5f, 1.5f);
                    }
                }
                if (lastCount > 100)
                {
                    panels[7].SetActive(true);
                    lastCount = 0;
                    Sound.SEplay(3);
                }
            }
            gaming = false;
            panchis.SetActive(false);
            if (slotDef)
            {
                panchiTra[0].transform.localPosition = new Vector2(155.95f, 0);
                panchiTra[1].transform.localPosition = new Vector2(-155.95f, 0);
                slotDEF();
            }
        }
        else
        {
            //シーンの読み込み
            if (loadPanel.activeSelf)
            {
                loadCount += 0.01f;
                if(loadCount >= 1)
                {
                    loadPanel.SetActive(false);
                    loadCount = 0;
                }
                else
                {
                    loadImage.fillAmount = loadCount;
                }
            }
            if (Sound.BGMAudio.isPlaying != true)
            {
                BGMplayer(1);
            }
            EnterKeyenable();
            if (ButtonChil.startKey)
            {
                panels[2].SetActive(false);
                ButtonChil.startKey = false;
            }
            if (ButtonChil.gameMord != 9 || startDef)
            {
                for (int i = 0; i < HeartAmount; i++)
                {
                    Hearts[i].SetActive(true);
                }
                MainCount = 1;
                phaseImage.sprite = phaseSprite[8];
                pImage.sprite = Images[ButtonChil.gameMord];
                if (memory.nowCloth[ButtonChil.gameMord] != 9)
                {
                    bImage.sprite = clothes[9 * ButtonChil.gameMord + memory.nowCloth[ButtonChil.gameMord]];
                }
                else
                {
                    bImage.sprite = Images[ButtonChil.gameMord + 9];
                }
                brigi.gravityScale = 0.3f;
                PlayParent.GameBackImage.sprite = backs[0];
                gaming = true;
                startDef = false;
                slotDEF();
                for (int i = 0; i < 4; i++)
                {
                    walls[i].sprite = Wsprite[2];
                }
            }
        }
    }
    void Phase()
    {
        phaseCount = 1;
        Sound.SEplay(9);
        for (int i = 0; i < 4; i++)
        {
            phases[i] = 1;
        }
        if (powerUps[1] == 0 && debuf[1] == 0)
        {
            pTra.localScale = new Vector3(4, 2, 1);
        }
        int judge = Random.Range(0, 45);
        if (judge > 40)
        {
            phases[0] = 1.3f;//跳ねる具合up
            situation = 1;
            phaseImage.sprite = phaseSprite[0];
        }
        else if (judge > 35 && ButtonChil.gameMord != 1 && ButtonChil.gameMord != 3)
        {
            phases[1] = 1.9f;//重力up
            situation = 2;
            phaseImage.sprite = phaseSprite[1];
        }
        else if (judge > 30)
        {
            phases[2] = 0.5f;//乱気流
            situation = 3;
            phaseImage.sprite = phaseSprite[2];
        }
        else if (judge > 25)
        {
            phases[2] = 0.33f;//右向きの暴風
            situation = 4;
            phaseImage.sprite = phaseSprite[3];
        }
        else if (judge > 20)
        {
            phases[2] = -0.33f;//左向きの暴風
            situation = 5;
            phaseImage.sprite = phaseSprite[4];
        }
        else if (judge > 15)//当たり判定減少
        {
            PosChange();
            phases[3] = 8;
            situation = 6;
            pTra.localScale = new Vector3(2, 1, 1);
            phaseImage.sprite = phaseSprite[5];
            powerUps[1] = 0;
            debuf[1] = 0;
        }
        else if (judge > 10)//壁床ばね
        {
            if (phases[4] == 1 && phases[5] == 1)
            {
                phases[4] = 2;
                situation = 7;
                for (int i = 0; i < 4; i++)
                {
                    walls[i].sprite = Wsprite[0];
                }
                phaseImage.sprite = phaseSprite[6];
            }
            else
            {
                phases[4] = 1;
                situation = 0;
                for (int i = 0; i < 4; i++)
                {
                    walls[i].sprite = Wsprite[2];
                }
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else if (ButtonChil.gameMord == 1)//跳ねる具合超up
        {
            if (phases[5] == 1 && phases[4] == 1)
            {
                phases[5] = 3;
                situation = 8;
                for (int i = 0; i < 4; i++)
                {
                    walls[i].sprite = Wsprite[1];
                }
                phaseImage.sprite = phaseSprite[7];
            }
            else
            {
                phases[5] = 1;
                situation = 0;
                for (int i = 0; i < 4; i++)
                {
                    walls[i].sprite = Wsprite[2];
                }
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else if (ButtonChil.gameMord == 2)//黄金バット
        {
            if (phases[6] == 1)
            {
                pImage.color = new Color(0.8117f, 0.8313f, 0.2470f, 1);
                phases[6] = 2;
                situation = 9;
                phaseImage.sprite = phaseSprite[9];
            }
            else
            {
                pImage.color = new Color(1, 1, 1, 1);
                phases[6] = 1;
                situation = 0;
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else if (ButtonChil.gameMord == 4)
        {//panchi
            if (phases[7] == 1)
            {
                panchis.SetActive(true);
                phases[7] = 2;
                situation = 10;
                phaseImage.sprite = phaseSprite[10];
            }
            else
            {
                panchis.SetActive(false);
                panchiTra[0].transform.localPosition = new Vector2(290, 0);
                panchiTra[1].transform.localPosition = new Vector2(-290, 0);
                phases[7] = 1;
                situation = 0;
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else if (ButtonChil.gameMord == 6)//ragby
        {
            if (phases[8] == 1)
            {
                typhoon.SetActive(true);
                phases[8] = 2;
                situation = 11;
                phaseImage.sprite = phaseSprite[11];
            }
            else
            {
                typhoon.SetActive(false);
                phases[8] = 1;
                situation = 0;
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else if (ButtonChil.gameMord == 8)//vary
        {
            if (phases[9] == 1)
            {
                warp.SetActive(true);
                phases[9] = 2;
                situation = 12;
                phaseImage.sprite = phaseSprite[12];
            }
            else
            {
                warp.SetActive(false);
                phases[9] = 1;
                situation = 0;
                phaseImage.sprite = phaseSprite[8];
            }
        }
        else
        {
            phaseImage.sprite = phaseSprite[8];
            situation = 0;
        }
    }
    public static void BGMplayer(int index)
    {
        Sound.BGMplay(index);
    }
    public static void BGMstop()
    {
        Sound.BGMAudio.Stop();
    }
    void EnterKeyenable()
    {
        if (enabool)
        {
            enableCount -= 0.01f;
            if (enableCount < 0)
            {
                enabool = false;
            }
        }
        else if (enabool != true)
        {
            enableCount += 0.01f;
            if (enableCount > 1)
            {
                enabool = true;
            }
        }
        startTex.color = new Color(1, 1, 1, enableCount);
    }
    void PlayPowerUp(int slot)
    {
        slotingCount++;
        if (slotingCount > 60)
        {
            slotingCount = 0;
            Main.slot = 0;
            Sound.SEplay(11);
            slotend = true;
            for (int i = 0; i < 4; i++)
            {
                simages[i].color = new Color(1, 1, 1, 0.407f);
            }
            switch (slot)
            {
                case 1:
                    powerUps[0] += 1;//coin
                    simages[0].color = new Color(1, 1, 1, 1);
                    break;
                case 2://playerが大きくなる
                    PosChange();
                    pTra.localScale = new Vector3(6, 3, 1);
                    powerUps[1] = 0.6f;
                    simages[1].color = new Color(1, 1, 1, 1);
                    phases[3] = 1;
                    if (situation == 6)
                    {
                        situation = 0;
                    }
                    break;
                case 3://ballが大きくなる
                    bTra.localScale = new Vector3(4.2f, 2.4f, 1);
                    powerUps[2] = 0.6f;
                    simages[2].color = new Color(1, 1, 1, 1);
                    break;
                case 4:
                    powerUps[3] += 3;//リフティング倍率が上がる
                    simages[3].color = new Color(1, 1, 1, 1);
                    break;
                default:
                    break;
            }
        }
        else
        {
            int index = slotingCount % 4;
            if (index == 0)
            {
                simages[3].color = new Color(1, 1, 1, 0.407f);
                simages[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                simages[index - 1].color = new Color(1, 1, 1, 0.407f);
                simages[index].color = new Color(1, 1, 1, 1);
            }
        }
    }
    void Debuf(int minuslot)
    {
        minuslotingCount++;
        if (minuslotingCount > 60)
        {
            minuslotingCount = 0;
            Main.minuslot = 0;
            Sound.SEplay(11);
            slotend = true;
            for (int i = 0; i < 4; i++)
            {
                minusimages[i].color = new Color(1, 1, 1, 0.407f);
            }
            switch (minuslot)
            {
                case 1:
                    debuf[0] += 1;//coin
                    minusimages[0].color = new Color(1, 1, 1, 1);
                    break;
                case 2://playerが小さくなる
                    PosChange();
                    pTra.localScale = new Vector3(2, 1, 1);
                    debuf[1] = 0.3f;
                    minusimages[1].color = new Color(1, 1, 1, 1);
                    phases[3] = 1;
                    if (situation == 6)
                    {
                        situation = 0;
                    }
                    break;
                case 3://ballが小さくなる
                    bTra.localScale = new Vector3(1.05f, 0.6f, 1);
                    debuf[2] = 0.3f;
                    minusimages[2].color = new Color(1, 1, 1, 1);
                    break;
                case 4:
                    debuf[3] += 3;//リフティング倍率が下がる
                    minusimages[3].color = new Color(1, 1, 1, 1);
                    break;
                default:
                    Main.minuslot = 0;
                    break;
            }
        }
        else
        {
            int index = minuslotingCount % 4;
            if (index == 0)
            {
                minusimages[3].color = new Color(1, 1, 1, 0.407f);
                minusimages[0].color = new Color(1, 1, 1, 1);
            }
            else
            {
                minusimages[index - 1].color = new Color(1, 1, 1, 0.407f);
                minusimages[index].color = new Color(1, 1, 1, 1);
            }
        }
    }
    void PosChange()
    {
        if (PlayParent.px > 330)
        {
            PlayParent.px = 328;
        }
        else if (PlayParent.px < -330)
        {
            PlayParent.px = -328;
        }
    }
    void slotDEF()
    {
        slotingCount = 0;
        minuslotingCount = 0;
        slot = 0;
        minuslot = 0;
        slotend = false;
        slots.SetActive(false);
        minuslots.SetActive(false);
        slotCounter = 255;
        stcount = 0;
        for (int i = 0; i < 4; i++)
        {
            simages[i].color = new Color(1, 1, 1, 1);
            minusimages[i].color = new Color(1, 1, 1, 1);
        }
        slotDef = false;
    }
    public static void Save()
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(memory, false);
        writer = new StreamWriter(DataManager.dataPath, false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
    public static void ChainChecker()
    {
        for (int i = 0; i < 8; i++)
        {
            if (memory.GameScores[i] > 50)
            {
                chains[i].SetActive(false);
                memory.chainLevel = 8 - (i + 1);
            }
        }
    }
}
