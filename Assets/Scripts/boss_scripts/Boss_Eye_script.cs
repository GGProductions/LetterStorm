using UnityEngine;
using System.Collections;

public class Boss_Eye_script : MonoBehaviour {

    private float damp = 15f; //fast

    private Vector3 player_location_transform;
    public Quaternion angleNeeded;

    /// <summary>
    /// update the rotation of the eye only
    /// //may be able to make it blink when boss gets upset or something like that
    /// </summary>
    void Update()
    {
  

        //locating Albert must be done continuously 
        player_location_transform = GameObject.Find("AlbertPlayerPrefab").transform.position;
        angleNeeded = Quaternion.LookRotation(player_location_transform - this.transform.position);
        //tilt the eye and aim acording to the damp factor for speed of rotation
        transform.rotation = Quaternion.Slerp(this.transform.rotation, angleNeeded, Time.deltaTime * damp);


    }

}
