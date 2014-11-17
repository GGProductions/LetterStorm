using UnityEngine;
using System.Collections;

public class SmartEnemy : Enemy {

    public GameObject Projectile;
    private float timeElapsed;
    private float zDestination;
    private bool reachedWaypoint;
    private int factorDivisor;
    private Vector3 playerLoc;
    private Quaternion aim;
    private Transform shooter;
    

	// Use this for initialization
	void Start () {
        base.Start();
        reachedWaypoint = false;
        timeElapsed = 0;
        zDestination = Random.Range(1f, 4.5f);
        factorDivisor = Random.Range(12, 20);
        shooter = transform.Find("SmartEnemyShooter");
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        playerLoc = GameObject.Find("AlbertPlayerPrefab").transform.position;
        aim = Quaternion.LookRotation(playerLoc - transform.position);
        shooter.rotation = Quaternion.Slerp(transform.rotation, aim, Time.deltaTime);
        


	}

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
        //timeElapsed += Time.deltaTime;

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
        //timeElapsed += Time.deltaTime;

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
        //timeElapsed += Time.deltaTime;

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
        //timeElapsed += Time.deltaTime;

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
        timeElapsed += Time.deltaTime;
        //Debug.Log("TimeElapsed: " + timeElapsed);
        float factor = Mathf.Cos(timeElapsed * 0.5f * Mathf.PI);

        Debug.Log("Factor: " + factor);
        transform.Translate(Vector3.right * (factor / factorDivisor), Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
