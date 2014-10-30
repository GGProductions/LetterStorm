using UnityEngine;
using System.Collections;

public class boss_spinTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float rotationSpeed = 300 * Time.deltaTime;
		transform.Rotate(new Vector3(1, 0, 0) * rotationSpeed);
	}
}
