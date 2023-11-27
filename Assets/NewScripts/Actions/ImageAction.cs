using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;

/// <summary>
/// イメージ系の動作を行うクラス
/// </summary>
public class ImageAction : MonoBehaviour
{
    const string unActiveTagName = "ActiveObject";

    //ContextMenuにて使用する関数のstring名
    const string clothImageGetName = "AllClothImageComponentGeter";
    const string sportSpritesGetName = "AllSportSpritesGeter";

    //スポーツの種類数
    const int sportTypeCount = 9;
    //最後のボムアニメーションの最大スケール
    const int bomAnimationMaxScale = 3;
    //着せ替えのロックの最大数
    const int rockMax = 8;
    //ボムアニメーションの実行スピード
    const float animSpeed = 1.3f;

    //ContextMenuにて一斉に取得をする際に使用する定数
    readonly string[] resourceSelect = new string[9] { "soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    [Header("メインメニュー")]
    [Header("着せ替えのイメージ")]
    [SerializeField] Image[] clothImages;
    [Header("着せ替えのロックに関するイメージ")]
    [SerializeField] GameObject[] rockObjs;

    [Header("プレイヤーのデータへの参照")]
    [SerializeField] DataAction dataAction;

    [Header("着せ替えのsprite情報を保持")]
    [SerializeField]
    public Sprite[] sportSprites;

    [Header("インゲーム")]
    [Header("ゲームオーバー時のボムオブジェを保持")]
    [SerializeField] GameObject endAnimationBom;

    [Header("ゲームプレイ内でデータを集約しておくクラスへの参照")]
    [SerializeField] InGameStockData gameDatas;

    [Header("ゲームプレイ中のオブジェクトをまとめた親オブジェクト")]
    [SerializeField] GameObject ingameParentObj;

    [Header("SystemActionクラスへの参照")]
    [SerializeField] SystemAction systemAction;

    [Header("ハート達の親オブジェクト")]
    [SerializeField] GameObject heartParent;

    [Header("背景変更用Spriteを保持")]
    [SerializeField] Sprite[] backImages;

    [Header("背景表示用")]
    [SerializeField] Image backGroundImage;

    [Header("ハート爆発用エフェクトのPrefab")]
    [SerializeField] GameObject bombEffect;

    //ハートを表示しているオブジェクト
    GameObject[] heartDisplays = new GameObject[10];

    //ハート爆発エフェクト生成後の格納先
    GameObject heartEndEffect;

    //対象プレイで使用するハートの数
    private int useHeartAmount;

    //着せ替えに使用するボタンの画像を変更する関数
    public void clothButtonImageChanger(int index)
    {
        for(int i = 0; i < clothImages.Length; i++)
        {
            clothImages[i].sprite = sportSprites[index * sportTypeCount + i];
        }
    }

    //ゲーム終了時のアニメーションを実行する関数
    public void GameEndAnimation(Vector2 lastBallPosition)
    {
        Transform bomTra = endAnimationBom.GetComponent<Transform>();
        bomTra.position = lastBallPosition;
        bomTra.DOScale(new Vector3(bomAnimationMaxScale, bomAnimationMaxScale, bomAnimationMaxScale), animSpeed).OnComplete(() => { ingameParentObj.SetActive(false); systemAction.PanelMove(SystemAction.MoveDirection.over, 0); AudioManager.instance.PlaySE(AudioManager.SE.ResultSE); });
    }

    //着せ替えにおけるロックを表示する関数
    public void RockDataIntoImage(int achiveIndex)
    {
        Data useData = dataAction.DataCopy();
        foreach(GameObject ob in rockObjs)
        {
            ob.SetActive(true);
        }
        int loopAmount;
        if(rockMax == useData.clothAchive[achiveIndex])
        {
            loopAmount = rockObjs.Length;
        }
        else
        {
            loopAmount = useData.clothAchive[achiveIndex];
        }
        for(int i = 0; i < loopAmount; i++)
        {
            rockObjs[i].SetActive(false);
        }
    }

    //ゲーム内で残機としてのハートを表示する関数
    public void HeartDisplay(int heartActiveValue)
    {
        useHeartAmount = heartActiveValue;
        for (int i = 0; i < heartParent.transform.childCount; i++)
        {
            heartDisplays[i] = heartParent.transform.GetChild(i).gameObject;
        }
        for(int i = 0; i < useHeartAmount; i++)
        {
            heartDisplays[i].SetActive(true);
        }
        GameObject objStuck = Instantiate(bombEffect);
        heartEndEffect = objStuck;
        heartEndEffect.transform.parent = heartParent.transform;
    }

    //ハートを減らす際の表示をする関数
    public void DeathDisplay(int heartIndex)
    {
        heartEndEffect.GetComponent<Transform>().position = heartDisplays[useHeartAmount - heartIndex].GetComponent<Transform>().position;
        heartDisplays[useHeartAmount - heartIndex].GetComponent<Transform>().DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0.4f).OnComplete(() =>
        {
            heartEndEffect.GetComponent<ParticleSystem>().Play();
            heartDisplays[useHeartAmount - heartIndex].SetActive(false);
        }
            );
    }

    //スコアによって背景を変更する関数
    public void BackGroundChanger(int backIndex)
    {
        backGroundImage.sprite = backImages[backIndex];
    }

#if UNITY_EDITOR
    /// <summary>
    /// エディタ上実行関数
    /// </summary>

    //着せ替えにて使用するボタンのimageコンポーネントを取得する関数
    [ContextMenu(clothImageGetName)]
    private void AllClothImageComponentGeter()
    {
        List<Image> texs = new List<Image>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<Image>(true);
            foreach (Image ob in children)
            {
                if (ob.tag != unActiveTagName) { continue; }
                texs.Add(ob);
            }
        }
        Array.Resize<Image>(ref clothImages, texs.Count);
        for (int i = 0; i < texs.Count; i++)
        {
            clothImages[i] = texs[i];
        }
    }

    //着せ替えのspriteをResourcesフォルダからserializeに代入する関数
    [ContextMenu(sportSpritesGetName)]
    private void AllSportSpritesGeter()
    {
        sportSprites = new Sprite[sportTypeCount * sportTypeCount];
        for(int i = 0; i < sportTypeCount; i++)
        {
            Sprite[] playerSprites = Resources.LoadAll<Sprite>(resourceSelect[i]);
            for(int j = 0; j < sportTypeCount; j++)
            {
                sportSprites[i * sportTypeCount + j] = playerSprites[j];
            }
        }
    }
#endif
}
