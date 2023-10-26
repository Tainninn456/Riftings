using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SystemAction : MonoBehaviour
{
    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    const float animaionSpeed = 0.3f;
    const float minScale = 0.05f;
    const int maxScale = 1;

    [Header("���C�����j���[")]
    [Header("�V�[���J�ڎ��ɒ����ւ������擾���邽��")]
    [SerializeField] DataAction useData;

    [Header("�p�l���̑J�ڐ�ʒu")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("���ۂɓ������p�l��")]
    [SerializeField] RectTransform[] movePanel;

    [Header("�T�E���h�̃|�b�v")]
    [SerializeField] GameObject soundPop;

    [Header("�����ւ��̃|�b�v")]
    [SerializeField] GameObject clothPop;

    [Header("�|�b�v�̐e�I�u�W�F�N�g")]
    [SerializeField] GameObject popParent;

    [SerializeField] DataAction dataAction;

    private int sportTypeNumber = 0;
    public enum MoveDirection
    {
        left,
        right
    }

    public enum PopName
    {
        Sound,
        Cloth
    }

    public enum PopOperaition
    {
        up,
        down
    }

    public void PanelMove(MoveDirection directionName, int panelNumber)
    {
        Vector3 leftPosition = new Vector3(0, 0, 0);
        Vector3 rightPosition = new Vector3(0, 0, 0);
        switch (panelNumber)
        {
            //�����ւ��p�l���̏ꍇ
            case 0:
                leftPosition = movePanelPositions[1].position;
                rightPosition = movePanelPositions[2].position;
                break;
            //�X�R�A�p�l���̏ꍇ
            case 1:
                leftPosition = movePanelPositions[0].position;
                rightPosition = movePanelPositions[1].position;
                break;
        }
        RectTransform myTrans = movePanel[panelNumber];
        switch (directionName)
        {
            case MoveDirection.left:
                myTrans.DOMove(leftPosition, animaionSpeed);
                break;
            case MoveDirection.right:
                myTrans.DOMove(rightPosition, animaionSpeed);
                break;
        }
    }

    //���Lpop�Ɋւ���
    public void PopUp(PopName popName)
    {
        popParent.SetActive(true);
        RectTransform myTrans = null;
        switch (popName)
        {
            case PopName.Sound:
                soundPop.SetActive(true);
                myTrans = soundPop.GetComponent<RectTransform>();
                break;
            case PopName.Cloth:
                clothPop.SetActive(true);
                myTrans = clothPop.GetComponent<RectTransform>();
                break;
        }
        myTrans.DOScale(new Vector3(maxScale, maxScale, maxScale), animaionSpeed);
    }

    public void PopDown(PopName popName)
    {
        GameObject myObj = null;
        switch (popName)
        {
            case PopName.Sound:
                myObj = soundPop;
                break;
            case PopName.Cloth:
                myObj = clothPop;
                break;
        }
        myObj.GetComponent<RectTransform>().DOScale(new Vector3(minScale, minScale, 1), animaionSpeed).OnComplete(() => myObj.SetActive(false)).OnComplete(() => popParent.SetActive(false));
    }

    //�O�����͂���V�[���J�ڂ̊J�n��錾
    public void SceneMoveStarter(int sendSportTypeNumber)
    {
        sportTypeNumber = sendSportTypeNumber;
        SceneMove();
    }
    //���ۂ̃V�[���J��
    private void SceneMove()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene(playSceneName);
    }
    //�V�[���J�ړ��Ŏ󂯓n���f�[�^�̓��e
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //�J�ڐ�V�[���̃I�u�W�F�N�g����
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //�f�[�^�̎擾
        Data stockData = dataAction.DataCopy();
        datareciver.sportType = sportTypeNumber;
        datareciver.clothSprite = dataAction.sportSprites[sportTypeNumber];
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
