using UnityEngine;
using System.Collections;

public class SmartEnemy : Enemy {

    // vars to control sine wave movement
    public Transform target1;
    public Transform target2;

    private float amplitudeX = 10f;
    private float amplitudeZ = 5f;
    private float omegaX = 0.1f;
    private float omegaZ = 0.1f;

    private bool reachedMiddle = false;

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
                SinMovePrep(atm);
                break;
        }
    }

    private void SinMovePrep(float atm)
    {
        if (reachedMiddle)
        {
            SinMove(atm);
        }
        else if (transform.position.z <= 1.5f)
        {
            //sinusoidal patrol
            //transform.Translate(new Vector3(1f, 0, 0) * atm, Space.World);
            reachedMiddle = true;
        }
        else
        {
            transform.Translate(Vector3.back * atm, Space.World);
        }
    }

    private void SinMove(float atm)
    {
        float x = amplitudeX * Mathf.Cos(atm);
        float z = Mathf.Abs(amplitudeZ * Mathf.Sin(atm));
        transform.localPosition = new Vector3(x, 0, z);
    }
}
