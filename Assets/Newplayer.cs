using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newplayer : MonoBehaviour
{
    Rigidbody2D rig;

    bool right; bool left;

    [SerializeField] int movePowerX;
    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
    }

    private void FixedUpdate()
    {
        if (right)
        {
            rig.velocity = new Vector2(movePowerX, 0);
            right = false;
        }
        else if (left)
        {
            rig.velocity = new Vector2(-1 * movePowerX, 0);
            left = false;
        }
    }
}
