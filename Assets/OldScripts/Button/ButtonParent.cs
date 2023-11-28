using UnityEngine;

public class ButtonsParent : MonoBehaviour
{
    //public ButtonParent button;
    public void OnClick()
    {
        OnClick(gameObject.name);
    }
    public  virtual void OnClick(string objectName)
    { }
}
