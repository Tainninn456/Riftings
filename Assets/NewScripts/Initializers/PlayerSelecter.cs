using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̏�������S��
/// </summary>
public class PlayerSelecter : MonoBehaviour
{
    [Header("initialize�̎Q��")]
    [SerializeField] DataReciver initialData;

    [Header("���ۂ̃v���C���[�̎Q��")]
    [SerializeField] Newplayer player;
    void Start()
    {
        GameObject mine = gameObject;
        Transform parentTrans = mine.GetComponent<Transform>();
        List<GameObject> objs = new List<GameObject>();
        int childAmount = mine.transform.childCount;
        //�v���C���[��sprite&collision�p��S��false�ɂ���
        for (int i = 0; i < childAmount; i++)
        {
            objs.Add(parentTrans.GetChild(i).gameObject);
            objs[i].SetActive(false);
        }
        //�g�p������̂���true�ɂ���
        GameObject playerObj = objs[initialData.sportType];
        playerObj.SetActive(true);
        player.PlayerComponentInserter(playerObj.GetComponent<Rigidbody2D>(), playerObj.GetComponent<Transform>());
    }
}
