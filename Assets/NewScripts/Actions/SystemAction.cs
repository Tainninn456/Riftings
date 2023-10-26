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

    [Header("パネルの遷移先位置")]
    [SerializeField] RectTransform[] movePanelPositions;

    [Header("実際に動かすパネル")]
    [SerializeField] RectTransform[] movePanel;

    [Header("サウンドのポップ")]
    [SerializeField] GameObject soundPop;

    [Header("着せ替えのポップ")]
    [SerializeField] GameObject clothPop;

    [Header("ポップの親オブジェクト")]
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

    //外部入力からシーン遷移の開始を宣言
    public void SceneMoveStarter(int sendSportTypeNumber)
    {
        sportTypeNumber = sendSportTypeNumber;
        SceneMove();
    }
    //実際のシーン遷移
    private void SceneMove()
    {
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
}
