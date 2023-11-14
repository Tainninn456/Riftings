using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinPool : MonoBehaviour
{

    [Header("�R�C�������ꏊ��x���Œ�l")]
    [SerializeField] float coinXposUnder;
    [Header("�R�C�������ꏊ��x���ō��l")]
    [SerializeField] float coinXposOver;
    [Header("�R�C�������ꏊ��y���Œ�l")]
    [SerializeField] float coinYposUnder;
    [Header("�R�C�������ꏊ��y���ō��l")]
    [SerializeField] float coinYposOver;

    [SerializeField] coinManager cmane;

    private ObjectPool<GameObject> coinPool;

    private GameObject createCoinInformation;

    //coinInformation�ɏ���n��
    public void CoinInformationInput(GameObject info)
    {
        createCoinInformation = info;
    }

    void Awake()
    {
        coinPool = new ObjectPool<GameObject>(OnCreatePooledObjectNormal, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
        cmane.GimicCoinPositionsGetter(coinXposUnder, coinXposOver, coinYposUnder, coinYposOver);
    }
    
    GameObject OnCreatePooledObjectNormal()
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
}
