using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    const string ballTagName = "ball";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�{�[���ƏՓ˂����ۂɃR�C���̐��𑝉�
        if (collision.gameObject.CompareTag(ballTagName))
        {
            GameManager.Instance.InformationAccess(GameManager.Information.coin, GameManager.Instruction.add);
            gameObject.SetActive(false);
        }
    }
}
