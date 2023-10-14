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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(coinTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.add);
            cMane.coinInfo.ReturnCoin(collision.gameObject);
            collision.gameObject.SetActive(false);
            texDisplay.TextDisplaing(coinTex, GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.use));
        }
    }
}
