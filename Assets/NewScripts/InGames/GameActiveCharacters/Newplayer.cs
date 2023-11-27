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


    Transform tra;

    Rigidbody2D rig;

    bool right; bool left;

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
    public void PlayerComponentInserter(Rigidbody2D inputRig, Transform inputTra)
    {
        rig = inputRig;
        tra = inputTra;
    }

    //スケール変更ギミック
    public void ScaleChanger(int scaleNumber)
    {
        switch (scaleNumber)
        {
            //通常の大きさ
            case 0:
                tra.DOScale(new Vector3(defaultPlayerScaleValue, defaultPlayerScaleValue, 1), animSpeed);
                break;
            //大きいバージョン
            case 1:
                tra.DOScale(new Vector3(playerBigScaleValue, playerBigScaleValue, 1), animSpeed);
                break;
            //小さいバージョン
            case 2:
                tra.DOScale(new Vector3(playerSmallScaleValue, playerSmallScaleValue, 1), animSpeed);
                break;
        }
    }
}
