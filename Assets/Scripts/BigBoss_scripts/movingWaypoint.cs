using UnityEngine;
using System.Collections;

public class movingWaypoint : MonoBehaviour {


	private float timeElapsed;
	// Use this for initialization
	void Start () {
        transform.GetComponent<MeshRenderer>().enabled = false;

    //    this.transform.GetComponent<MeshRenderer>().enabled= false;
	}
	
	// Update is called once per frame
    void Update()
    { }
    
}
    