using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;


        if (Input.GetKey(KeyCode.W)) moveY = +1f;
        if (Input.GetKey(KeyCode.W)) moveY = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = +1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;

        Vector3 velocityVector = new Vector3(moveX, moveY).normalized;
        GetComponent<IMoveVelocity>().SetVelocity(velocityVector);



    }


}
