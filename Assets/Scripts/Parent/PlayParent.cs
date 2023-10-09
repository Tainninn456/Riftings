using UnityEngine;
using UnityEngine.UI;

public class PlayParent : MonoBehaviour
{
    public static float px;
    public static int bx; public static int by; public static int hx; public static int backIndex = 0; public static int coindex;public static int dif = 20;
    static int[] xRange = new int[9] {250, 160, 290, 280, 260, 190, 250, 350, 250 };
    public static float addY; public static float timeCount;
    public static bool once = true; static bool on2e = true; public static bool on3e = true; public static bool nc = true; public static bool mc = true;
    public static Sound ISsound = new Sound();
    public static Sprite[] BackImages = new Sprite[3];
    public static Image GameBackImage;
    public static GameObject[] CoinStock = new GameObject[50];
    public static Coin[] co = new Coin[50];
    public static void pmove(int rc, float rp)//playerˆÚ“®
    {
        if (ButtonMove.rM && px < xRange[ButtonChil.gameMord] - Main.powerUps[1] * rc + Main.debuf[1] * rc + Main.phases[3] * rp)
        {
            px += 20;
        }
        else if (ButtonMove.lM && px > -xRange[ButtonChil.gameMord] + Main.powerUps[1] * rc - Main.debuf[1] * rc - Main.phases[3] * rp)
        {
            px -= 20;
        }
        Main.pTra.transform.localPosition = new Vector2(px, -80);
    }
    public static void bWall(int hx)//•Ç‚Ì‚Ô‚Â‚©‚è
    {
        if (Main.warpbool)
        {
            int k = 0;
            if(Random.Range(0, 5) > 2)
            {
                k = 330;
            }
            else
            {
                k = -330;
            }
            if(bx > 330)
            {
                Main.brigi.Sleep();
                if (Random.Range(0, 5) > 2)
                {
                    Main.bTra.transform.localPosition = new Vector2(k, 6.6f);
                }
                else
                {
                    Main.bTra.transform.localPosition = new Vector2(k, 123);
                }
                Main.brigi.AddForce(new Vector2(-0.2f, 0), ForceMode2D.Impulse);
            }
            else if(bx < -330)
            {
                Main.brigi.Sleep();
                if (Random.Range(0, 5) > 2)
                {
                    Main.bTra.transform.localPosition = new Vector2(k, 6.6f);
                }
                else
                {
                    Main.bTra.transform.localPosition = new Vector2(k, 123);
                }
                Main.brigi.AddForce(new Vector2(0.2f, 0), ForceMode2D.Impulse);
            }
        }
        else if (on2e)
        {
            if (bx > 300 - Main.powerUps[2] * 25 + Main.debuf[2] * 25)
            {
                if (Sound.playSESound)
                {
                    Sound.SEplay(0);
                    Sound.playSESound = false;
                }
                Main.brigi.constraints = RigidbodyConstraints2D.FreezePositionX;
                Main.brigi.constraints = RigidbodyConstraints2D.None;
                if (Main.phases[7] == 2 && by < 35 && by > -35)
                {
                    Debug.Log(by);
                    Main.panchiTra[0].transform.localPosition = new Vector2(135, 0);
                    Main.brigi.AddForce(new Vector2(-1, 0.5f), ForceMode2D.Impulse);
                    Main.brigi.AddTorque(1, ForceMode2D.Impulse);
                }
                else
                {
                    Main.brigi.AddForce(new Vector2(-0.2f * Main.phases[4] * Main.phases[5], 0), ForceMode2D.Impulse);
                    Main.brigi.AddTorque(0.6f, ForceMode2D.Impulse);
                }
                on2e = false;
            }
            else if (bx < -300 + Main.powerUps[2] * 25 - Main.debuf[2] * 25)
            {
                if (Sound.playSESound)
                {
                    Sound.SEplay(0);
                    Sound.playSESound = false;
                }
                Main.brigi.constraints = RigidbodyConstraints2D.FreezePositionX;
                Main.brigi.constraints = RigidbodyConstraints2D.None;
                if (Main.phases[7] == 2 && by < 35 && by > -35)
                {
                    Main.panchiTra[1].transform.localPosition = new Vector2(-135, 0);
                    Main.brigi.AddForce(new Vector2(1, 0.5f), ForceMode2D.Impulse);
                    Main.brigi.AddTorque(-1, ForceMode2D.Impulse);
                }
                else
                {
                    Main.brigi.AddForce(new Vector2(0.2f * Main.phases[4] * Main.phases[5], 0), ForceMode2D.Impulse);
                    Main.brigi.AddTorque(-0.6f, ForceMode2D.Impulse);
                }
                on2e = false;
            }
            else if(by > 450 - Main.powerUps[2] * 50 + Main.debuf[2] * 50)//“Vˆä‚Ì‚Ô‚Â‚©‚è
            {
                if (Sound.playSESound)
                {
                    Sound.SEplay(0);
                    Sound.playSESound = false;
                }
                Main.brigi.constraints = RigidbodyConstraints2D.FreezePositionY;
                Main.brigi.constraints = RigidbodyConstraints2D.None;
                Main.brigi.AddForce(new Vector2(0, -0.1f * Main.phases[4] * Main.phases[0] * Main.phases[5] *( Main.phases[6] + 1)), ForceMode2D.Impulse);
                on2e = false;
            }
        }
        else if (bx < 290 - Main.powerUps[2] * 50 + Main.debuf[2] * 50 + Main.phases[3] * 1.875f && bx > -290 + Main.powerUps[2] * 50 - Main.debuf[2] * 50)
        {
            on2e = true;
            Sound.playSESound = true;
        }
    }
    public static void get()
    {
        bx = (int)Main.bTra.transform.localPosition.x;
        by = (int)Main.bTra.transform.localPosition.y;
        px = (int)Main.pTra.transform.localPosition.x;
    }
    public static void Torque()
    {
        if (bx > px)
        {
            Main.brigi.AddTorque(-1.4f, ForceMode2D.Impulse);
        }
        else
        {
            Main.brigi.AddTorque(1.4f, ForceMode2D.Impulse);
        }
    }
    public static void Reonce(float line)
    {
        if (by > line)
        {
            once = true;
        }
    }
    public static void BackGround()
    {
        if (timeCount < 110)
        {
            if (timeCount > 50 * (backIndex + 1))
            {
                backIndex++;
            }
            GameBackImage.sprite = BackImages[backIndex];
        }
    }
    public static void Coinplus(int under, int over)
    {
        int amount = Random.Range(1, 3);
        for (int i = 0; i < amount; i++)
        {
            Main.coins[coindex].SetActive(true);
            float x = Random.Range(-300, 300);
            float y = Random.Range(under, over);
            Main.coinTrans[coindex].transform.localPosition = new Vector2(x, y);
            coindex++;
            if(coindex == 50)
            {
                coindex = 0;
            }
        }
    }
    public static void DifChecker()
    {
        if(timeCount > dif)
        {
            Main.brigi.gravityScale += 0.1f;
            addY += 0.07f;
            dif += 20;
        }
    }
    public static void SpeCoin()
    {
        if (numberCoin.coinumber == 0 && nc)
        {
            Main.numberCoins[0].SetActive(true);
            float x = Random.Range(-300, 300);
            float y = Random.Range(-40, 70);
            Main.numberTrans[0].transform.localPosition = new Vector2(x, y);
            nc = false;
        }
    }
    public static void MinusSpeCoint()
    {
        if (minusNumberCoin.minusCoinNumber == 0 && mc)
        {
            Main.minusCoins[0].SetActive(true);
            float x = Random.Range(-80, 80);
            float y = Random.Range(-40, 70);
            Main.minusNumberTrans[0].transform.localPosition = new Vector2(x, y);
            mc = false;
        }
    }
    public static void NextCalc()
    {
        if(timeCount > 899)
        {
            Main.comentTex.text = "EXCELENT!";
            Main.nextTex.text = "";
        }
        else if(timeCount > 499)
        {
            Main.comentTex.text = "GREAT!";
            Main.nextTex.text = "";
        }
        else if(timeCount > 349)
        {
            Main.nextTex.text = (500 - timeCount).ToString();
        }
        else if(timeCount > 99)
        {
            Main.nextTex.text = (350 - timeCount).ToString();
        }
        else
        {
            Main.nextTex.text = (100 - timeCount).ToString();
        }
    }
}