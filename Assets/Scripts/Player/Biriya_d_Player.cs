using UnityEngine;
public class Biriya_d_Player : PlayParent
{
    public static void move()//trueはゲームオーバー
    {
        pmove(50, 1.875f);
        bmove();
    }
    public static void bmove()
    {
        float div = 1;
        DifChecker();
        BackGround();
        get();
        if (Main.situation == 6)
        {
            div = 1.2f;
        }
        if (Mathf.Abs(bx - px) > 55 * (Main.powerUps[1] + 1) * (Main.powerUps[2] + 1) / div / (Main.debuf[1] + 1) / (Main.debuf[2] + 1) && by < 0 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70 || Main.over)//ゲームオーバー
        {
            Main.HeartAmount--;
            if (Main.HeartAmount == -1)
            {
                Main.over = true;
                if (Main.memory.GameScores[7] < timeCount)
                {
                    Main.memory.GameScores[7] = (int)timeCount;
                }
                Main.LastScoreTex.text = timeCount.ToString();
                Main.LastCoinTex.text = Main.coinCount.ToString();
                NextCalc();
                Main.BGMstop();
            }
            else
            {
                px = 0;
                Main.Hearts[Main.HeartAmount].SetActive(false);
                Main.brigi.Sleep();
                Main.bTra.transform.localPosition = new Vector2(0, 0);
                Main.pTra.transform.localPosition = new Vector2(0, -80);
            }
        }
        else if (by < 0 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70 - Main.debuf[1] * 70 - Main.debuf[2] * 70 - Main.phases[3] * 1.875f)//衝突時キック
        {
            if (once)
            {
                Coinplus(50, 200);
                Sound.SEplay(7);
                timeCount += 1 * (Main.powerUps[3] + 1) / (Main.debuf[3] + 1);
                Main.brigi.Sleep();
                Main.brigi.AddForce(new Vector2(0, (0.9f + addY) * Main.phases[0] / Main.phases[1]), ForceMode2D.Impulse);
                once = false;
            }
            hx = (int)Main.pTra.transform.localPosition.x;
            Main.brigi.AddForce(new Vector2((bx - px) * 0.001f, 0), ForceMode2D.Impulse);
            Torque();
        }
        else if (Main.situation == 3)
        {
            int bi = 1;
            if (Random.Range(0, 10) > 4)
            {
                bi = -1;
            }
            Main.brigi.AddForce(new Vector2(Main.phases[2] * bi, 0));
        }
        else if (Main.situation == 4 || Main.situation == 5)
        {
            if (by > 180 && on3e)
            {
                Main.brigi.AddForce(new Vector2(Main.phases[2], 0), ForceMode2D.Impulse);
                on3e = false;
            }
            else if(by < 73)
            {
                on3e = true;
            }
            Main.brigi.AddForce(new Vector2(Main.phases[2] * 0.002f, 0), ForceMode2D.Impulse);
        }
        Reonce(-30 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70);
        bWall(hx);
        Main.counter.text = timeCount.ToString();
    }
}
