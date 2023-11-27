using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの初期化を担当
/// </summary>
public class PlayerSelecter : MonoBehaviour
{
    [Header("initializeの参照")]
    [SerializeField] DataReciver initialData;

    [Header("実際のプレイヤーの参照")]
    [SerializeField] Newplayer player;
    void Start()
    {
        GameObject mine = gameObject;
        Transform parentTrans = mine.GetComponent<Transform>();
        List<GameObject> objs = new List<GameObject>();
        int childAmount = mine.transform.childCount;
        //プレイヤーのsprite&collision用を全てfalseにする
        for (int i = 0; i < childAmount; i++)
        {
            objs.Add(parentTrans.GetChild(i).gameObject);
            objs[i].SetActive(false);
        }
        //使用するものだけtrueにする
        GameObject playerObj = objs[initialData.sportType];
        playerObj.SetActive(true);
        player.PlayerComponentInserter(playerObj.GetComponent<Rigidbody2D>(), playerObj.GetComponent<Transform>());
    }
}
