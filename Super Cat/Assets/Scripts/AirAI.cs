using UnityEngine;
using System.Collections;
using Pathfinding;

public class AirAI : MonoBehaviour {

    public Transform target;
    // How many times a second i will update our path;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rb;
    // The calculated path
    public Path path;
    // The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;
    [HideInInspector]
    public bool pathIsEnded = false;
    // The max didtance from the AI to a waipont for it ro continue to the next waipoint
    public float nextWaypointDistance = 3;
    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    private bool SearchingForTarget = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            if (!SearchingForTarget)
            {
                SearchingForTarget = true;
                StartCoroutine(SearchForTarget());
            }
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }

    private IEnumerator SearchForTarget()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("AirTarget");

        if (GameObject.FindGameObjectWithTag("Bullet")) // if it is a bullet the taget is the Player.
        {
            sResult = GameObject.FindGameObjectWithTag("Player");
        }

        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForTarget());
        }
        else
        {
            target = sResult.transform;
            SearchingForTarget = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    private IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!SearchingForTarget)
            {
                SearchingForTarget = true;
                StartCoroutine(SearchForTarget());
            }
            yield return false;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            if (!SearchingForTarget)
            {
                SearchingForTarget = true;
                StartCoroutine(SearchForTarget());
            }
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathIsEnded)
                return;

            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;

        // Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }
    }
}
