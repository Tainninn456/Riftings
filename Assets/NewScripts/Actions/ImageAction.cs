using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DG.Tweening;

/// <summary>
/// �C���[�W�n�̓�����s���N���X
/// </summary>
public class ImageAction : MonoBehaviour
{
    const string unActiveTagName = "ActiveObject";

    //ContextMenu�ɂĎg�p����֐���string��
    const string clothImageGetName = "AllClothImageComponentGeter";
    const string sportSpritesGetName = "AllSportSpritesGeter";

    //�X�|�[�c�̎�ސ�
    const int sportTypeCount = 9;
    //�Ō�̃{���A�j���[�V�����̍ő�X�P�[��
    const int bomAnimationMaxScale = 3;
    //�����ւ��̃��b�N�̍ő吔
    const int rockMax = 8;
    //�{���A�j���[�V�����̎��s�X�s�[�h
    const float animSpeed = 1.3f;

    //ContextMenu�ɂĈ�ĂɎ擾������ۂɎg�p����萔
    readonly string[] resourceSelect = new string[9] { "soccer/", "tennis/", "baseball/", "boring/", "panchi/", "tabletennis/", "ragby/", "biriyard", "volley/" };

    [Header("���C�����j���[")]
    [Header("�����ւ��̃C���[�W")]
    [SerializeField] Image[] clothImages;
    [Header("�����ւ��̃��b�N�Ɋւ���C���[�W")]
    [SerializeField] GameObject[] rockObjs;

    [Header("�v���C���[�̃f�[�^�ւ̎Q��")]
    [SerializeField] DataAction dataAction;

    [Header("�����ւ���sprite����ێ�")]
    [SerializeField]
    public Sprite[] sportSprites;

    [Header("�C���Q�[��")]
    [Header("�Q�[���I�[�o�[���̃{���I�u�W�F��ێ�")]
    [SerializeField] GameObject endAnimationBom;

    [Header("�Q�[���v���C���Ńf�[�^���W�񂵂Ă����N���X�ւ̎Q��")]
    [SerializeField] InGameStockData gameDatas;

    [Header("�Q�[���v���C���̃I�u�W�F�N�g���܂Ƃ߂��e�I�u�W�F�N�g")]
    [SerializeField] GameObject ingameParentObj;

    [Header("SystemAction�N���X�ւ̎Q��")]
    [SerializeField] SystemAction systemAction;

    [Header("�n�[�g�B�̐e�I�u�W�F�N�g")]
    [SerializeField] GameObject heartParent;

    [Header("�w�i�ύX�pSprite��ێ�")]
    [SerializeField] Sprite[] backImages;

    [Header("�w�i�\���p")]
    [SerializeField] Image backGroundImage;

    [Header("�n�[�g�����p�G�t�F�N�g��Prefab")]
    [SerializeField] GameObject bombEffect;

    //�n�[�g��\�����Ă���I�u�W�F�N�g
    GameObject[] heartDisplays = new GameObject[10];

    //�n�[�g�����G�t�F�N�g������̊i�[��
    GameObject heartEndEffect;

    //�Ώۃv���C�Ŏg�p����n�[�g�̐�
    private int useHeartAmount;

    //�����ւ��Ɏg�p����{�^���̉摜��ύX����֐�
    public void clothButtonImageChanger(int index)
    {
        for(int i = 0; i < clothImages.Length; i++)
        {
            clothImages[i].sprite = sportSprites[index * sportTypeCount + i];
        }
    }

    //�Q�[���I�����̃A�j���[�V���������s����֐�
    public void GameEndAnimation(Vector2 lastBallPosition)
    {
        Transform bomTra = endAnimationBom.GetComponent<Transform>();
        bomTra.position = lastBallPosition;
        bomTra.DOScale(new Vector3(bomAnimationMaxScale, bomAnimationMaxScale, bomAnimationMaxScale), animSpeed).OnComplete(() => { ingameParentObj.SetActive(false); systemAction.PanelMove(SystemAction.MoveDirection.over, 0); AudioManager.instance.PlaySE(AudioManager.SE.ResultSE); });
    }

    //�����ւ��ɂ����郍�b�N��\������֐�
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

    //�Q�[�����Ŏc�@�Ƃ��Ẵn�[�g��\������֐�
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

    //�n�[�g�����炷�ۂ̕\��������֐�
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

    //�X�R�A�ɂ���Ĕw�i��ύX����֐�
    public void BackGroundChanger(int backIndex)
    {
        backGroundImage.sprite = backImages[backIndex];
    }

#if UNITY_EDITOR
    /// <summary>
    /// �G�f�B�^����s�֐�
    /// </summary>

    //�����ւ��ɂĎg�p����{�^����image�R���|�[�l���g���擾����֐�
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

    //�����ւ���sprite��Resources�t�H���_����serialize�ɑ������֐�
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
