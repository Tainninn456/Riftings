using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    public static bool rM; public static bool lM;
    public void RPushDown()
    {
        rM = true;
    }
    public void LPushDown()
    {
        lM = true;
    }
    public void RPushUp()
    {
        rM = false;
    }
    public void LPushUp()
    {
        lM = false;
    }
}
