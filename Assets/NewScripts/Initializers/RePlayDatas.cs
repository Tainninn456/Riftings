using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayDatas : MonoBehaviour
{
    [Header("ボールscript取得")]
    [SerializeField] NewBall ballScript;
    [Header("コインマネージャー取得")]
    [SerializeField] coinManager cmaneScript;

    private Vector2 nowBallSpeed = new Vector2(0, 0);
    public void StopGame()
    {
        nowBallSpeed = ballScript.BallRigChanger("get", new Vector2(0, 0));
        cmaneScript.porzBool = true;
    }

    public void ReStartGame()
    {
        ballScript.BallRigChanger("set", nowBallSpeed);
        cmaneScript.porzBool = false;
    }
}
