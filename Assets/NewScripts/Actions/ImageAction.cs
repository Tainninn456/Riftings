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

    [SerializeField] Image[] scoreImages;
    [SerializeField] Image[] clothImages;


    public void DataIntoText()
    {

    }

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
}
