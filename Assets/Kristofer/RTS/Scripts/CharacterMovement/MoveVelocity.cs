using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{
    private Vector3 velocityVector;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetVelocity(Vector3 velocityVector)
    {

        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {

        rb.velocity = velocityVector * moveSpeed;
        
    }
}
