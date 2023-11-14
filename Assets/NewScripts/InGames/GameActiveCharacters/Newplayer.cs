using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

/// <summary>
/// ゲームプレイ中のプレイヤー
/// </summary>
public class Newplayer : MonoBehaviour
{
    //エディタ上実行関数
    const string scaleGetMethodName = "ScalePositionGetter";

    [Header("プレイヤーの移動速度")]
    [SerializeField] int movePowerX;

    [Header("ギミックによるプレイヤーの拡大スケール")]
    [SerializeField] float playerBigScaleValue;

    [Header("ギミックによるプレイヤーの縮小スケール")]
    [SerializeField] float playerSmallScaleValue;

    [Header("プレイヤーの通常時のスケール")]
    [SerializeField] float defaultPlayerScaleValue;

    [Header("スケール変更の速度")]
    [SerializeField] float animSpeed;

    [Header("拡大縮小時の位置調整用")]
    [SerializeField] Transform[] scalePosition;

    Transform tra;

    Rigidbody2D rig;

    bool right; bool left;

    private int sportIndex;

    private void FixedUpdate()
    {
        //右方向移動
        if (right)
        {
            if (rig.velocity.x < 0)
            {
                rig.velocity = new Vector2(0, 0);
            }
            rig.velocity = new Vector2(movePowerX, 0);
        }
        //左方向移動
        else if (left)
        {
            rig.velocity = new Vector2(-1 * movePowerX, 0);
        }
        //滑り防止
        else
        {
            rig.velocity = Vector2.zero;
        }
    }

    //入力及び移動
    public void MoveDirectionStart(int directionNumber)
    {
        switch (directionNumber)
        {
            case 0:
                left = true;
                break;
            case 1:
                right = true;
                break;
        }
    }

    public void MoveDirectionEnd(int directionNumber)
    {
        switch (directionNumber)
        {
            case 0:
                left = false;
                break;
            case 1:
                right = false;
                break;
        }
    }

    //初期情報入力
    public void PlayerComponentInserter(Rigidbody2D inputRig, Transform inputTra, int inputSportIndex)
    {
        rig = inputRig;
        tra = inputTra;
        sportIndex = inputSportIndex;
    }

    //スケール変更ギミック
    public void ScaleChanger(int scaleNumber)
    {
        Vector2 traStock = tra.position;
        float playerTraXpos = traStock.x;

        float scalePositionLeft = scalePosition[2 * sportIndex].position.x;
        float scalePositionRight = scalePosition[2 * sportIndex + 1].position.x;
        if (playerTraXpos < scalePositionLeft)
        {
            tra.position = new Vector2(scalePositionLeft, traStock.y);
        }
        else if(playerTraXpos > scalePositionRight)
        {
            tra.position = new Vector2(scalePositionRight, traStock.y);
        }
        switch (scaleNumber)
        {
            //通常の大きさ
            case 0:
                tra.DOScale(new Vector3(defaultPlayerScaleValue, defaultPlayerScaleValue, 1), animSpeed);
                //tra.localScale = new Vector3(defaultPlayerScaleValue, defaultPlayerScaleValue, 1);
                break;
            //大きいバージョン
            case 1:
                tra.DOScale(new Vector3(playerBigScaleValue, playerBigScaleValue, 1), animSpeed);
                //tra.localScale = new Vector3(playerBigScaleValue, playerBigScaleValue, 1);
                break;
            //小さいバージョン
            case 2:
                tra.DOScale(new Vector3(playerSmallScaleValue, playerSmallScaleValue, 1), animSpeed);
                //tra.localScale = new Vector3(playerSmallScaleValue, playerSmallScaleValue, 1);
                break;
        }
    }


#if UNITY_EDITOR
    //なぜか順番が保障されない
    [ContextMenu(scaleGetMethodName)]
    private void ScalePositionGetter()
    {
        List<Transform> scalePositions = new List<Transform>();
        foreach(var trans in Selection.gameObjects)
        {
            scalePositions.Add(trans.GetComponent<Transform>());
        }
        Array.Resize<Transform>(ref scalePosition, scalePositions.Count);
        for(int i = 0; i < scalePosition.Length; i++)
        {
            scalePosition[i] = scalePositions[i];
        }
    }
#endif
}
