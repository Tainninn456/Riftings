using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinPlayer : MonoBehaviour
{
    [SerializeField] GameObject ballReference;
    [SerializeField] Transform[] ScreenRange;
    [SerializeField] float tapReflectRange;
    [SerializeField] float slopeJudgeValue;
    [SerializeField] float defaultShotDirection;

    private Transform ballTransReference;
    private pinBall ballScriptReference;

    private float[] ScreenRangePosition = new float[4];

    private bool one;

    private void Start()
    {
        ballTransReference = ballReference.GetComponent<Transform>();
        ballScriptReference = ballReference.GetComponent<pinBall>();
        for (int i = 0; i < 2; i++)
        {
            ScreenRangePosition[i] = ScreenRange[i].position.x;
        }
        for (int i = 0; i < 2; i++)
        {
            ScreenRangePosition[i + 2] = ScreenRange[i + 2].position.y;
        }
    }
    void Update()
    {
        //タップによる吹っ飛ばし
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.mousePosition;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            if (worldPos.x >= ScreenRangePosition[0] && worldPos.x <= ScreenRangePosition[1]
                && worldPos.y >= ScreenRangePosition[2] && worldPos.y <= ScreenRangePosition[3])
            {
                //Debug.Log(worldPos);
                //Debug.Log((Vector2)ballTransReference.position);
                //タップ位置とボールまでの距離が関数実行範囲内
                if (Vector2.Distance(ballTransReference.position, worldPos) < tapReflectRange)
                {
                    Vector2 directionStock = (Vector2)ballTransReference.position - worldPos;
                    //ボールの位置がタップ位置より低い場合反転
                    if (directionStock.y < 0)
                    {
                        directionStock = new Vector2(directionStock.x, -1 * directionStock.y);
                    }
                    //ボールの位置とタップ位置に依存する傾きが低い場合の修正処理
                    Vector2 returnDirection = GetSlope(directionStock.normalized, worldPos.normalized);
                    ballScriptReference.ShotBall(returnDirection.normalized);
                }
            }
        }
    }
    //傾きが小さい場合修正を行う関数
    private Vector2 GetSlope(Vector2 targetDirection, Vector2 myPosition)
    {
        float deltaY = targetDirection.y - myPosition.y;
        float deltaX = targetDirection.x - myPosition.x;

        float slope = Mathf.Abs(deltaY / deltaX);
        Debug.Log(slope);

        if (slope < slopeJudgeValue)
        {
            Debug.Log("修正");
            return targetDirection;
        }
        
        //正方向の跳ね返り
        if(deltaX > 0)
        {
            //Debug.Log("goplus");
            return new Vector2(defaultShotDirection, defaultShotDirection);
        }
        //負方向の跳ね返り
        else
        {
            //Debug.Log("gominus");
            return new Vector2(-1 * defaultShotDirection, defaultShotDirection);
        }
    }
}
