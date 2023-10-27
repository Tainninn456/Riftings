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

    private InGameStockData gameDatas;

    private CoinInformation coinInfo;

    [Header("データ系へのアクセス")]
    [SerializeField] GameObject managerInformation;

    [Header("テキストへのアクセス")]
    [SerializeField] TextAction textAction;

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

        //コンポーネントを取得
        gameDatas = managerInformation.GetComponent<InGameStockData>();
        coinInfo = managerInformation.GetComponent<CoinInformation>();
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
            gameDatas.kickCount++;
            textAction.KickCountDisplay(gameDatas.kickCount);
        }
        else if (collision.gameObject.CompareTag(deathTagName))
        {
        }
    }

    public Vector2 BallRigChanger(string access, Vector2 inputSpeed)
    {
        switch (access)
        {
            case "get":
                return boalRig.velocity;
            case "set":
                boalRig.velocity = inputSpeed;
                break;
        }
        return new Vector2(50, 50);
    }
}
