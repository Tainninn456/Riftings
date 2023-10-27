using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePlayDatas : MonoBehaviour
{
    [Header("ボールscript取得")]
    [SerializeField] Newboal ballScript;
    [Header("コインマネージャー取得")]
    [SerializeField] coinManager cmaneScript;

    private Vector2 nowBallSpeed = new Vector2(0, 0);
    public void StopWorldInfrence()
    {
        nowBallSpeed = ballScript.BallRigChanger("get", new Vector2(0, 0));
        cmaneScript.porzBool = true;
    }

    public void ReWorldInfrence()
    {
        ballScript.BallRigChanger("set", nowBallSpeed);
        cmaneScript.porzBool = false;
    }
}
