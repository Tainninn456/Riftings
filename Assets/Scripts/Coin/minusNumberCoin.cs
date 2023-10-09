using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minusNumberCoin : MonoBehaviour
{
    public static int minusCoinNumber; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            Main.coinCount += (int)(2 * Main.memory.CoinLevel * (Main.powerUps[0] + 1) / ( Main.debuf[0] + 1));
            minusCoinNumber++;
            if (minusCoinNumber > 6)
            {
                Main.minusCoins[0].SetActive(true);
                float x = Random.Range(-300, 300);
                float y = Random.Range(-40, 150);
                Main.minusNumberTrans[0].transform.localPosition = new Vector2(x, y);
                minusCoinNumber = 0;
                Sound.SEplay(10);
                Main.minuslot = Random.Range(1, 4);
                for (int i = 0; i < 4; i++)
                {
                    Main.powerUps[i] = 0;
                }
                if (Main.debuf[1] == 0)
                {
                    Main.pTra.localScale = new Vector3(4, 2, 1);
                    Main.phases[3] = 1;
                    if (Main.situation == 6)
                    {
                        Main.situation = 0;
                    }
                }
                if(Main.debuf[2] == 0)
                {
                    Main.bTra.localScale = new Vector3(2.1f, 1.2f, 1);
                }
                numberCoin.coinumber = 0;
                for(int i = 0; i < 7; i++)
                {
                    Main.numberCoins[i].SetActive(false);
                }
                PlayParent.mc = true;
                PlayParent.nc = true;
            }
            else
            {
                Main.minusCoins[minusCoinNumber].SetActive(true);
                float x = Random.Range(-180, 180);
                float y = Random.Range(20, 150);
                Main.minusNumberTrans[minusCoinNumber].transform.localPosition = new Vector2(x, y);
            }
            gameObject.SetActive(false);
            Sound.SEplay(8);
            Main.coincounter.text = Main.coinCount.ToString();
        }
    }
}
