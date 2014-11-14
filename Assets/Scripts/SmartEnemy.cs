using UnityEngine;
using System.Collections;

public class SmartEnemy : Enemy {

    private float timeElapsed;
    private bool reachedWaypoint;
    
	// Use this for initialization
	void Start () {
        base.Start();
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	/*void Update () {
    
	}*/

    public override void findPath(float atm)
    {
        switch (Path)
        {
            case 0:
                transform.Translate(Vector3.back * atm, Space.World);
                break;
            case 1:
                transform.Translate(new Vector3(0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 2:
                transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 3:
                SinMove(atm);
                break;
        }
    }

    private void SinMove(float atm)
    {
        timeElapsed += Time.deltaTime;

        if (reachedWaypoint)
        {
            float factor = Mathf.Cos(timeElapsed);
            transform.Translate(Vector3.right * (factor / 20) * Time.timeScale, Space.World);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.z <= 1f) {
                reachedWaypoint = true;
        }
        else {
            transform.Translate(Vector3.back * atm, Space.World);
        }
    }
}
