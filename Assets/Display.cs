using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [Header("popの親")]
    public GameObject popParent;
    [Header("popの内容")]
    public GameObject[] pops;
    [Header("着せ替え用スプライトを取得")]
    public Image[] clothes;
}
