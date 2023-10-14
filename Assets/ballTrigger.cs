using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballTrigger : MonoBehaviour
{
    const string coinTagName = "coin";

    [Header("コインマネージャー")]
    [SerializeField] coinManager cMane;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag(coinTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.add);
            cMane.coinInfo.ReturnCoin(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
