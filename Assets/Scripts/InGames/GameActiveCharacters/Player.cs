using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゲームプレイ中のプレイヤーのクラス
/// </summary>
public class Player : MonoBehaviour
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

    //プレイヤーのトランスフォーム
    Transform playerTransform;

    //プレイヤーのrigidbody
    Rigidbody2D playerRigid;

    //進む方向に関するbool値
    bool right; bool left;

    private void FixedUpdate()
    {
        //右方向移動
        if (right)
        {
            if (playerRigid.velocity.x < 0)
            {
                playerRigid.velocity = new Vector2(0, 0);
            }
            playerRigid.velocity = new Vector2(movePowerX, 0);
        }
        //左方向移動
        else if (left)
        {
            playerRigid.velocity = new Vector2(-1 * movePowerX, 0);
        }
        //滑り防止
        else
        {
            playerRigid.velocity = Vector2.zero;
        }
    }

    //ボタン押下で呼び出され進行中を知らせる関数
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

    //進行の終了を知らせる関数
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

    //PlayerSelecterクラスから初期化されるための関数
    public void PlayerComponentInserter(Rigidbody2D inputRig, Transform inputTra)
    {
        playerRigid = inputRig;
        playerTransform = inputTra;
    }

    //スケール変更ギミックを実行する関数
    public void ScaleChanger(int scaleNumber)
    {
        switch (scaleNumber)
        {
            //通常の大きさ
            case 0:
                playerTransform.DOScale(new Vector3(defaultPlayerScaleValue, defaultPlayerScaleValue, 1), animSpeed);
                break;
            //大きいバージョン
            case 1:
                playerTransform.DOScale(new Vector3(playerBigScaleValue, playerBigScaleValue, 1), animSpeed);
                break;
            //小さいバージョン
            case 2:
                playerTransform.DOScale(new Vector3(playerSmallScaleValue, playerSmallScaleValue, 1), animSpeed);
                break;
        }
    }
}
