using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float jumpForce = 3f;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    private void Update()
    {

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        moveDirection = new Vector2(h, v);

        Movement(moveDirection);
        Flip();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();


        }

    }

    private void Flip()
    {
        if (moveDirection.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveDirection.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void Movement(Vector2 direction)
    {

        rb.velocity = (new Vector2(direction.x * moveSpeed, rb.velocity.y));

    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
        

    }



}
