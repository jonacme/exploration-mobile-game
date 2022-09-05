using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour, IMovePosition
{
    private Vector3 movePosition;

    public void SetMovePosition(Vector3 movePosition)
    {

        this.movePosition = movePosition;

    }


    private void Update()
    {
        Vector3 moveDirection = (movePosition - transform.position).normalized;

        if (Vector3.Distance(movePosition, transform.position) < 1f) moveDirection = Vector3.zero;
        GetComponent<IMoveVelocity>().SetVelocity(moveDirection);

    }


}
