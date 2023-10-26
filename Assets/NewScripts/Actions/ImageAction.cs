using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ImageAction : MonoBehaviour
{
    const string unActiveTagName = "ActiveObject";
    const string scoreImageGetName = "AllScoreImageComponentGeter";
    const string clothImageGetName = "AllClothImageComponentGeter";
    const string sportSpritesGetName = "AllSportSpritesGeter";
    const int sportTypeCount = 9;

    //script���ł̒萔
    readonly string[] resourceSelect = new string[9] { "soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    [Header("���C�����j���[")]
    [SerializeField] Image[] scoreImages;
    [SerializeField] Image[] clothImages;

    [SerializeField]
    public Sprite[] sportSprites;

    public void clothButtonImageChanger(int index)
    {
        for(int i = 0; i < clothImages.Length; i++)
        {
            clothImages[i].sprite = sportSprites[index * sportTypeCount + i];
        }
    }

    public void DataIntoImage()
    {

    }

    /// <summary>
    /// �G�f�B�^����s�֐�
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
}
