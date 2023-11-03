using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;

public class ImageAction : MonoBehaviour
{
    const string unActiveTagName = "ActiveObject";
    const string scoreImageGetName = "AllScoreImageComponentGeter";
    const string clothImageGetName = "AllClothImageComponentGeter";
    const string sportSpritesGetName = "AllSportSpritesGeter";
    const int sportTypeCount = 9;
    const int bomAnimationMaxScale = 3;
    const float animSpeed = 1.3f;

    //script内での定数
    readonly string[] resourceSelect = new string[9] { "soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    [Header("メインメニュー")]
    [SerializeField] Image[] scoreImages;
    [SerializeField] Image[] clothImages;
    [SerializeField] GameObject[] rockObjs;

    [SerializeField] DataAction dataAction;

    [SerializeField] shopPrices shopDatas;

    [SerializeField]
    public Sprite[] sportSprites;

    [Header("インゲーム")]
    [SerializeField] GameObject endAnimationBom;

    [SerializeField] InGameStockData gameDatas;

    [SerializeField] GameObject ingameAllObjects;

    [SerializeField] SystemAction systemAction;

    public void clothButtonImageChanger(int index)
    {
        for(int i = 0; i < clothImages.Length; i++)
        {
            clothImages[i].sprite = sportSprites[index * sportTypeCount + i];
        }
    }

    public void Animation(Vector2 lastBallPosition)
    {
        Transform bomTra = endAnimationBom.GetComponent<Transform>();
        bomTra.position = lastBallPosition;
        bomTra.DOScale(new Vector3(bomAnimationMaxScale, bomAnimationMaxScale, bomAnimationMaxScale), animSpeed).OnComplete(() => { ingameAllObjects.SetActive(false); systemAction.PanelMove(SystemAction.MoveDirection.over, 0); });
    }

    public void RockDataIntoImage(int achiveIndex)
    {
        Data useData = dataAction.DataCopy();
        foreach(GameObject ob in rockObjs)
        {
            ob.SetActive(true);
        }
        for(int i = 0; i < useData.clothAchive[achiveIndex]; i++)
        {
            rockObjs[i].SetActive(false);
        }
    }
#if UNITY_EDITOR
    /// <summary>
    /// エディタ上実行関数
    /// </summary>
    [ContextMenu(scoreImageGetName)]
    private void AllScoreImageComponentGeter()
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
        Array.Resize<Image>(ref scoreImages, texs.Count);
        for (int i = 0; i < texs.Count; i++)
        {
            scoreImages[i] = texs[i];
        }
    }

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
