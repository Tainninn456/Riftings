using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAction : MonoBehaviour
{
    [Header("タッチエフェクト")]
    [SerializeField] GameObject touchEffect;

    //タッチエフェクトの場所
    Transform effectTrans;

    private void Start()
    {
        effectTrans = touchEffect.GetComponent<Transform>();
    }
    void Update()
    {
        if(Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchEffect.SetActive(true);
                    effectTrans.position = GetTouchPosition();
                    break;
                case TouchPhase.Moved:
                    effectTrans.position = GetTouchPosition();
                    break;
                case TouchPhase.Ended:
                    touchEffect.SetActive(false);
                    break;
            }
        }
    }

    private Vector2 GetTouchPosition()
    {
        Vector2 screenPos = Input.mousePosition;
        Debug.Log(Camera.main);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}
