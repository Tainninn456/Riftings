using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    private static int unActiveCount;
    private static List<GameObject> coinPoolUnActives = new List<GameObject>();
    private static List<GameObject> coinPoolActives = new List<GameObject>();

    private GameObject ReuseCoin(GameObject coinbase)
    {
        unActiveCount--;
        GameObject returnObj = coinPoolUnActives[0];
        coinPoolUnActives.Remove(returnObj);
        return returnObj;
    }

    private GameObject CreateCoin(GameObject coin)
    {
        GameObject obj = Instantiate(coin);
        coinPoolUnActives.Add(obj);
        coinPoolActives.Add(coin);
        unActiveCount++;
        return obj;
    }

    public GameObject InitialCreate(GameObject coin)
    {
        GameObject obj = Instantiate(coin);
        coinPoolUnActives.Add(obj);
        unActiveCount++;
        return obj;
    }

    //デフォルトの呼び出し関数
    public GameObject UseCoin(GameObject coin)
    {
        if(unActiveCount == 0)
        {
            return CreateCoin(coin);
        }
        else
        {
            return ReuseCoin(coin);
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        coin.SetActive(false);
        coinPoolUnActives.Add(coin);
        unActiveCount++;
    }

    public void UnActivator()
    {
        for(int i = 0; i < coinPoolActives.Count; i++)
        {
            coinPoolActives[i].SetActive(false);

        }
    }

    public void DoActivator()
    {
        Debug.Log("doactive");
        for(int i = 0; i < coinPoolActives.Count; i++)
        {
            coinPoolActives[i].SetActive(true);
        }
    }
}
