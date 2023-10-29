using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelecter : MonoBehaviour
{
    [Header("initializeの参照")]
    [SerializeField] DataReciver initialData;

    [Header("実際のプレイヤーの参照")]
    [SerializeField] Newplayer player;
    //プレイヤーの格納数
    const int childCount = 9;
    void Start()
    {
        GameObject mine = gameObject;
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            objs.Add(mine.transform.GetChild(i).gameObject);
            objs[i].SetActive(false);
        }
        GameObject playerObj = objs[initialData.sportType];
        playerObj.SetActive(true);
        player.PlayerComponentInserter(playerObj.GetComponent<Rigidbody2D>(), playerObj.GetComponent<Transform>(), initialData.sportType);
    }
}
