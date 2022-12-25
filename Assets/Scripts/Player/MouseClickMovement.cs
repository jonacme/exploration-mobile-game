using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseClickMovement : MonoBehaviour
{
    public Tilemap map;

    public bool isMoving;
    public Vector3Int targetCell;
    public Vector3Int currentCell;

    public NetWorkId id;


    // Start is called before the first frame update
    void Start()
    {
        isMoving= false;
    }

    Vector3Int RelativeCell(Vector2 direction)
    {
        return map.WorldToCell(transform.position + (Vector3) direction);
    }

    public void SetPos(Vector2 pos)
    {
        transform.position = map.WorldToCell(pos);       // want to see how it works with time.deltaTime
    }

    IEnumerator WaitToMove()
    {
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetCell = map.WorldToCell(worldPos);

        isMoving= true;

        while(map.WorldToCell(transform.position) != targetCell)
        {
            yield return new WaitForSeconds(0.3f);

            var diff = targetCell - map.WorldToCell(transform.position);

            Vector3Int nextCell;
            if(Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            {
                nextCell = RelativeCell((new Vector2(diff.x, 0)).normalized);
            }
            else
            {
                nextCell = RelativeCell((new Vector2(0, diff.y)).normalized);
            }

            //transform.position = map.WorldToCell(nextCell);
            var json = JsonUtility.ToJson(new Vector3(nextCell.x, nextCell.y, nextCell.z));
            StartCoroutine(Fetch.instance.Post("http://127.0.0.1:8125/set-positions/" + id._name, json));
           
        }

        isMoving= false;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!isMoving)
            {
                StartCoroutine(WaitToMove());
                Debug.Log("Move?");
            }
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y, 0 * Time.deltaTime);
    }

}
