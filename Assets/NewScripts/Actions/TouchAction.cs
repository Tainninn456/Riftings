using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAction : MonoBehaviour
{
    [Header("�^�b�`�G�t�F�N�g")]
    [SerializeField] GameObject touchEffect;

    //�^�b�`�G�t�F�N�g�̏ꏊ
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
