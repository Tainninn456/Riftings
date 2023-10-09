using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWarp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            Main.brigi.Sleep();
            Main.bTra.transform.localPosition = new Vector2(Random.Range(-60, 60), 180);
        }
    }
}
