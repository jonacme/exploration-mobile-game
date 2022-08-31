using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector2 input;
    public float moveSpeed;    
    public bool isMoving;

    [Header("Layer Settings")]
    public LayerMask ForeGround;        // layer Called ForeGround & MoutainsAndTree
    public LayerMask MoutainsAndSea;    
    public LayerMask TreeAndBushes;  // random encounter layer in world map.

    private Enemies enemy;

    

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //removediagnoal Movement
            if (input.x != 0) input.y = 0;


            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));  
            }            
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        RandomEncounter();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, ForeGround) != null)
        {
            return false;
        }
        else if(Physics2D.OverlapCircle(targetPos, 0.2f, MoutainsAndSea) != null)
        {
            return false;
        }
        return true;
    }

    private void RandomEncounter()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, TreeAndBushes) != null)
        {
            if (Random.Range(1, 101) <= 10)    
            {
                GameManager.Instance.battleScene(enemy);
            }
        }
    }  
    
}
