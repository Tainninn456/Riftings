using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";

    TextDiplay texDisplay = new TextDiplay();
    [Header("コインマネージャー")]
    [SerializeField] coinManager cMane;
    [Header("コインの取得数を表示するテキスト")]
    [SerializeField] TextMeshProUGUI coinTex;


    TextAction parentTextAction;
    InGameStockData parentGameStockData;
    CoinInformation coinInfo;

    private void Start()
    {
        Debug.Log(gameObject.transform.parent);
        Transform parent = gameObject.transform.parent;
        parentTextAction = parent.GetComponent<TextAction>();
        parentGameStockData = parent.GetComponent<InGameStockData>();
        coinInfo = parent.GetComponent<CoinInformation>();
        Debug.Log(parentTextAction);
        Debug.Log(parentGameStockData);
        Debug.Log(coinInfo);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(coinTagName))
        {
           // GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.add);
            //cMane.coinInfo.ReturnCoin(collision.gameObject);
            //texDisplay.TextDisplaing(coinTex, GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.use));
            parentGameStockData.coinCount += coinInfo.CoinValue;
            parentTextAction.CoinCountDisplay(parentGameStockData.coinCount);
        }
    }
}
