using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseInput : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {

            var moveTo = GetComponent<RTSController>().GetWorldMousePosition();

            GetComponent<IMovePosition>().SetMovePosition(moveTo);

        }
    }

}
