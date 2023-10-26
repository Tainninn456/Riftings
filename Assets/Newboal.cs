using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ボールに関するscript
/// </summary>
public class Newboal : MonoBehaviour
{
    [Header("ボールが超えてはいけない速度(int)")]
    [SerializeField] int maxBoalSpeed;
    [Header("ボールの速度を抑える倍率(float)")]
    [SerializeField] float boalSpeedRatio;

    [Header("Initializeの参照")]
    [SerializeField] DataReciver initialData;

    [Header("インゲームデータストックへのアクセス")]
    [SerializeField] InGameStockData gameDatas;

    [Header("コイン情報へのアクセス")]
    [SerializeField] CoinInformation coinInfo;

    const string playerTagName = "Player";
    const string deathTagName = "Death";

    private Rigidbody2D boalRig;
    private SpriteRenderer boalSprite;

    private void Start()
    {
        //ボールのコライダー設定
        GameObject obj = gameObject;

        //ボールの着せ替え設定
        boalSprite = obj.GetComponent<SpriteRenderer>();
        boalSprite.sprite = initialData.clothSprite;

        //ボールのcollision用コライダー
        boalRig = obj.GetComponent<Rigidbody2D>();
        obj.AddComponent<PolygonCollider2D>();

        //ボールのtrigger用コライダー
        GameObject childobj = obj.transform.GetChild(0).gameObject;
        PolygonCollider2D targetcollider = childobj.AddComponent<PolygonCollider2D>();
        targetcollider.isTrigger = true;
        targetcollider.points = obj.GetComponent<PolygonCollider2D>().points;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //超えてはいけない速度を設定
        Vector2 boalVec = boalRig.velocity;
        if (maxBoalSpeed < Mathf.Abs(boalVec.x) + Mathf.Abs(boalVec.y))
        {
            boalRig.velocity = boalVec * boalSpeedRatio;
        }

        //キック回数を加算
        if (collision.gameObject.CompareTag(playerTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.kick, GameManager.Instruction.add);
            //texDisplay.TextDisplaing(kickTex, GameManager.Instance.InformationAccess(GameManager.Information.kick, GameManager.Instruction.use));
        }
        else if (collision.gameObject.CompareTag(deathTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, GameManager.ModeName.soccer, GameManager.State.result);
        }
    }
}
