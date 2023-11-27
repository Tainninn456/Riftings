using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageInput : InputParent
{
    //ContextMenu�Ŏg�p����֐���string��
    const string methodClothMoneyName = "ClothChangeButtonsGetter";

    [Header("ImageAction�N���X�̎Q��")]
    [SerializeField] ImageAction imageAction;

    [Header("�����ւ��w���ɂĎg�p����{�^��")]
    [SerializeField] Button[] clothChangeButtons;

    private void Start()
    {
        //�����ւ��w���{�^���̂��ꂼ���ImageAction�N���X���̊֐����Ăяo���悤�Ɋ��蓖�Ă�
        for(int i = 0; i < clothChangeButtons.Length; i++)
        {
            int indexNumber = i;
            clothChangeButtons[i].onClick.AddListener(() => imageAction.clothButtonImageChanger(indexNumber));
        }
    }
#if UNITY_EDITOR
    //�����ւ��w���̍ۂɎg�p����{�^���̃R���|�[�l���g���擾����֐�
    [ContextMenu(methodClothMoneyName)]
    private void ClothChangeButtonsGetter()
    {
        var buttonArray = base.ButtonGetter();
        Array.Resize<Button>(ref clothChangeButtons, buttonArray.Length);
        for (int i = 0; i < buttonArray.Length; i++)
        {
            clothChangeButtons[i] = buttonArray[i];
        }
    }
#endif
}
