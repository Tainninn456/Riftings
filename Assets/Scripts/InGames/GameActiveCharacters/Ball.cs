using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// ボールのクラス
/// </summary>
public class Ball : MonoBehaviour
{
    //衝突時のタグ判定に用いる文字列
    const string playerTagName = "Player";
    const string deathTagName = "Death";
    const string wallTagName = "wall";

    [Header("ボールを蹴る際の増えるカウント")]
    [SerializeField] int kickCountAddValue;
    [Header("重力を変更する境界")]
    [SerializeField] int gravityChangePoint;
    [Header("ボールが超えてはいけない速度(int)")]
    [SerializeField] int maxBallSpeed;
    [Header("ボールの速度を抑える倍率(float)")]
    [SerializeField] float ballSpeedRatio;
    [Header("重力が加わる大きさ")]
    [SerializeField] float addGravity;
    [Header("ボールの重力を加える頻度")]
    [SerializeField] int gravityfrequency;
    [Header("ボールを蹴る力")]
    [SerializeField] float kickDefault;
    [Header("ボールの回転力")]
    [SerializeField] float rotationPower;
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

    [Header("スケール変更時の各スケール値")]
    [SerializeField] float ballBigScaleValue;

    [SerializeField] float ballSmallScaleValue;

    [SerializeField] float defaultBallScaleValue;

    [Header("スケール変更の速度")]
    [SerializeField] float animSpeed;

    [Header("背景変更に必要なキックカウントを保持")]
    [SerializeField] int[] backChangeValues;

    [Header("DataReciverクラスの参照")]
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
    private Rigidbody2D ballRigid;
    private Transform ballTrans;
    private SpriteRenderer ballSprite;

    //エフェクトのコンポーネントを取得
    private Transform effectTrans;
    private ParticleSystem hitParticle;

    //ボールのギミックに関すること
    private bool gimicing;
    private bool gravityGimicing;

    //ボールの初期ポジション
    private Vector2 ballDefaultPosition;

    //プレイ中のスポーツの種類
    private int playIndex;
    //背景の表示種類の選択用
    private int judgeValueIndex;

    //残基を示す
    private int useHeartAmount;

    private void Start()
    {
        //ボールのコライダー設定
        GameObject obj = gameObject;

        //ボールのトランスフォームを取得
        ballTrans = obj.GetComponent<Transform>();

        //ボールの着せ替え設定
        ballSprite = obj.GetComponent<SpriteRenderer>();
        ballSprite.sprite = initialData.clothSprite;

        //ボールのcollision用コライダー
        ballRigid = obj.GetComponent<Rigidbody2D>();
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
        ballDefaultPosition = ballTrans.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //超えてはいけない速度を設定
        Vector2 ballVector = ballRigid.velocity;
        if (maxBallSpeed < Mathf.Abs(ballVector.x) + Mathf.Abs(ballVector.y))
        {
            ballRigid.velocity = ballVector * ballSpeedRatio;
        }

        //プレイヤーに衝突した時の処理
        if (collision.gameObject.CompareTag(playerTagName))
        {
            gameDatas.kickCount += kickCountAddValue;
            textAction.KickCountDisplay(gameDatas.kickCount);
            EffectAction(ballTrans.position);

            //キック時の計算
            Vector2 kickVelocity = ballRigid.velocity.normalized;
            float newValueY = Mathf.Lerp(0.6f, 1, (kickVelocity.y + 1) / 2f);
            kickVelocity.y = newValueY;
            float diffierenceValue = ballTrans.position.x - collision.gameObject.transform.position.x;
            float newValueX = Mathf.Lerp(0.1f, 0.5f, Mathf.Abs(kickVelocity.x)) * Mathf.Sign(diffierenceValue);
            kickVelocity.x = newValueX;
            ballRigid.Sleep();
            //ボールに速度を代入する
            ballRigid.velocity = kickVelocity * kickDefault;
            //ボールに回転力を加える
            ballRigid.AddTorque(newValueX * rotationPower, ForceMode2D.Impulse);
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
            ballRigid.Sleep();
            if (useHeartAmount == 0)
            {
                gameDatas.GameOver = true;
                dataAction.GameEndDataSaveStarter(playIndex);
                textAction.GameEndTextDisplay(gameDatas.kickCount, gameDatas.coinCount);
                imageAction.GameEndAnimation(gameObject.GetComponent<Transform>().position);
                AudioManager.instance.StopBGM();
                gameObject.SetActive(false);
            }
            else
            {
                imageAction.DeathDisplay(useHeartAmount);
                useHeartAmount -= 1;
                ballTrans.position = ballDefaultPosition;
            }
        }
        //壁との衝突処理(ギミック発動中でなければ発生しない処理)
        else if (collision.gameObject.CompareTag(wallTagName))
        {
            AudioManager.instance.PlaySE(AudioManager.SE.wall);
            EffectAction(ballTrans.position);
            if (gimicing)
            {
                ballRigid.AddForce(ballRigid.velocity.normalized * wallReflectPower, ForceMode2D.Impulse);
            }
        }
    }

    //ボールを横方向に一時的に
    public void WindAttackGimic()
    {
        int rand = Random.Range(0, 11);
        ballRigid.velocity = new Vector2(0, ballRigid.velocity.y);
        if(rand > 5)
        {
            ballRigid.AddForce(new Vector2(windPower, 0), ForceMode2D.Impulse);
        }
        else
        {
            ballRigid.AddForce(new Vector2(-windPower, 0), ForceMode2D.Impulse);
        }
    }

    //乱気流ギミックを実行する関数
    public void WindRandomAttackGimic()
    {
        ballRigid.AddForce(new Vector2(Random.Range(-randPower, randPower), Random.Range(-randPower, randPower)), ForceMode2D.Force);
    }

    //ボールにかかる重力を変更するギミックを実行する関数
    public void GravityChanger(int grabityType)
    {
        float gravityStuck = 0;
        float kickPowerStuck = 0;
        switch (grabityType)
        {
            //一定頻度で重量が上がる方の処理
            case 0:
                gravityStuck = addGravity;
                kickPowerStuck = addKickPower;
                break;
            //一時的に重量が跳ねあがる方の処理
            case 1:
                gravityStuck = addGravity * gravitymagnification;
                kickPowerStuck = addKickPower * gravitymagnification;
                gravityGimicing = true;
                break;
            //一時的に上がったgravityScaleを戻す処理
            case 2:
                if (!gravityGimicing) { break; }
                gravityStuck = addGravity * gravitymagnification * -1;
                kickPowerStuck = addKickPower * gravitymagnification * -1;
                gravityGimicing = false;
                break;
        }
        ballRigid.gravityScale += gravityStuck;
        kickDefault += kickPowerStuck;
    }

    //キックカウントに値を加算する関数
    public void KickAddValueChanger(int value)
    {
        kickCountAddValue = value;
    }

    //ボールのスケールを変更する関数
    public void BallScaleChanger(int scaleNumber)
    {
        switch (scaleNumber)
        {
            case 0:
                ballTrans.DOScale(new Vector3(ballBigScaleValue, ballBigScaleValue, 1), animSpeed);
                break;
            case 1:
                ballTrans.DOScale(new Vector3(ballSmallScaleValue, ballSmallScaleValue, 1), animSpeed);
                break;
            case 2:
                ballTrans.DOScale(new Vector3(defaultBallScaleValue, defaultBallScaleValue, 1), animSpeed);
                break;
        }
    }

    //壁衝突時の処理を変更する関数
    public void WallGimicStarter(bool wallBool)
    {
        gimicing = wallBool;
    }

    //ポーズにおけるvelocityを操作する関数
    public Vector2 BallRigChanger(string access, Vector2 inputSpeed)
    {
        switch (access)
        {
            case "get":
                return ballRigid.velocity;
            case "set":
                ballRigid.velocity = inputSpeed;
                break;
        }
        return new Vector2(50, 50);
    }

    //衝突時エフェクトを実行する関数
    private void EffectAction(Vector2 targetPosition)
    {
        effectTrans.position = targetPosition;
        hitParticle.Play();
    }
}
