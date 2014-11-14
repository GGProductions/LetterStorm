using UnityEngine;
using System.Collections;

public class SmartEnemy : Enemy {

    private float timeElapsed;
    private float zDestination;
    private bool reachedWaypoint;
    private int factorDivisor;
    
	// Use this for initialization
	void Start () {
        base.Start();
        reachedWaypoint = false;
        timeElapsed = 0;
        zDestination = Random.Range(1f, 3f);
        factorDivisor = Random.Range(8, 14);
	}
	
	// Update is called once per frame
	/*void Update () {
    
	}*/

    public override void findPath(float atm)
    {
        switch (Path)
        {
            case 0:
                BackMove(atm);
                break;
            case 1:
                RightMove(atm);
                //transform.Translate(new Vector3(0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 2:
                LeftMove(atm);
                //transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 3:
                SinMove(atm);
                break;
        }
    }

    private void BackMove(float atm)
    {
        timeElapsed += Time.deltaTime;

        if (reachedWaypoint)
        {
            WaypointReached();
        }
        else if (transform.position.z <= zDestination)
        {
                reachedWaypoint = true;
        }
        else {
            transform.Translate(Vector3.back * atm, Space.World);
        }
    }
    private void LeftMove(float atm)
    {
        timeElapsed += Time.deltaTime;

        if (reachedWaypoint)
        {
            WaypointReached();
        }
        else if (transform.position.z <= zDestination)
        {
            reachedWaypoint = true;
        }
        else
        {
            transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm, Space.World);
        }
    }
    private void RightMove(float atm)
    {
        timeElapsed += Time.deltaTime;

        if (reachedWaypoint)
        {
            WaypointReached();
        }
        else if (transform.position.z <= zDestination)
        {
            reachedWaypoint = true;
        }
        else
        {
            transform.Translate(new Vector3(0.5f, 0f, -1f) * atm, Space.World);
        }
    }
    private void SinMove(float atm)
    {
        timeElapsed += Time.deltaTime;

        if (reachedWaypoint)
        {
            WaypointReached();
        }
        else if (transform.position.z <= zDestination)
        {
            reachedWaypoint = true;
        }
        else
        {
            transform.Translate(Vector3.back * atm, Space.World);
        }
    }

    private void WaypointReached()
    {
        float factor = Mathf.Cos(timeElapsed);
        transform.Translate(Vector3.right * (factor / factorDivisor) * Time.timeScale, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
