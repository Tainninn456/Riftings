using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TextAction : MonoBehaviour
{
    const string ActiveTagName = "ActiveObject";
    const string methodScoreName = "AllScoreTextComponentGeter";
    const string methodMoneyName = "AllMoneyTextComponentGeter";

    [Header("���C�����j���[")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI[] moneyTexts;

    [Header("�C���Q�[��")]
    [SerializeField] TextMeshProUGUI kickText;
    [SerializeField] TextMeshProUGUI coinText;

    /// <summary>
    /// ���C�����j���[�����s�֐�
    /// </summary>
    public void DataIntoText()
    {

    }

    /// <summary>
    /// �C���Q�[�������s�֐�
    /// </summary>
    
    public void KickCountDisplay(int kickCount)
    {
        kickText.text = kickCount.ToString();
    }

    public void CoinCountDisplay(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }

#if UNITY_EDITOR

    /// <summary>
    /// �G�f�B�^����s�֐�
    /// </summary>
    [ContextMenu(methodScoreName)]
    private void AllScoreTextComponentGeter()
    {
        List<TextMeshProUGUI> texs = new List<TextMeshProUGUI>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI ob in children)
            {
                if(ob.tag != ActiveTagName) {continue; }
                texs.Add(ob);
            }
        }
        Array.Resize<TextMeshProUGUI>(ref scoreTexts, texs.Count);
        for(int i = 0; i < texs.Count; i++)
        {
            scoreTexts[i] = texs[i];
        }
    }

    [ContextMenu(methodMoneyName)]
    private void AllMoneyTextComponentGeter()
    {
        List<TextMeshProUGUI> texs = new List<TextMeshProUGUI>();
        foreach (var rootGameObject in Selection.gameObjects)
        {
            var children = rootGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI ob in children)
            {
                if (ob.tag != ActiveTagName) { continue; }
                texs.Add(ob);
            }
        }
        Array.Resize<TextMeshProUGUI>(ref moneyTexts, texs.Count);
        for (int i = 0; i < texs.Count; i++)
        {
            moneyTexts[i] = texs[i];
        }
    }
#endif
}
