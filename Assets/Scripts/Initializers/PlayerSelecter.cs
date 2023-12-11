using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの初期化を行うクラス
/// </summary>
public class PlayerSelecter : MonoBehaviour
{
    [Header("DataReciverクラスの参照")]
    [SerializeField] DataReciver initialData;

    [Header("プレイヤーの参照")]
    [SerializeField] Player player;
    void Start()
    {
        GameObject mine = gameObject;
        Transform parentTrans = mine.GetComponent<Transform>();
        List<GameObject> objs = new List<GameObject>();
        int childAmount = mine.transform.childCount;
        //プレイヤーのsprite&collision用オブジェクトを全て非アクティブにする
        for (int i = 0; i < childAmount; i++)
        {
            objs.Add(parentTrans.GetChild(i).gameObject);
            objs[i].SetActive(false);
        }
        //使用するオブジェクトだけをアクティブにする
        GameObject playerObj = objs[initialData.sportType];
        playerObj.SetActive(true);
        player.PlayerComponentInserter(playerObj.GetComponent<Rigidbody2D>(), playerObj.GetComponent<Transform>());
    }
}
