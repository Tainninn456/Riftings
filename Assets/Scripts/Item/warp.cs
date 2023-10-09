using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warp : MonoBehaviour
{
    public float x; //public float y;
    public static bool hit = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball") && hit && Main.over != true)
        {
            Main.warpbool = true;
            hit = false;
            Main.brigi.Sleep();
            switch (x)
            {
                case 134.81f:
                    Main.brigi.AddForce(new Vector2(0.5f, 0), ForceMode2D.Impulse);
                    break;
                case -134.81f:
                    Main.brigi.AddForce(new Vector2(-0.5f, 0), ForceMode2D.Impulse);
                    break;
                default:
                    break;
            }
        }
    }
}
