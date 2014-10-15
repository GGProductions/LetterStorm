using UnityEngine;
using System.Collections;

public class SmartEnemy : Enemy {

	/*// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
     
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
        if (transform.position.z <= 1.5f)
        {
            //sinusoidal patrol
            transform.Translate(new Vector3(1f, 0, 0) * atm, Space.World);
        }
        else
        {
            transform.Translate(Vector3.back * atm, Space.World);
        }
    }
}
