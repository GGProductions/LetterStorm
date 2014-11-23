using UnityEngine;
using System.Collections;

public class movingWaypoint : MonoBehaviour {


	private float timeElapsed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime * 0.5f;
		float factor = Mathf.Cos(timeElapsed) * 0.2f;

        transform.Translate(Vector3.forward * (factor / 10) * Time.timeScale, Space.World);
	}
}
