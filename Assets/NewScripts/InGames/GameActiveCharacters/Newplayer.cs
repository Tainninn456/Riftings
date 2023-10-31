using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class Newplayer : MonoBehaviour
{
    const string scaleGetMethodName = "ScalePositionGetter";
    const float defaultPlayerScaleValue = 0.15f;

    Transform tra;

    Rigidbody2D rig;

    bool right; bool left;

    private int sportIndex;

    [SerializeField] int movePowerX;

    [SerializeField] float playerBigScaleValue;

    [SerializeField] float playerSmallScaleValue;

    [SerializeField] Transform[] scalePosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
    }

    private void FixedUpdate()
    {
        if (right)
        {
            if (rig.velocity.x < 0)
            {
                rig.velocity = new Vector2(0, 0);
            }
            rig.velocity = new Vector2(movePowerX, 0);
        }
        else if (left)
        {
            rig.velocity = new Vector2(-1 * movePowerX, 0);
        }
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
                tra.localScale = new Vector3(defaultPlayerScaleValue, defaultPlayerScaleValue, 1);
                break;
            //大きいバージョン
            case 1:
                tra.localScale = new Vector3(playerBigScaleValue, playerBigScaleValue, 1);
                break;
            //小さいバージョン
            case 2:
                tra.localScale = new Vector3(playerSmallScaleValue, playerSmallScaleValue, 1);
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
