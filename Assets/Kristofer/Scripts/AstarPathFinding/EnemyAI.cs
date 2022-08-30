using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;





public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform art;
    private Rigidbody2D rb;
    Seeker seeker;
    Path path;

    public float moveSpeed = 10f;
    public float nextWayPointDistance = 3f;

    private int currentWayPoint = 0;
    private bool reachedEndDestination;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    private void FixedUpdate()
    {
        if (path == null)
            return; 

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndDestination = true;
            return;
        }
        else
        {
            reachedEndDestination = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;

        rb.AddForce(force);

        var distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if(force.x >= 0.01f)
        {
            art.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(force.x <= -0.01f)
        {

            art.localScale = new Vector3(-1f, 1f, 1f);
        }

    }


    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;

        }


    }

}
    