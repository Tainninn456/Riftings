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

    [Header("メインメニュー")]
    [Header("シーン遷移時に着せ替え情報を取得するため")]
    [SerializeField] DataAction useData;

    [Header("パネルの遷移先位置(インゲームでは0=resultLastPosition)")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("実際に動かすパネル(インゲームでは0=resultPanel)")]
    [SerializeField] RectTransform[] movePanel;

    [Header("モードチェンジ用の回転アクション(0=menu,1=sub)")]
    [SerializeField] GameObject[] rotationGroups;

    [Header("サウンドのポップ(インゲームでも使用)")]
    [SerializeField] GameObject soundPop;

    [Header("着せ替えのポップ")]
    [SerializeField] GameObject clothPop;

    [Header("ポップの親オブジェクト(インゲームでも使用)")]
    [SerializeField] GameObject popParent;

    [SerializeField] DataAction dataAction;

    [Header("インゲーム")]
    [Header("ポーズのポップ")]
    [SerializeField] GameObject porzPop;

    [Header("シーン遷移データの参照")]
    [SerializeField] DataReciver referencsData;

    [Header("ゲーム内アクティブオブジェクトの親")]
    [SerializeField] GameObject activeParent;

    [Header("ポーズ用オブジェクト")]
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
                //着せ替えパネルの場合
                case 0:
                    leftPosition = movePanelPositions[1].position;
                    rightPosition = movePanelPositions[2].position;
                    break;
                //スコアパネルの場合
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
    /// 下記ポップ系
    /// </summary>

    //下記popに関して
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
            //自身を非表示にするのみ
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
            //メインからピンボールへ
            case 0:
                Debug.Log("#");
                r1tra.Rotate(new Vector3(0, -90, 0));
                rotationGroups[1].SetActive(true);
                r0tra.DORotate(new Vector3(0, -90, 0), 2).OnComplete(() => r1tra.DORotate(new Vector3(0, 0, 0), 2)).OnComplete(() => rotationGroups[0].SetActive(false));
                break;
            //ピンボールからメインへ
            case 1:
                r0tra.Rotate(new Vector3(0, -90, 0));
                rotationGroups[0].SetActive(true);
                r1tra.DORotate(new Vector3(0, -90, 0), 2).OnComplete(() => r0tra.DORotate(new Vector3(0, 0, 0), 2)).OnComplete(() => rotationGroups[1].SetActive(false));
                break;
        }
    }

    //ポーズに関する処理(内容が増えるごとに追加していく)
    private void ActiveOperation(bool Operation)
    {
        if(SceneManager.GetActiveScene().name == menuSceneName) { return; }
        //止める場合の処理
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
    /// 下記シーン遷移系
    /// </summary>

    //スポーツの種類を指定しないシーン遷移
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

    //外部入力からシーン遷移の開始を宣言
    public void SceneMoveStarter(int sendSportTypeNumber)
    {
        sportTypeNumber = sendSportTypeNumber;
        SceneMove();
    }
    //実際のシーン遷移
    private void SceneMove()
    {
        ActiveOperation(false);
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene(playSceneName);
    }
    //シーン遷移内で受け渡すデータの内容
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        //遷移先シーンのオブジェクト検索
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //データの取得
        Data stockData = dataAction.DataCopy();
        datareciver.sportType = sportTypeNumber;
        datareciver.clothSprite = dataAction.sportSprites[sportTypeNumber];
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    private void ReloadScene(Scene next, LoadSceneMode mode)
    {
        //遷移先シーンのオブジェクト検索
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //データの取得
        datareciver.sportType = referencsData.sportType;
        datareciver.clothSprite = referencsData.clothSprite;
        SceneManager.sceneLoaded -= ReloadScene;
    }
}
