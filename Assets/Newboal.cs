using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールに関するscript
/// </summary>
public class Newboal : MonoBehaviour
{
    [Header("ボールが超えてはいけない速度(int)")]
    [SerializeField] int maxBoalSpeed;
    [Header("ボールの速度を抑える倍率(float)")]
    [SerializeField] float boalSpeedRatio;
    [Header("ボールのスプライト種類")]
    [SerializeField] Sprite[] boalSprites;

    //script内での定数
    readonly string[] resourceSelect = new string[9] { "soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    const string playerTagName = "Player";
    const string deathTagName = "Death";

    private Rigidbody2D boalRig;
    private SpriteRenderer boalSprite;

    private void Start()
    {
        //ボールのコライダー設定
        GameObject obj = gameObject;

        boalSprite = obj.GetComponent<SpriteRenderer>();
        if (!GameManager.Instance.ClothAccess(GameManager.Information.clothbool, GameManager.Instruction.use, false))
        {
            boalSprite.sprite = boalSprites[GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.menu)];
        }
        else 
        {
            boalSprite.sprite = Resources.LoadAll<Sprite>(resourceSelect[GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.game)])[GameManager.Instance.InformationAccess(GameManager.Information.cloth, GameManager.Instruction.use, GameManager.ModeName.soccer, 0)];
        }
        
        boalRig = obj.GetComponent<Rigidbody2D>();
        obj.AddComponent<PolygonCollider2D>();

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
        }
        else if (collision.gameObject.CompareTag(deathTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.state, GameManager.Instruction.insert, GameManager.ModeName.soccer, GameManager.State.result);
        }
    }
}
