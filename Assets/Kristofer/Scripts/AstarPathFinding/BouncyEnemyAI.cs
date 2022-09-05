using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BouncyEnemyAI : MonoBehaviour
{

    public Transform target;
    public float activateDistance = 20f;
    public float updatePerSeconds = 0.5f;

    [Header("Enemy Stats")]
    public float speed = 100f;
    public float nextWayPointDistance = 3f;
    public float jumpNodeHeightRequirement = 1f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Conditions")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool enemyLookAt = true;


    private Path path;
    private int currentWayPoint = 0;
    private bool isGrounded = false;
    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        

        InvokeRepeating("UpdatePath", 0f, updatePerSeconds);
        
    }

    private void FixedUpdate()
    {
        if(TargetInDistance() && followEnabled)
        {
            PathFollow();
        }


    }
    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);

        }

    }

    void PathFollow()
    {
        //17:27
        if (path == null)
            return;

        //End of path
        if (currentWayPoint >= path.vectorPath.Count)
            return;

        isGrounded = Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        //Jump
        if(jumpEnabled && isGrounded)
        {

            if(direction.y > jumpNodeHeightRequirement)
            {

                rb.AddForce(Vector2.up * speed * jumpModifier);
            }

        }

        //Movement
        rb.AddForce(force);

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        if(distance < nextWayPointDistance)
        {

            currentWayPoint++;
        }

        if(enemyLookAt)
        {

            if(rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

        }

    }

    private bool TargetInDistance()
    {

        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;

    }

    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }


    }


}
