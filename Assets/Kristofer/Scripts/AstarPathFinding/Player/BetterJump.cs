using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        if(rb.velocity.y < 0)
        {

            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y  > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

    }


}
