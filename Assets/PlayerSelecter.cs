using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelecter : MonoBehaviour
{
    //ƒvƒŒƒCƒ„[‚ÌŠi”[”
    const int childCount = 9;
    void Start()
    {
        GameObject mine = gameObject;
        List<GameObject> objs = new List<GameObject>();
        for(int i = 0; i < childCount; i++)
        {
            objs.Add(mine.transform.GetChild(i).gameObject);
            objs[i].SetActive(false);
        }
        Debug.Log(GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.game));
        objs[GameManager.Instance.InformationAccess(GameManager.Information.mode, GameManager.Instruction.use, GameManager.ModeName.soccer, GameManager.State.game)].SetActive(true);
    }
}
