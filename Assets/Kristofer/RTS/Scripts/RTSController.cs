using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RTSController : MonoBehaviour
{
    private Vector3 startPosition;
    private List<UnitRTS> selectedUnit;
    public Transform selectedAreaTransform;


    private void Awake()
    {
        selectedUnit = new List<UnitRTS>();
        selectedAreaTransform.gameObject.SetActive(false);

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            //Mouse pressed
            selectedAreaTransform.gameObject.SetActive(true);
            startPosition = GetWorldMousePosition();
            
        }
        
        if(Input.GetMouseButton(0))
        {
            // Selection area 
            Vector3 currentMousePosition = GetWorldMousePosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y));

            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y));

            selectedAreaTransform.position = lowerLeft;
            selectedAreaTransform.localScale = upperRight - lowerLeft;


        }

        if(Input.GetMouseButtonUp(0))
        {
            //Mouse released
            selectedAreaTransform.gameObject.SetActive(false);
            Collider2D[] colliderArray = Physics2D.OverlapAreaAll(startPosition, GetWorldMousePosition());

            //Deselect all units
            foreach (UnitRTS unitRTS in selectedUnit)
            {
                unitRTS.SetSelected(false);


            }


            selectedUnit.Clear();
            foreach(Collider2D cd in colliderArray)
            {
                UnitRTS unit = cd.GetComponent<UnitRTS>();

                if(unit != null)
                {
                    unit.SetSelected(true);
                    selectedUnit.Add(unit);
                    

                }

            }

           
            
        }


        if (Input.GetMouseButtonDown(1))
        {

            //Right button pressed
            Vector3 moveToPosition = GetWorldMousePosition();

            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 2, 5, 7 });

            int targetPositionIndex = 0;
            
            foreach(UnitRTS unit in selectedUnit)
            {

                //Move to
                unit.MoveTo(targetPositionList[targetPositionIndex]);
                targetPositionIndex = (targetPositionIndex + 1) % targetPositionList.Count;
               

            }

        }


    }
    
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[]ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionsList = new List<Vector3>();
        positionsList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionsList.AddRange(GetPositionAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));


        }

        return positionsList;

    }

    private List<Vector3> GetPositionAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);

           

        }

        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 v3,float angle)
    {
        return Quaternion.Euler(0, 0, angle) * v3;

    }


    //Getting mouse world position

    public Vector3 GetWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public Vector3 GetWorldMousePosition()
    {

        Vector3 v3 = GetWorldPositionWithZ(Input.mousePosition, Camera.main);
        v3.z = 0f;
        return v3;


    }

}
