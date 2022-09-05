using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    private GameObject selectedGameObject;
    private IMovePosition movePosition;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        movePosition = GetComponent<IMovePosition>();
        selectedGameObject.SetActive(false);
    }


    public void SetSelected(bool visible)
    {

        selectedGameObject.SetActive(visible);


    }

    public void MoveTo(Vector3 targetPosition)
    {

        movePosition.SetMovePosition(targetPosition);

    }
}
