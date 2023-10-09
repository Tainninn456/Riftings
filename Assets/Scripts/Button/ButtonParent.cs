using UnityEngine;

public class ButtonParent : MonoBehaviour
{
    public ButtonParent button;
    public void OnClick()
    {
        button.OnClick(gameObject.name);
    }
    public  virtual void OnClick(string objectName)
    { }
}
