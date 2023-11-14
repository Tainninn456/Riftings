using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// ボール自体のscript
/// </summary>
public class Newboal : MonoBehaviour
{

    const string playerTagName = "Player";
    const string deathTagName = "Death";
    const string wallTagName = "wall";

    [Header("ボールを蹴る際の増えるカウント")]
    [SerializeField] int kickCountAddValue;
    [Header("重力を変更する境界")]
    [SerializeField] int gravityChangePoint;
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
    [SerializeField] float wallReflectPower;
    [Header("ギミック用：横方向の風力")]
    [SerializeField] float windPower;

    [SerializeField] float ballBigScaleValue;

    [SerializeField] float ballSmallScaleValue;

    [SerializeField] float defaultBallScaleValue;

    [Header("スケール変更の速度")]
    [SerializeField] float animSpeed;

    [SerializeField] int[] backChangeValues;

    [SerializeField] Transform[] scalePosition;

    [Header("Initializeの参照")]
    [SerializeField] DataReciver initialData;

    [Header("ゲーム内データ群へのアクセス")]
    [SerializeField] GameObject managerInformation;

    [Header("アクション全般へのアクセス")]
    [SerializeField] GameObject actions;

    [Header("エフェクトへのアクセス")]
    [SerializeField] GameObject hitEffect;

    //managerInformationの内容をstart関数にて取得
    private InGameStockData gameDatas;
    private coinManager cMane;
    private TextAction textAction;
    private ImageAction imageAction;
    private DataAction dataAction;

    //ボールのコンポーネントを取得
    private Rigidbody2D boalRig;
    private Transform boalTra;
    private SpriteRenderer boalSprite;

    //エフェクトのコンポーネントを取得
    private Transform effectTrans;
    private ParticleSystem hitParticle;

    //ボールのギミックに関すること
    private bool gimicing;
    private bool gravityGimicing;

    //ボールの初期ポジション
    private Vector2 ballDefaultPosition;

    //
    private int playIndex;
    private int judgeValueIndex;

    //残基を示す
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
        textAction = actions.GetComponent<TextAction>();
        imageAction = actions.GetComponent<ImageAction>();
        dataAction = actions.GetComponent<DataAction>();

        //エフェクトの方のコンポーネントを取得
        effectTrans = hitEffect.GetComponent<Transform>();
        hitParticle = hitEffect.GetComponent<ParticleSystem>();

        //イニシャライザーからの取得
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

        //プレイヤーに衝突した時の処理
        if (collision.gameObject.CompareTag(playerTagName))
        {
            gameDatas.kickCount += kickCountAddValue;
            textAction.KickCountDisplay(gameDatas.kickCount);
            EffectAction(boalTra.position);

            //キック時の計算
            Vector2 kickVelocity = boalRig.velocity.normalized;
            float newValue = Mathf.Lerp(0.6f, 1, (kickVelocity.y + 1) / 2f);
            kickVelocity.y = newValue;
            boalRig.AddForce(kickVelocity * kickDefault, ForceMode2D.Impulse);
            int judgeValue = gameDatas.kickCount;
            if(judgeValue > gravityChangePoint) 
            {
                gravityChangePoint += gravityfrequency;
                GravityChanger(0);
            }
            if (judgeValue > backChangeValues[judgeValueIndex])
            {
                if(judgeValueIndex < backChangeValues.Length)
                {
                    judgeValueIndex++;
                    imageAction.BackGroundChanger(judgeValueIndex - 1);
                }
            }
            AudioManager.instance.PlaySE(AudioManager.SE.kick);
        }
        //デススペースに衝突した時の処理
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
        //壁との衝突処理(ギミック発動中でなければ発生しない処理)
        else if (collision.gameObject.CompareTag(wallTagName))
        {
            AudioManager.instance.PlaySE(AudioManager.SE.wall);
            EffectAction(boalTra.position);
            if (gimicing)
            {
                boalRig.AddForce(boalRig.velocity.normalized * wallReflectPower, ForceMode2D.Impulse);
            }
        }
    }

    public void WindAttackGimic()
    {
        int rand = Random.Range(0, 11);
        boalRig.velocity = new Vector2(0, boalRig.velocity.y);
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
                boalTra.DOScale(new Vector3(ballBigScaleValue, ballBigScaleValue, 1), animSpeed);
                //boalTra.localScale = new Vector3(ballBigScaleValue, ballBigScaleValue, 1);
                break;
            case 1:
                boalTra.DOScale(new Vector3(ballSmallScaleValue, ballSmallScaleValue, 1), animSpeed);
                //boalTra.localScale = new Vector3(ballSmallScaleValue, ballSmallScaleValue, 1);
                break;
            case 2:
                boalTra.DOScale(new Vector3(defaultBallScaleValue, defaultBallScaleValue, 1), animSpeed);
                //boalTra.localScale = new Vector3(defaultBallScaleValue, defaultBallScaleValue, 1);
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

    //エフェクト実行
    private void EffectAction(Vector2 targetPosition)
    {
        effectTrans.position = targetPosition;
        hitParticle.Play();
    }
}
