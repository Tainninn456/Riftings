using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SystemAction : MonoBehaviour
{
    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    //Dotween�A�j���[�V�����̃X�s�[�h
    const float animationSpeed = 0.3f;
    //Dotween���s���̑Ώ�Popup��Transform�ŏ��X�P�[��
    const float minScale = 0.05f;
    //Dotween���s���̑Ώ�Popup��Transform�ő�X�P�[��
    const int maxScale = 1;

    [Header("���C�����j���[")]
    [Header("�V�[���J�ڎ��ɒ����ւ������擾���邽��")]
    [SerializeField] DataAction useData;

    [Header("�p�l���̑J�ڐ�ʒu(�C���Q�[���ł�0=resultLastPosition)")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("���ۂɓ������p�l��(�C���Q�[���ł�0=resultPanel)")]
    [SerializeField] RectTransform[] movePanel;

    [Header("�T�E���h�̃|�b�v�A�b�v(�C���Q�[���ł��g�p)")]
    [SerializeField] GameObject soundPopup;

    [Header("�����ւ��̃|�b�v�A�b�v")]
    [SerializeField] GameObject clothPopup;

    [Header("�|�b�v�A�b�v�̐e�I�u�W�F�N�g(�C���Q�[���ł��g�p)")]
    [SerializeField] GameObject popupParent;

    [Header("�v���C���[�̃f�[�^�ւ̎Q��")]
    [SerializeField] DataAction dataAction;

    [Header("�C���Q�[��")]
    [Header("�|�[�Y�̃|�b�v�A�b�v")]
    [SerializeField] GameObject porzPopup;

    [Header("�V�[���J�ڃf�[�^�̎Q��")]
    [SerializeField] DataReciver referencsData;

    [Header("�Q�[�����A�N�e�B�u�I�u�W�F�N�g�̐e")]
    [SerializeField] GameObject activeParent;

    [Header("�|�[�Y�p�I�u�W�F�N�g")]
    [SerializeField] RePlayDatas replayer;

    [Header("�Q�[�����f�[�^�ւ̎Q��")]
    [SerializeField] InGameStockData gameDatas;

    private int sportTypeNumber = 0;
    public enum MoveDirection
    {
        left,
        right,
        over
    }

    public enum PopupName
    {
        None,
        Sound,
        Cloth,
        Porz
    }

    public enum PopupOperaion
    {
        display,
        hidden
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.menu);
        }
        else if (SceneManager.GetActiveScene().name == playSceneName)
        {
            AudioManager.instance.PlayBGM(AudioManager.BGM.play);
        }
    }

    /// <summary>
    /// �p�l��
    /// </summary>

    //�p�l���̑J�ڂ����s����֐�
    public void PanelMove(MoveDirection directionName, int panelNumber)
    {
        //���U���g��ʂ�\�����邽�߂̑J��
        if (directionName == MoveDirection.over)
        {
            Vector3 downPosition = new Vector3(0, 0, 0);
            RectTransform myTrans = movePanel[panelNumber];
            myTrans.DOMove(downPosition, animationSpeed);
        }
        else
        {
            //���C�����j���[��ʑJ��
            AudioManager.instance.PlaySE(AudioManager.SE.panelMove);
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
                    myTrans.DOMove(leftPosition, animationSpeed);
                    break;
                case MoveDirection.right:
                    myTrans.DOMove(rightPosition, animationSpeed);
                    break;
            }
        }
    }

    /// <summary>
    /// �|�b�v�A�b�v
    /// </summary>

    //�|�b�v�A�b�v�̕\�����s���֐�
    public void PopupDisplay(PopupName popName)
    {
        popupParent.SetActive(true);
        RectTransform myTrans = null;
        AudioManager.instance.PlaySE(AudioManager.SE.popUp);
        switch (popName)
        {
            case PopupName.Sound:
                soundPopup.SetActive(true);
                myTrans = soundPopup.GetComponent<RectTransform>();
                break;
            case PopupName.Cloth:
                clothPopup.SetActive(true);
                myTrans = clothPopup.GetComponent<RectTransform>();
                break;
            case PopupName.Porz:
                porzPopup.SetActive(true);
                myTrans = porzPopup.GetComponent<RectTransform>();
                ActiveOperation(true);
                break;
        }
        myTrans.DOScale(new Vector3(maxScale, maxScale, maxScale), animationSpeed);
    }

    //�|�b�v�A�b�v�̔�\�����s���֐�
    public void PopupHidden(PopupName popName)
    {
        GameObject myObj = null;
        AudioManager.instance.PlaySE(AudioManager.SE.popDown);
        switch (popName)
        {
            case PopupName.Sound:
                myObj = soundPopup;
                break;
            case PopupName.Cloth:
                myObj = clothPopup;
                break;
            case PopupName.Porz:
                myObj = porzPopup;
                ActiveOperation(false);
                break;
        }
        myObj.GetComponent<RectTransform>()
            .DOScale(new Vector3(minScale, minScale, 1), animationSpeed)
            .OnComplete(() =>
            {
        myObj.SetActive(false);
        popupParent.SetActive(false);
            });
    }

    //�A�����ă|�b�v�A�b�v��\�����邽�߂̊֐�
    public void PopupChainDisplay(PopupOperaion operationName, PopupName targetPopName)
    {
        GameObject myObj = null;
        switch (targetPopName)
        {
            case PopupName.Sound:
                myObj = soundPopup;
                break;
            case PopupName.Cloth:
                myObj = clothPopup;
                break;
            case PopupName.Porz:
                myObj = porzPopup;
                break;
        }
        switch (operationName)
        {
            case PopupOperaion.display:
                myObj.SetActive(true);
                myObj.GetComponent<RectTransform>().DOScale(new Vector3(maxScale, maxScale, maxScale), animationSpeed);
                break;
            //���g���\���ɂ���̂�
            case PopupOperaion.hidden:
                myObj.GetComponent<RectTransform>().DOScale(new Vector3(minScale, minScale, 1), animationSpeed).OnComplete(() => myObj.SetActive(false));
                break;
        }
    }

    //�|�[�Y�Ɋւ��鏈��(���e�������邲�Ƃɒǉ����Ă���)
    private void ActiveOperation(bool Operation)
    {
        if(SceneManager.GetActiveScene().name == menuSceneName) { return; }
        //�~�߂�ꍇ�̏���
        if (Operation)
        {
            replayer.StopGame();
            activeParent.SetActive(false);
            gameDatas.GimicCalculating = false;
        }
        else
        {
            gameDatas.GimicCalculating = true;
            activeParent.SetActive(true);
            replayer.ReStartGame();
        }
    }

    /// <summary>
    /// �V�[���J��
    /// </summary>

    //�X�|�[�c�̎�ނ��w�肵�Ȃ��V�[���J��
    public void SimpleSceneMover(int sceneIndex)
    {
        switch (sceneIndex)
        {
            //�v���C�V�[���Ɉړ�����
            case 0:
                AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
                SceneManager.sceneLoaded += ReloadScene;
                SceneManager.LoadScene(playSceneName);
                break;
            //���j���[�V�[���Ɉړ�����
            case 1:
                AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
                SceneManager.LoadScene(menuSceneName);
                break;
        }
    }

    //�ʏ�v���C���[�h�ŃV�[���J��
    public void SceneMoveStarter(int sendSportTypeNumber)
    {
        AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
        sportTypeNumber = sendSportTypeNumber;
        SceneMove();
    }
    //���ۂ̃V�[���J��
    private void SceneMove()
    {
        ActiveOperation(false);
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
        datareciver.heartAmount = stockData.heartLevel;
        datareciver.coinLevel = stockData.coinLevel;
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    private void ReloadScene(Scene next, LoadSceneMode mode)
    {
        //�J�ڐ�V�[���̃I�u�W�F�N�g����
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //�f�[�^�̎擾
        datareciver.sportType = referencsData.sportType;
        datareciver.clothSprite = referencsData.clothSprite;
        datareciver.heartAmount = referencsData.heartAmount;
        datareciver.coinLevel = referencsData.coinLevel;
        SceneManager.sceneLoaded -= ReloadScene;
    }
}
