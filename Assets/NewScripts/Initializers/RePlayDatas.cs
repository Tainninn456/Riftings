using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ポーズ後のリプレイ時に必要なデータを保持しておくクラス
/// </summary>
public class RePlayDatas : MonoBehaviour
{
    [Header("ボールの参照")]
    [SerializeField] Ball ballReference;
    [Header("コインマネージャーの参照")]
    [SerializeField] coinManager coinManagerReference;

    //ボールのvelocityを保持
    private Vector2 nowBallSpeed = new Vector2(0, 0);

    //ポーズによってゲームを停止する際に実行する関数
    public void StopGame()
    {
        nowBallSpeed = ballReference.BallRigChanger("get", new Vector2(0, 0));
        coinManagerReference.porzBool = true;
    }

    //ポーズ後にゲームを再開する際に実行する関数
    public void ReStartGame()
    {
        ballReference.BallRigChanger("set", nowBallSpeed);
        coinManagerReference.porzBool = false;
    }
}
