using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


/// <summary>
/// コインのオブジェクトプールを管理するクラス
/// </summary>
public class CoinPool : MonoBehaviour
{
    //コインに関するオブジェクトプール
    private ObjectPool<GameObject> coinPool;

    //生成するコインの情報を保持する変数
    private GameObject createCoinInformation;

    //コインの生成位置を保持する変数群
    private float coinXposUnder;
    private float coinXposOver;
    private float coinYposUnder;
    private float coinYposOver;

    //coinInformationに情報を渡す関数
    public void CoinInformationInput(GameObject info)
    {
        createCoinInformation = info;
    }

    //coinManagerからコインの生成ポジションを取得する関数
    public void CoinPositionSetter(float xUnderValue, float xOverValue, float yUnderValue, float yOverValue)
    {
        coinXposUnder = xUnderValue;
        coinXposOver = xOverValue;
        coinYposUnder = yUnderValue;
        coinYposOver = yOverValue;
    }
    void Awake()
    {
        coinPool = new ObjectPool<GameObject>(OnCreatePooledObjectNormal, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject);
    }
    
    /// <summary>
    /// コインプール
    /// </summary>
    /// 

    //プール内が空であった場合に実行する関数
    GameObject OnCreatePooledObjectNormal()
    {
        return Instantiate(createCoinInformation);
    }

    //プールにオブジェクトがある場合に実行する関数
    void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    //プールへオブジェクトを返す関数
    void OnReleaseToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    //プール内のオブジェクトを消す関数
    void OnDestroyPooledObject(GameObject obj)
    {
        Destroy(obj);
    }

    //外部からオブジェクトプールへアクセスする関数
    public GameObject GetGameObject()
    {
        GameObject obj = coinPool.Get();
        Transform tf = obj.transform;
        tf.position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));

        return obj;
    }

    //外部からオブジェクトプールへオブジェクトを返す関数
    public void ReleaseGameObject(GameObject obj)
    {
        coinPool.Release(obj);
    }
}
