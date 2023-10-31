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

    [Header("�p�l���̑J�ڐ�ʒu(�C���Q�[���ł�0=resultLastPosition)")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("���ۂɓ������p�l��(�C���Q�[���ł�0=resultPanel)")]
    [SerializeField] RectTransform[] movePanel;

    [Header("���[�h�`�F���W�p�̉�]�A�N�V����(0=menu,1=sub)")]
    [SerializeField] GameObject[] rotationGroups;

    [Header("�T�E���h�̃|�b�v(�C���Q�[���ł��g�p)")]
    [SerializeField] GameObject soundPop;

    [Header("�����ւ��̃|�b�v")]
    [SerializeField] GameObject clothPop;

    [Header("�|�b�v�̐e�I�u�W�F�N�g(�C���Q�[���ł��g�p)")]
    [SerializeField] GameObject popParent;

    [SerializeField] DataAction dataAction;

    [Header("�C���Q�[��")]
    [Header("�|�[�Y�̃|�b�v")]
    [SerializeField] GameObject porzPop;

    [Header("�V�[���J�ڃf�[�^�̎Q��")]
    [SerializeField] DataReciver referencsData;

    [Header("�Q�[�����A�N�e�B�u�I�u�W�F�N�g�̐e")]
    [SerializeField] GameObject activeParent;

    [Header("�|�[�Y�p�I�u�W�F�N�g")]
    [SerializeField] RePlayDatas replayer;

    private int sportTypeNumber = 0;
    public enum MoveDirection
    {
        left,
        right,
        over
    }

    public enum PopName
    {
        None,
        Sound,
        Cloth,
        Porz
    }

    public enum PopOperaition
    {
        up,
        down
    }

    public void PanelMove(MoveDirection directionName, int panelNumber)
    {
        if (directionName == MoveDirection.over)
        {
            Vector3 downPosition = new Vector3(0, 0, 0);
            RectTransform myTrans = movePanel[panelNumber];
            myTrans.DOMove(downPosition, animaionSpeed).OnComplete(() => Debug.Log("##"));
        }
        else
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
    }

    /// <summary>
    /// ���L�|�b�v�n
    /// </summary>

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
            case PopName.Porz:
                porzPop.SetActive(true);
                myTrans = porzPop.GetComponent<RectTransform>();
                ActiveOperation(true);
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
            case PopName.Porz:
                myObj = porzPop;
                ActiveOperation(false);
                break;
        }
        myObj.GetComponent<RectTransform>().DOScale(new Vector3(minScale, minScale, 1), animaionSpeed).OnComplete(() => myObj.SetActive(false)).OnComplete(() => popParent.SetActive(false));
    }

    public void PopChain(PopOperaition operationName, PopName targetPopName)
    {
        GameObject myObj = null;
        switch (targetPopName)
        {
            case PopName.Sound:
                myObj = soundPop;
                break;
            case PopName.Cloth:
                myObj = clothPop;
                break;
            case PopName.Porz:
                myObj = porzPop;
                break;
        }
        switch (operationName)
        {
            case PopOperaition.up:
                myObj.SetActive(true);
                myObj.GetComponent<RectTransform>().DOScale(new Vector3(maxScale, maxScale, maxScale), animaionSpeed);
                break;
            //���g���\���ɂ���̂�
            case PopOperaition.down:
                myObj.GetComponent<RectTransform>().DOScale(new Vector3(minScale, minScale, 1), animaionSpeed).OnComplete(() => myObj.SetActive(false));
                break;
        }
    }

    public void RotationGroups(int rotatoNumber)
    {
        Transform r1tra = rotationGroups[1].GetComponent<Transform>();
        Transform r0tra = rotationGroups[0].GetComponent<Transform>();
        switch (rotatoNumber)
        {
            //���C������s���{�[����
            case 0:
                Debug.Log("#");
                r1tra.Rotate(new Vector3(0, -90, 0));
                rotationGroups[1].SetActive(true);
                r0tra.DORotate(new Vector3(0, -90, 0), 2).OnComplete(() => r1tra.DORotate(new Vector3(0, 0, 0), 2)).OnComplete(() => rotationGroups[0].SetActive(false));
                break;
            //�s���{�[�����烁�C����
            case 1:
                r0tra.Rotate(new Vector3(0, -90, 0));
                rotationGroups[0].SetActive(true);
                r1tra.DORotate(new Vector3(0, -90, 0), 2).OnComplete(() => r0tra.DORotate(new Vector3(0, 0, 0), 2)).OnComplete(() => rotationGroups[1].SetActive(false));
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
            replayer.StopWorldInfrence();
            activeParent.SetActive(false);
        }
        else
        {
            activeParent.SetActive(true);
            replayer.ReWorldInfrence();
        }
    }

    /// <summary>
    /// ���L�V�[���J�ڌn
    /// </summary>

    //�X�|�[�c�̎�ނ��w�肵�Ȃ��V�[���J��
    public void SimpleSceneMover(int sceneIndex)
    {
        ActiveOperation(false);
        switch (sceneIndex)
        {
            case 0:
                SceneManager.sceneLoaded += ReloadScene;
                SceneManager.LoadScene(playSceneName);
                break;
            case 1:
                SceneManager.LoadScene(menuSceneName);
                break;
        }
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
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    private void ReloadScene(Scene next, LoadSceneMode mode)
    {
        //�J�ڐ�V�[���̃I�u�W�F�N�g����
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //�f�[�^�̎擾
        datareciver.sportType = referencsData.sportType;
        datareciver.clothSprite = referencsData.clothSprite;
        SceneManager.sceneLoaded -= ReloadScene;
    }
}
