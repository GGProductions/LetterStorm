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
        shooter.rotation = Quaternion.Slerp(transform.rotation, aim, Time.deltaTime * 100f);

	}

    /// <summary>
    /// A swith case that calls whichever movement algorithm has been predetermined based on an enemy's spawning location.
    /// </summary>
    /// <param name="atm">The distance an enemy is to move evey frame update.</param>
    public override void MoveEnemy(float atm)
    {
        switch (Path)
        {
            case 0:
                BackMove(atm);
                break;
            case 1:
                RightMove(atm);
                break;
            case 2:
                LeftMove(atm);
                break;
            case 3:
                SinMove(atm);
                break;
        }
    }
    
    /// <summary>
    /// The enemy moves straight back and then back and forth. The enemy remains on screen until the player destroys it.
    /// </summary>
    /// <param name="atm">The distance an enemy is to move evey frame update.</param>
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
            transform.Translate(Vector3.back * atm * Time.timeScale, Space.World);
        }
    }
    /// <summary>
    /// The enemy has spawned sufficiently far to the right that it will move in a down-left direction and then back and forth. The enemy remains
    /// on screen until the player destroys it.
    /// </summary>
    /// <param name="atm">The distance an enemy is to move evey frame update.</param>
    private void LeftMove(float atm)
    {
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
            transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm * Time.timeScale, Space.World);
        }
    }
    /// <summary>
    /// The enemy has spawned sufficiently far to the left that it will move in a down-right direction and then back and forth. The enemy remains
    /// on screen until the player destroys it.
    /// </summary>
    /// <param name="atm">The distance an enemy is to move evey frame update.</param>
    private void RightMove(float atm)
    {
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
            transform.Translate(new Vector3(0.5f, 0f, -1f) * atm * Time.timeScale, Space.World);
        }
    }
    private void SinMove(float atm)
    {
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
            transform.Translate(Vector3.back * atm * Time.timeScale, Space.World);
        }
    }
    /// <summary>
    /// Once an enemy has reached a specific point on the screen, it no longer moves along the z-axis, and simply moves back and forth until it is destroyed, firing at the player.
    /// </summary>
    private void WaypointReached()
    {
        timeElapsed += Time.deltaTime;
        //Debug.Log("TimeElapsed: " + timeElapsed);
        float factor = Mathf.Cos(timeElapsed * 0.5f * Mathf.PI);

        transform.Translate(Vector3.right * (factor / factorDivisor) * Time.timeScale, Space.World);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
