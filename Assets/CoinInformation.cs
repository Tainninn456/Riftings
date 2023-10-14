using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInformation : MonoBehaviour
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
        coinPoolUnActives.Add(coin);
        coinPoolActives.Add(coin);
        unActiveCount++;
        return coin;
    }

    public GameObject UseCoin(GameObject coin)
    {
        if(unActiveCount == 0)
        {
            return CreateCoin(coin);
        }
        else
        {
            Debug.Log(coinPoolUnActives.Count);
            return ReuseCoin(coin);
        }
    }

    public void ReturnCoin(GameObject coin)
    {
        Debug.Log("##");
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
        for(int i = 0; i < coinPoolActives.Count; i++)
        {
            coinPoolActives[i].SetActive(true);
        }
    }
}
