using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class coinManager : MonoBehaviour
{
    [Header("�R�C���I�u�W�F�N�g")]
    [SerializeField] GameObject coin;
    [Header("���߂ɐ������Ă����R�C���̖���")]
    [SerializeField] int InitialCoinAmount;
    [Header("�R�C���𐶐�����Ԋu")]
    [SerializeField] int coinCreateInterval;

    [Header("�R�C�������ꏊ��x���Œ�l")]
    [SerializeField] float coinXposUnder;
    [Header("�R�C�������ꏊ��x���ō��l")]
    [SerializeField] float coinXposOver;
    [Header("�R�C�������ꏊ��y���Œ�l")]
    [SerializeField] float coinYposUnder;
    [Header("�R�C�������ꏊ��y���ō��l")]
    [SerializeField] float coinYposOver;


    ObjectPool<GameObject> pools;

    private int createCounter;

    bool once;
    private void Awake()
    {
        pools = new ObjectPool<GameObject>(() => Instantiate(coin), (GameObject obj) => obj.SetActive(true), (GameObject obj) => obj.SetActive(false), (GameObject obj) => Destroy(obj), false, 10, 70);
    }

    private void Update()
    {
        if (!once)
        {
            GameManager.Instance.StateChanger(GameManager.State.Play);
            once = true;
        }
        createCounter++;
        if(createCounter > coinCreateInterval)
        {
            //GameObject coinObj = Instantiate(coin, new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver)), Quaternion.identity);
            GameObject coinObj =  pools.Get();
            coinObj.GetComponent<Transform>().position = new Vector2(Random.Range(coinXposUnder, coinXposOver), Random.Range(coinYposUnder, coinYposOver));
            createCounter = 0;
        }
    }
}
