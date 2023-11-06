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
    [Header("重力が加わる大きさ")]
    [SerializeField] float addGravity;
    [Header("ボールの重力を加える頻度")]
    [SerializeField] int gravityfrequency;
    [Header("ボールを蹴る力")]
    [SerializeField] float kickDefault;
    [Header("ボール蹴り力に加える大きさの倍率")]
    [SerializeField] float addKickPower;
    [Header("ギミック用：一時的に上がる重力の倍率")]
    [SerializeField] int gravitymagnification;
    [Header("ギミック用：乱気流用")]
    [SerializeField] float randPower;
    [Header("ギミック用：壁の反射力")]
    [SerializeField] int wallReflectPower;

    [SerializeField] float ballBigScaleValue;

    [SerializeField] float ballSmallScaleValue;

    [SerializeField] Transform[] scalePosition;

    [Header("Initializeの参照")]
    [SerializeField] DataReciver initialData;

    private InGameStockData gameDatas;

    private coinManager cMane;

    [Header("データ系へのアクセス")]
    [SerializeField] GameObject managerInformation;

    [Header("テキストへのアクセス")]
    [SerializeField] TextAction textAction;

    [Header("イメージへのアクセス")]
    [SerializeField] ImageAction imageAction;

    [Header("ファイルデータを管理しているscriptへのアクセス")]
    [SerializeField] DataAction dataAction;

    const string playerTagName = "Player";
    const string deathTagName = "Death";
    const string wallTagName = "wall";

    const float windPower = 1.3f;

    const float defaultBallScaleValue = 0.1f;

    private Rigidbody2D boalRig;
    private Transform boalTra;
    private SpriteRenderer boalSprite;

    private int kickCountAddValue = 1;
    private int gravityChangePoint = 10;
    private bool gimicing;
    private bool gravityGimicing;

    private Vector2 ballDefaultPosition;

    private int playIndex;

    private int useHeartAmount;
    private void Start()
    {
        //ボールのコライダー設定
        GameObject obj = gameObject;

        //ボールのトランスフォームを取得
        boalTra = obj.GetComponent<Transform>();

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
        cMane = managerInformation.GetComponent<coinManager>();

        playIndex = initialData.sportType;
        useHeartAmount = initialData.heartAmount;
        gameDatas.coinMultiplication = initialData.coinLevel;
        imageAction.HeartDisplay(useHeartAmount);
        cMane.CoinValueChanger(1 * gameDatas.coinMultiplication);

        //ボールのスタートのポジションを保持
        ballDefaultPosition = boalTra.position;
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
            gameDatas.kickCount += kickCountAddValue;
            textAction.KickCountDisplay(gameDatas.kickCount);
            boalRig.AddForce(boalRig.velocity.normalized * kickDefault, ForceMode2D.Impulse);
            if(gameDatas.kickCount > gravityChangePoint) 
            {
                gravityChangePoint += gravityfrequency;
                GravityChanger(0);
            }
        }
        else if (collision.gameObject.CompareTag(deathTagName))
        {
            boalRig.Sleep();
            if (useHeartAmount == 0)
            {
                gameDatas.GameOver = true;
                dataAction.GameEndDataSaveStarter(playIndex);
                textAction.GameEndTextDisplay(gameDatas.kickCount, gameDatas.coinCount);
                imageAction.Animation(gameObject.GetComponent<Transform>().position);
                AudioManager.instance.StopBGM();
                gameObject.SetActive(false);
            }
            else
            {
                imageAction.DeathDisplay(useHeartAmount);
                useHeartAmount -= 1;
                boalTra.position = ballDefaultPosition;
            }
        }
        else if (gimicing && collision.gameObject.CompareTag(wallTagName))
        {
            boalRig.AddForce(boalRig.velocity.normalized * wallReflectPower, ForceMode2D.Impulse);
        }
    }

    public void WindAttackGimic()
    {
        int rand = Random.Range(0, 11);
        if(rand > 5)
        {
            boalRig.AddForce(new Vector2(windPower, 0), ForceMode2D.Impulse);
        }
        else
        {
            boalRig.AddForce(new Vector2(-windPower, 0), ForceMode2D.Impulse);
        }
    }

    public void WindRandomAttackGimic()
    {
        boalRig.AddForce(new Vector2(Random.Range(-randPower, randPower), Random.Range(-randPower, randPower)), ForceMode2D.Force);
    }

    public void GravityChanger(int grabityType)
    {
        switch (grabityType)
        {
            //一定頻度で重量が上がる方の処理
            case 0:
                boalRig.gravityScale += addGravity;
                kickDefault += addKickPower;
                break;
            //一時的に重量が跳ねあがる方の処理
            case 1:
                boalRig.gravityScale += addGravity * gravitymagnification;
                kickDefault += addKickPower * gravitymagnification;
                gravityGimicing = true;
                break;
            //一時的に上がったgravityScaleを戻す処理
            case 2:
                if (!gravityGimicing) { break; }
                boalRig.gravityScale -= addGravity * gravitymagnification;
                kickDefault -= addKickPower * gravitymagnification;
                gravityGimicing = false;
                break;
        }
    }

    public void KickAddValueChanger(int value)
    {
        kickCountAddValue = value;
    }

    public void BallScaleChanger(int value)
    {
        Vector2 traStock = boalTra.position;
        float playerTraXpos = traStock.x;

        float scalePositionLeft = scalePosition[2 * playIndex].position.x;
        float scalePositionRight = scalePosition[2 * playIndex + 1].position.x;
        if (playerTraXpos < scalePositionLeft)
        {
            boalTra.position = new Vector2(scalePositionLeft, traStock.y);
        }
        else if (playerTraXpos > scalePositionRight)
        {
            boalTra.position = new Vector2(scalePositionRight, traStock.y);
        }
        switch (value)
        {
            case 0:
                boalTra.localScale = new Vector3(ballBigScaleValue, ballBigScaleValue, 1);
                break;
            case 1:
                boalTra.localScale = new Vector3(ballSmallScaleValue, ballSmallScaleValue, 1);
                break;
            case 2:
                boalTra.localScale = new Vector3(defaultBallScaleValue, defaultBallScaleValue, 1);
                break;
        }
    }
    public void WallGimicStarter(bool wallBool)
    {
        gimicing = wallBool;
    }
    public void GravityGimicStarter()
    {
        gravityGimicing = true;
    }

    //ポーズ中の処理
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
