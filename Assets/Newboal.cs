using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newboal : MonoBehaviour
{
    //キックの強さ
    [Header("キックの強さを決定(int)")]
    [SerializeField] private int boalBouncePower;
    [Header("縦方向の力を補正する(float)")]
    [SerializeField] private float powerYAdder;

    const string playerTagName = "Player";

    Rigidbody2D rig;
    Transform tra;

    Calculation cal = new Calculation();
    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        tra = gameObject.GetComponent<Transform>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ボールがプレイヤーと衝突した時
        if (collision.gameObject.CompareTag(playerTagName))
        {
            rig.Sleep();
            BounceBoal(collision.gameObject.GetComponent<Transform>().position);
        }
    }

    //跳ね返り処理
    private void BounceBoal(Vector2 playerPosition)
    {
        //プレイヤーとのVector差分を計算
        (float, float) powerStock = cal.BouncePowerCalculation((Vector2)tra.position - playerPosition);
        //キック！！
        rig.AddForce(new Vector2(powerStock.Item1, powerStock.Item2 * powerYAdder) * boalBouncePower, ForceMode2D.Impulse);
    }
}
