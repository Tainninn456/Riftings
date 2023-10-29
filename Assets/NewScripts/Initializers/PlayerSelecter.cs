using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelecter : MonoBehaviour
{
    [Header("initialize�̎Q��")]
    [SerializeField] DataReciver initialData;

    [Header("���ۂ̃v���C���[�̎Q��")]
    [SerializeField] Newplayer player;
    //�v���C���[�̊i�[��
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