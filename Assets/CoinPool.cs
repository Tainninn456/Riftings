using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinPool : MonoBehaviour
{
    private static int unActiveCount;
    private static List<GameObject> coinPoolUnActives = new List<GameObject>();
    private static List<GameObject> coinPoolActives = new List<GameObject>();

    [Header("コイン生成場所のx軸最低値")]
    [SerializeField] float coinXposUnder;
    [Header("コイン生成場所のx軸最高値")]
    [SerializeField] float coinXposOver;
    [Header("コイン生成場所のy軸最低値")]
    [SerializeField] float coinYposUnder;
    [Header("コイン生成場所のy軸最高値")]
    [SerializeField] float coinYposOver;

    private ObjectPool<GameObject> coinPool;

    private GameObject createCoinInformation;

    //coinInformationに情報を渡す
    public void CoinInformationInput(GameObject info)
    {
        createCoinInformation = info;
    }

    void Awake()
    {
        coinPool = new ObjectPool<GameObject>(OnCreatePooledObject, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }

    GameObject OnCreatePooledObject()
    {
        return Instantiate(createCoinInformation);
    }

    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetGameObject()
    {
        GameObject obj = coinPool.Get();
        Transform tf = obj.transform;
        tf.position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));

        return obj;
    }

    public void ReleaseGameObject(GameObject obj)
    {
        coinPool.Release(obj);
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
