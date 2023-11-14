using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinBall : MonoBehaviour
{
    [SerializeField] float bouncePower;
    [SerializeField] float tapPower;

    const string bumperTagName = "bumper";

    Rigidbody2D boalRig;
    Transform boalTra;

    private void Start()
    {
        GameObject ob = gameObject;
        boalRig = ob.GetComponent<Rigidbody2D>();
        boalTra = ob.GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(bumperTagName))
        {
            boalRig.Sleep();
            boalRig.AddForce((boalTra.position - collision.gameObject.GetComponent<Transform>().position).normalized * bouncePower, ForceMode2D.Impulse);
        }
    }

    public void ShotBall(Vector2 reflectDirection)
    {
        boalRig.Sleep();
        boalRig.AddForce(reflectDirection * tapPower, ForceMode2D.Impulse);
    }
}
