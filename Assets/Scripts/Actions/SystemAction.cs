using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SystemAction : MonoBehaviour
{
    const string menuSceneName = "menuScene";
    const string playSceneName = "playScene";

    //Dotweenアニメーションのスピード
    const float animationSpeed = 0.3f;
    //Dotween実行時の対象PopupのTransform最小スケール
    const float minScale = 0.05f;
    //Dotween実行時の対象PopupのTransform最大スケール
    const int maxScale = 1;

    [Header("メインメニュー")]
    [Header("シーン遷移時に着せ替え情報を取得するため")]
    [SerializeField] DataAction useData;

    [Header("パネルの遷移先位置(インゲームでは0=resultLastPosition)")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("実際に動かすパネル(インゲームでは0=resultPanel)")]
    [SerializeField] RectTransform[] movePanel;

    [Header("サウンドのポップアップ(インゲームでも使用)")]
    [SerializeField] GameObject soundPopup;

    [Header("着せ替えのポップアップ")]
    [SerializeField] GameObject clothPopup;

    [Header("ポップアップの親オブジェクト(インゲームでも使用)")]
    [SerializeField] GameObject popupParent;

    [Header("プレイヤーのデータへの参照")]
    [SerializeField] DataAction dataAction;

    [Header("インゲーム")]
    [Header("ポーズのポップアップ")]
    [SerializeField] GameObject porzPopup;

    [Header("シーン遷移データの参照")]
    [SerializeField] DataReciver referencsData;

    [Header("ゲーム内アクティブオブジェクトの親")]
    [SerializeField] GameObject activeParent;

    [Header("ポーズ用オブジェクト")]
    [SerializeField] RePlayDatas replayer;

    [Header("ゲーム内データへの参照")]
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
    /// パネル
    /// </summary>

    //パネルの遷移を実行する関数
    public void PanelMove(MoveDirection directionName, int panelNumber)
    {
        //リザルト画面を表示するための遷移
        if (directionName == MoveDirection.over)
        {
            Vector3 downPosition = new Vector3(0, 0, 0);
            RectTransform myTrans = movePanel[panelNumber];
            myTrans.DOMove(downPosition, animationSpeed);
        }
        else
        {
            //メインメニュー画面遷移
            AudioManager.instance.PlaySE(AudioManager.SE.panelMove);
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
                    myTrans.DOMove(leftPosition, animationSpeed);
                    break;
                case MoveDirection.right:
                    myTrans.DOMove(rightPosition, animationSpeed);
                    break;
            }
        }
    }

    /// <summary>
    /// ポップアップ
    /// </summary>

    //ポップアップの表示を行う関数
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

    //ポップアップの非表示を行う関数
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

    //連続してポップアップを表示するための関数
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
            //自身を非表示にするのみ
            case PopupOperaion.hidden:
                myObj.GetComponent<RectTransform>().DOScale(new Vector3(minScale, minScale, 1), animationSpeed).OnComplete(() => myObj.SetActive(false));
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
    /// シーン遷移
    /// </summary>

    //スポーツの種類を指定しないシーン遷移
    public void SimpleSceneMover(int sceneIndex)
    {
        switch (sceneIndex)
        {
            //プレイシーンに移動する
            case 0:
                AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
                SceneManager.sceneLoaded += ReloadScene;
                SceneManager.LoadScene(playSceneName);
                break;
            //メニューシーンに移動する
            case 1:
                AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
                SceneManager.LoadScene(menuSceneName);
                break;
        }
    }

    //通常プレイモードでシーン遷移
    public void SceneMoveStarter(int sendSportTypeNumber)
    {
        AudioManager.instance.PlaySE(AudioManager.SE.sceneMove);
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
        datareciver.heartAmount = stockData.heartLevel;
        datareciver.coinLevel = stockData.coinLevel;
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    private void ReloadScene(Scene next, LoadSceneMode mode)
    {
        //遷移先シーンのオブジェクト検索
        var datareciver = GameObject.FindWithTag("DataReciver").GetComponent<DataReciver>();
        //データの取得
        datareciver.sportType = referencsData.sportType;
        datareciver.clothSprite = referencsData.clothSprite;
        datareciver.heartAmount = referencsData.heartAmount;
        datareciver.coinLevel = referencsData.coinLevel;
        SceneManager.sceneLoaded -= ReloadScene;
    }
}
