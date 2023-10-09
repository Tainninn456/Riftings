using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberCoin : MonoBehaviour
{
    public static int coinumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            Main.coinCount += (int)(2 * Main.memory.CoinLevel * (Main.powerUps[0] + 1) / (Main.debuf[0] + 1));
            coinumber++;
            if (coinumber > 6)
            {
                Main.numberCoins[0].SetActive(true);
                float x = Random.Range(-300, 300);
                float y = Random.Range(-40, 150);
                Main.numberTrans[0].transform.localPosition = new Vector2(x, y);
                coinumber = 0;
                Sound.SEplay(10);
                Main.slot = Random.Range(1, 4);
                for (int i = 0; i < 4; i++)
                {
                    Main.debuf[i] = 0;
                }
                if (Main.powerUps[1] == 0)
                {
                    Main.pTra.localScale = new Vector3(4, 2, 1);
                    Main.phases[3] = 1;
                    if (Main.situation == 6)
                    {
                        Main.situation = 0;
                    }
                }
                if(Main.powerUps[2] == 0)
                {
                    Main.bTra.localScale = new Vector3(2.1f, 1.2f, 1);
                }
                minusNumberCoin.minusCoinNumber = 0;
                for(int i = 0; i < 7; i++)
                {
                    Main.minusCoins[i].SetActive(false);
                }
                PlayParent.mc = true;
                PlayParent.nc = true;
            }
            else
            {
                Main.numberCoins[coinumber].SetActive(true);
                float x = Random.Range(-180, 180);
                float y = Random.Range(20, 150);
                Main.numberTrans[coinumber].transform.localPosition = new Vector2(x, y);
            }
            gameObject.SetActive(false);
            Sound.SEplay(8);
            Main.coincounter.text = Main.coinCount.ToString();
        }
    }
}
